CREATE PROCEDURE [dbo].[process_analysis](@symbol NVARCHAR(50))
AS
    BEGIN

        MERGE daily_analysis a
        USING
        (
            SELECT 
                   h.symbol, 
                   h.[date],
                   CASE
                       WHEN h.[open] <> 0
                       THEN ABS((h.[close] - h.[open]) / h.[open])
                       ELSE 0
                   END AS open_close_perc,
                   CASE
                       WHEN h.[low] <> 0
                       THEN(h.[high] - h.[low]) / h.[low]
                       ELSE 0
                   END AS high_low_perc,
                   CASE
                       WHEN h.[close] >= h.[open]
                       THEN 'U'
                       ELSE 'D'
                   END AS dir,
                   CASE
                       WHEN(h.[high] - h.[low]) <> 0
                       THEN ABS(h.[close] - h.[open]) / (h.[high] - h.[low])
                       ELSE 0
                   END AS oc_by_hl_perc
            FROM daily_history h
            WHERE h.symbol = @symbol
        ) u
        ON u.symbol = a.symbol
           AND u.[date] = a.[date]
            WHEN MATCHED
            THEN UPDATE SET 
                            a.open_close_perc = u.open_close_perc, 
                            a.high_low_perc = u.high_low_perc, 
                            a.dir = u.dir, 
                            a.oc_by_hl_perc = u.oc_by_hl_perc
            WHEN NOT MATCHED
            THEN
              INSERT(
                     symbol, 
                     [date], 
                     open_close_perc, 
                     high_low_perc, 
                     dir, 
                     oc_by_hl_perc)
              VALUES
        (
             u.symbol, 
             u.[date], 
             u.open_close_perc, 
             u.high_low_perc, 
             u.dir, 
             u.oc_by_hl_perc
        );

        --Update Direction ON
        --DECLARE cur CURSOR
        --FOR SELECT 
        --           a.[date], 
        --           a.dir
        --    FROM daily_analysis a
        --    WHERE a.symbol = @symbol
        --    ORDER BY 
        --             a.[date];

        --DECLARE @date DATE, @dir CHAR(1), @prev_dir CHAR(1)= 'N', @prev_cnt INT= 1;

        --OPEN cur;
        --FETCH NEXT FROM cur INTO @date, @dir;
        --WHILE @@fetch_status = 0
        --    BEGIN
        --        IF @dir <> @prev_dir
        --            BEGIN
        --                IF @dir = 'D'
        --                    SET @prev_cnt = -1;
        --                    ELSE
        --                    SET @prev_cnt = 1;

        --                UPDATE a
        --                  SET 
        --                      a.dir_on = @prev_cnt
        --                FROM daily_analysis a
        --                WHERE 
        --                      a.symbol = @symbol
        --                      AND a.[date] = @date;

        --                SET @prev_dir = @dir;
        --        END;
        --            ELSE
        --            BEGIN
        --                IF @dir = 'D'
        --                    SET @prev_cnt = @prev_cnt - 1;
        --                    ELSE
        --                    SET @prev_cnt = @prev_cnt + 1;

        --                UPDATE a
        --                  SET 
        --                      a.dir_on = @prev_cnt
        --                FROM daily_analysis a
        --                WHERE 
        --                      a.symbol = @symbol
        --                      AND a.[date] = @date;
        --        END;

        --        FETCH NEXT FROM cur INTO @date, @dir;
        --    END;
        --CLOSE cur;
        --DEALLOCATE cur;
    END;