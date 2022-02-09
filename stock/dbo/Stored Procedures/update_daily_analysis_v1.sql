
CREATE PROCEDURE [dbo].[update_daily_analysis_v1]
AS
    BEGIN
        DECLARE @sql NVARCHAR(MAX), @sql_select1 NVARCHAR(MAX), @sql_select2 NVARCHAR(MAX), @sql_select3 NVARCHAR(MAX), @sql_from NVARCHAR(MAX), @sql_where NVARCHAR(MAX), @table NVARCHAR(50), @col NVARCHAR(50), @prev_table NVARCHAR(50);

        DECLARE cur CURSOR
        FOR SELECT 
                   tab.name AS table_name, 
                   col.name AS column_name
            FROM sys.tables AS tab
                 INNER JOIN sys.columns AS col ON tab.object_id = col.object_id
            WHERE tab.name in ('daily_analysis', 'daily_volume', 'daily_avg')
                  AND col.name NOT IN('date', 'symbol')
            ORDER BY 
                     table_name, 
                     col.name;

        OPEN cur;
        FETCH NEXT FROM cur INTO @table, @col;
        WHILE @@fetch_status = 0
            BEGIN
                IF @sql_select1 IS NULL
                   OR LEN(@sql_select1) <= 3800
                    IF @col LIKE '%perc'
                        BEGIN
                            SELECT 
                                   @sql_select1 = concat(@sql_select1, 'round(', @table, '.[', @col, '] * 100, 1) as ', replace(@table, 'daily_', ''), '_', @col, ', ');
                    END;
                        ELSE
                        BEGIN
                            SELECT 
                                   @sql_select1 = concat(@sql_select1, @table, '.[', @col, '] as ', replace(@table, 'daily_', ''), '_', @col, ', ');
                    END;
                    ELSE
                    IF @sql_select2 IS NULL
                       OR LEN(@sql_select2) <= 3800

                        IF @col LIKE '%perc'
                            BEGIN
                                SELECT 
                                       @sql_select1 = concat(@sql_select1, 'round(', @table, '.[', @col, '] * 100, 1) as ', replace(@table, 'daily_', ''), '_', @col, ', ');
                        END;
                            ELSE
                            BEGIN
                                SELECT 
                                       @sql_select2 = concat(@sql_select2, @table, '.[', @col, '] as ', replace(@table, 'daily_', ''), '_', @col, ', ');
                        END;
                        ELSE
                        IF @sql_select3 IS NULL
                           OR LEN(@sql_select3) <= 3800
                            IF @col LIKE '%perc'
                                BEGIN
                                    SELECT 
                                           @sql_select1 = concat(@sql_select1, 'round(', @table, '.[', @col, '] * 100, 1) as ', replace(@table, 'daily_', ''), '_', @col, ', ');
                            END;
                                ELSE
                                BEGIN
                                    SELECT 
                                           @sql_select3 = concat(@sql_select3, @table, '.[', @col, '] as ', replace(@table, 'daily_', ''), '_', @col, ', ');
                            END;

                IF @table <> ISNULL(@prev_table, '')
                   AND @table <> 'daily_history'
                    BEGIN
                        SELECT 
                               @sql_from = concat(ISNULL(@sql_from, ''), ' left outer join ', @table);
                        SELECT 
                               @sql_from = concat(ISNULL(@sql_from, ''), ' on ', @table, '.', 'symbol = daily_history.symbol ', ' and ', @table, '.', '[date] = daily_history.[date] ');
                        SET @prev_table = @table;
                END;

                FETCH NEXT FROM cur INTO @table, @col;
            END;

        CLOSE cur;
        DEALLOCATE cur;

        SET @sql = N'alter view daily_analysis_v1 as select daily_history.[symbol], watch_list.watch_type, daily_history.[seq], daily_history.[date], daily_history.[open], daily_history.[close], daily_history.[high], daily_history.[low], daily_history.[volume], daily_history.[avg], ' + CONVERT(NVARCHAR(MAX), ISNULL(@sql_select1, '')) + CONVERT(NVARCHAR(MAX), ISNULL(@sql_select2, '')) + CONVERT(NVARCHAR(MAX), ISNULL(@sql_select3, '')) + N' getdate() as last_created' + ' from daily_history ' + CONVERT(NVARCHAR(MAX), @sql_from) + ', watch_list WHERE daily_history.symbol = watch_list.symbol ';


        EXEC sp_executesql 
             @sql;

    END;
