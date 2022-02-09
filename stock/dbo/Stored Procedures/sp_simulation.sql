CREATE PROCEDURE [dbo].[sp_simulation]
(@symbols         NVARCHAR(4000), 
 @start_dttm      DATE, 
 @drop_limit_perc FLOAT          = -3, 
 @sell_limit_perc FLOAT          = -3, 
 @max_days_hold   INT            = 30, 
 @show_detail     CHAR(1)        = 'N'
)
AS
BEGIN


    IF OBJECT_ID('tempdb..#tb_sim') IS NOT NULL
    BEGIN
        DROP TABLE #tb_sim;
    END;

    CREATE TABLE #tb_sim
    (symbol       NVARCHAR(50), 
     buy_date     DATE, 
     sell_date    DATE, 
     buy_price    FLOAT, 
     buy_price_i  FLOAT, 
     sell_price   FLOAT, 
     sell_price_i FLOAT, 
     chg_perc     FLOAT, 
     chg_perc_i   FLOAT, 
     remark       NVARCHAR(250)
    );

    DECLARE @symbol NVARCHAR(50), @sell_limit FLOAT;

    DECLARE cur CURSOR
    FOR SELECT symbol
        FROM watch_list w
        WHERE w.symbol IN
        (
            SELECT value
            FROM STRING_SPLIT(@symbols, ',')
        );

    OPEN cur;
    FETCH NEXT FROM cur INTO @symbol;
    WHILE @@fetch_status = 0
    BEGIN
        DECLARE @sim_start DATE, @sim_end DATE, @index NVARCHAR(50);
        SET @sim_start = dbo.get_cur_day(@start_dttm);
        SET @sim_end = DATEADD(day, 365, @start_dttm);
        SET @index = 'SPY';

        DECLARE @s_open FLOAT, @s_price FLOAT, @s_low FLOAT, @count INT, @i_price FLOAT, @buy_price_limit FLOAT;

        SET @count = 0;

        WHILE(@sim_start < @sim_end)
        BEGIN

            SELECT @s_price = h.[close], 
                   @s_low = h.[low], 
                   @s_open = h.[open]
            FROM daily_history h
            WHERE h.symbol = @symbol
                  AND h.[date] = @sim_start;

            SELECT @i_price = h.[close]
            FROM daily_history h
            WHERE h.symbol = 'SPY'
                  AND h.[date] = @sim_start;

            --1. 무조건 Close Price에 산다
            IF @count = 0
            BEGIN
                INSERT INTO #tb_sim
                (symbol, 
                 buy_date, 
                 buy_price, 
                 buy_price_i
                )
                SELECT @symbol, 
                       @sim_start, 
                       @s_price, 
                       @i_price;
                SET @sim_start = DATEADD(day, 1, @sim_start);
                SET @buy_price_limit = @s_price + (@s_price * (@sell_limit_perc / 100));
                SET @count = @count + 1;
                CONTINUE;
            END;

            --2. Low가 손절보다 낮아지면 Low에 맞춰서 판다
            IF @s_open <= @buy_price_limit
            BEGIN
                UPDATE t
                  SET 
                      t.sell_date = @sim_start, 
                      t.sell_price = @s_open, 
                      t.sell_price_i = @i_price, 
                      t.chg_perc = ROUND((@s_open - t.buy_price) / t.buy_price * 100, 2), 
                      t.chg_perc_i = ROUND((@i_price - t.buy_price_i) / t.buy_price_i * 100, 2), 
                      t.remark = 'Open [' + CAST(@s_open AS NVARCHAR(10)) + '] lower than limit [' + CAST(@buy_price_limit AS NVARCHAR(10)) + ']'
                FROM #tb_sim t
                WHERE t.symbol = @symbol;
                BREAK;
            END;

            IF @s_low <= @buy_price_limit
            BEGIN
                UPDATE t
                  SET 
                      t.sell_date = @sim_start, 
                      t.sell_price = @s_low, 
                      t.sell_price_i = @i_price, 
                      t.chg_perc = ROUND((@s_low - t.buy_price) / t.buy_price * 100, 2), 
                      t.chg_perc_i = ROUND((@i_price - t.buy_price_i) / t.buy_price_i * 100, 2), 
                      t.remark = 'Low [' + CAST(@s_low AS NVARCHAR(10)) + '] lower than limit [' + CAST(@buy_price_limit AS NVARCHAR(10)) + ']'
                FROM #tb_sim t
                WHERE t.symbol = @symbol;
                BREAK;
            END;

            IF @s_price <= @buy_price_limit
            BEGIN
                UPDATE t
                  SET 
                      t.sell_date = @sim_start, 
                      t.sell_price = @s_price, 
                      t.sell_price_i = @i_price, 
                      t.chg_perc = ROUND((@s_price - t.buy_price) / t.buy_price * 100, 2), 
                      t.chg_perc_i = ROUND((@i_price - t.buy_price_i) / t.buy_price_i * 100, 2), 
                      t.remark = 'Close [' + CAST(@s_price AS NVARCHAR(10)) + '] lower than limit [' + CAST(@buy_price_limit AS NVARCHAR(10)) + ']'
                FROM #tb_sim t
                WHERE t.symbol = @symbol;
                BREAK;
            END;

            --3. 날짜가 가득차면 팝니다.
            IF @sim_start >= DATEADD(day, @max_days_hold, @start_dttm)
            BEGIN
                UPDATE t
                  SET 
                      t.sell_date = @sim_start, 
                      t.sell_price = @s_price, 
                      t.sell_price_i = @i_price, 
                      t.chg_perc = ROUND((@s_price - t.buy_price) / t.buy_price * 100, 2), 
                      t.chg_perc_i = ROUND((@i_price - t.buy_price_i) / t.buy_price_i * 100, 2), 
                      t.remark = 'Due to Date'
                FROM #tb_sim t
                WHERE t.symbol = @symbol;
                BREAK;
            END;

            SET @sim_start = DATEADD(day, 1, @sim_start);
            SET @count = @count + 1;
        END;

        FETCH NEXT FROM cur INTO @symbol;
    END;

    CLOSE cur;
    DEALLOCATE cur;

    SELECT s.symbol, 
           s.buy_date, 
           s.sell_date, 
           s.buy_price, 
           s.sell_price, 
           s.chg_perc, 
           s.chg_perc_i, 
           s.remark
    FROM #tb_sim s
    ORDER BY s.chg_perc DESC;

END;