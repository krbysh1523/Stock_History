CREATE PROCEDURE [dbo].[sp_generate_idx]
AS
    BEGIN
        --create/update daily_history
        MERGE daily_history i
        USING
        (
            SELECT 'IDX' AS symbol, 
                   dh.[date] AS hist_date, 
                   AVG(dh.[open]) AS hist_open, 
                   AVG(dh.[close]) AS hist_close, 
                   AVG(dh.[high]) AS hist_high, 
                   AVG(dh.[low]) AS hist_low, 
                   AVG(dh.[volume]) AS hist_volume
              FROM daily_history dh
             WHERE dh.symbol IN('DIA', 'QQQ', 'SPY')
             GROUP BY dh.[date]
        ) h
    ON i.symbol = h.symbol
       AND i.[date] = h.hist_date
            WHEN MATCHED
            THEN UPDATE SET i.[open] = h.hist_open, 
                            i.[close] = h.hist_close, 
                            i.[high] = h.hist_high, 
                            i.[low] = h.hist_low, 
                            i.[volume] = h.hist_volume
            WHEN NOT MATCHED
            THEN
              INSERT(symbol, 
                     [date], 
                     [open], 
                     [close], 
                     [high], 
                     [low], 
                     [volume])
              VALUES
        (
                     h.symbol, 
                     h.hist_date, 
                     h.hist_open, 
                     h.hist_close, 
                     h.hist_high, 
                     h.hist_low, 
                     h.hist_volume
        );

        --Update daily_close
        UPDATE dc
           SET dc.price = dh.[close]
          FROM daily_close dc, daily_history dh
         WHERE dc.symbol = 'IDX'
               AND dc.symbol = dh.symbol
               AND dc.date_hist = dh.[date];

        --Update SMA
        DECLARE 
               @sma     NVARCHAR(10), 
               @sma_day INT;
        DECLARE cur CURSOR
        FOR SELECT value AS sma
              FROM STRING_SPLIT('5,10,20,30,60,120,240', ',');
        OPEN cur;
        FETCH NEXT FROM cur INTO 
                                 @sma;
        WHILE @@fetch_status = 0
            BEGIN
                SET @sma_day = CAST(@sma AS INT);
                --process sma
                EXEC dbo.process_sma_detail @symbol = 'IDX', 
                                            @last_ran = '1/1/1900', 
                                            @table_name = 'daily_close', 
                                            @col_name = 'close', 
                                            @days = @sma_day;
                FETCH NEXT FROM cur INTO 
                                         @sma;
            END;
        CLOSE cur;
        DEALLOCATE cur;
    END;