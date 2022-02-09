CREATE PROCEDURE [dbo].[process_sma_detail]
(@symbol     NVARCHAR(10), 
 @last_ran   DATE, 
 @table_name NVARCHAR(50), 
 @col_name   NVARCHAR(50), 
 @days       INT
)
AS
    BEGIN

        DECLARE @sql NVARCHAR(MAX), @sma_col_name NVARCHAR(50);
        SET @sma_col_name = 'sma_' + format(@days, '000');

        SET @sql = 'MERGE ' + @table_name + ' AS VOL
        USING
        (
            SELECT 
                   @symbol AS SYMBOL, 
                   [DATE], 
                   AVG([' + @col_name + ']) OVER(
                   ORDER BY 
                            [DATE] ROWS BETWEEN ' + CAST((@days - 1) AS NVARCHAR(10)) + ' PRECEDING AND CURRENT ROW) AS SMA
            FROM DAILY_HISTORY
            WHERE SYMBOL = @symbol
                  AND [DATE] >= @last_ran
        ) SMA
        ON VOL.DATE_HIST = SMA.[DATE]
           AND VOL.SYMBOL = SMA.SYMBOL
            WHEN MATCHED
            THEN UPDATE SET 
                            ' + @sma_col_name + ' = SMA.SMA
            WHEN NOT MATCHED
            THEN
              INSERT(
                     SYMBOL, 
                     DATE_HIST, 
                     ' + @sma_col_name + ')
              VALUES
        (
             SMA.SYMBOL, 
             SMA.[DATE], 
             SMA.SMA
        );';

        EXEC sp_executesql 
             @sql, 
             N'@symbol nvarchar(50), @last_ran date', 
             @symbol = @symbol, 
             @last_ran = @last_ran;

    END;