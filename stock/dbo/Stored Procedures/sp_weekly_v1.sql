CREATE PROCEDURE [dbo].[sp_weekly_v1]
(@symbols    NVARCHAR(4000), 
 @start_dttm DATE, 
 @week_count INT            = 12
)
AS
BEGIN

    IF OBJECT_ID('tempdb..#tb_sim') IS NOT NULL
    BEGIN
        DROP TABLE #tb_sim;
    END;

    CREATE TABLE #tb_sim
    (symbol     NVARCHAR(50), 
     date_hist  DATE, 
     week_no    NVARCHAR(10), 
     price_sma5 FLOAT, 
     price_high FLOAT, 
     price_low  FLOAT, 
     vol_sma5   FLOAT
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
        DECLARE @sim_start DATE, @sim_end DATE;
        SET @sim_start = dbo.get_cur_day(@start_dttm);
        SET @sim_end = DATEADD(week, @week_count, @start_dttm);

        DECLARE @price_sma5 FLOAT, @price_high FLOAT, @price_low FLOAT, @count INT, @vol_sma5 FLOAT;

        SET @count = 1;

        WHILE(@sim_start < @sim_end)
        BEGIN
            --Get Price SMA005 for Weeks Average
            SELECT @price_sma5 = c.sma_005
            FROM daily_close c
            WHERE c.symbol = @symbol
                  AND c.date_hist = @sim_start;

            --Get Volume SMA005 for Weeks Average
            SELECT @vol_sma5 = v.sma_005
            FROM daily_volume v
            WHERE v.symbol = @symbol
                  AND v.date_hist = @sim_start;

            SELECT @price_high = MAX(h.[high]), 
                   @price_low = MIN(h.[low])
            FROM daily_history h
            WHERE h.symbol = @symbol
                  AND h.[date] BETWEEN DATEADD(day, -6, @sim_start) AND @sim_start;

            INSERT INTO #tb_sim
            (symbol, 
             date_hist, 
             week_no, 
             price_sma5, 
             price_high, 
             price_low, 
             vol_sma5
            )
            VALUES
            (
                 @symbol, 
                 @sim_start, 
                 CAST(@count AS NVARCHAR(10)), 
                 @price_sma5, 
                 @price_high, 
                 @price_low, 
                 @vol_sma5
            );

            SET @sim_start = DATEADD(week, 1, @sim_start);
            SET @count = @count + 1;

        END;

        FETCH NEXT FROM cur INTO @symbol;
    END;

    CLOSE cur;
    DEALLOCATE cur;

    SELECT s.symbol, 
           s.date_hist, 
           s.week_no, 
           ROUND(s.price_sma5, 2) AS price_sma5, 
           ROUND(s.price_high, 2) AS price_high, 
           ROUND(s.price_low, 2) AS price_low, 
           s.vol_sma5
    FROM #tb_sim s
    ORDER BY s.symbol, 
             s.date_hist DESC;
END;