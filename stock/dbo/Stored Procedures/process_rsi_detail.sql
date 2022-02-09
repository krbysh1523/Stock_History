CREATE PROCEDURE [dbo].[process_rsi_detail]
(@symbol     NVARCHAR(10), 
 @last_ran   DATE, 
 @table_name NVARCHAR(50), 
 @col_name   NVARCHAR(50), 
 @days       INT          = 14
)
AS
    BEGIN
        DECLARE @sql NVARCHAR(MAX), @new_col_name NVARCHAR(50);
        SET @new_col_name = 'RSI_' + format(@days, '000');

        SET @sql = 'MERGE ' + @table_name + ' AS VOL
        USING
        (
            SELECT 
                   A.SYMBOL, 
                   A.DATE, 
                   (100 - (100 / (1 + (CASE
                                           WHEN(AVG(LOSS) OVER(
                                               ORDER BY 
                                                        [DATE] ROWS ' + CAST(@days AS NVARCHAR(10)) + ' PRECEDING)) = 0
                                           THEN 0
                                           ELSE(AVG(GAIN) OVER(
                                               ORDER BY 
                                                        [DATE] ROWS ' + CAST(@days AS NVARCHAR(10)) + ' PRECEDING)) / (AVG(LOSS) OVER(
                                               ORDER BY 
                                                        [DATE] ROWS ' + CAST(@days AS NVARCHAR(10)) + ' PRECEDING))
                                       END)))) RSI
            FROM
            (
                SELECT 
                       @symbol AS SYMBOL, 
                       [DATE],
                       CASE
                           WHEN([' + @col_name + '] - LAG([' + @col_name + ']) OVER(
                                ORDER BY 
                                         [DATE])) > 0
                           THEN([' + @col_name + '] - LAG([' + @col_name + ']) OVER(
                                ORDER BY 
                                         [DATE]))
                           ELSE 0
                       END GAIN,
                       CASE
                           WHEN([' + @col_name + '] - LAG([' + @col_name + ']) OVER(
                                ORDER BY 
                                         [DATE])) < 0
                           THEN ABS([' + @col_name + '] - LAG([' + @col_name + ']) OVER(
                                    ORDER BY 
                                             [DATE]))
                           ELSE 0
                       END LOSS
                FROM DAILY_HISTORY
                WHERE SYMBOL = @symbol
                      AND [DATE] >= @last_ran
            ) A
        ) RSI
		ON VOL.[DATE] = RSI.[DATE]
           AND VOL.SYMBOL = RSI.SYMBOL
            WHEN MATCHED
            THEN UPDATE SET ' + @new_col_name + ' = ISNULL(RSI.RSI, VOL.' + @new_col_name + ')
            WHEN NOT MATCHED
            THEN
              INSERT(
                     SYMBOL, 
                     [DATE], 
                     ' + @new_col_name + ')
              VALUES
        (
             RSI.SYMBOL, 
             RSI.[DATE], 
             RSI.RSI
        );';

        EXEC sp_executesql 
             @sql, 
             N'@symbol nvarchar(50), @last_ran date', 
             @symbol = @symbol, 
             @last_ran = @last_ran;

    END;