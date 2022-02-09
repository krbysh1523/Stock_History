CREATE PROCEDURE [dbo].[process_history](@symbol NVARCHAR(50))
AS
    BEGIN
        MERGE daily_history h
        USING
        (
            SELECT 
                   s.symbol, 
                   s.date, 
                   ROUND(s.[open] * (s.adjusted_close / s.[close]), 6) AS [open], 
                   ROUND(s.adjusted_close, 6) AS [close], 
                   ROUND(s.[high] * (s.adjusted_close / s.[close]), 6) AS [high], 
                   ROUND(s.[low] * (s.adjusted_close / s.[close]), 6) AS [low], 
                   s.volume, 
                   ROUND(((s.[open] * (s.adjusted_close / s.[close])) + s.adjusted_close) / 2, 6) AS [avg]
            FROM daily_source s
            WHERE s.symbol = @symbol
        ) u
        ON h.symbol = u.symbol
           AND h.[date] = u.[date]
            WHEN NOT MATCHED
            THEN
              INSERT(
                     symbol, 
                     [date], 
                     [open], 
                     [close], 
                     [high], 
                     [low], 
                     [avg], 
                     [volume])
              VALUES
        (
             u.symbol, 
             u.[date], 
             u.[open], 
             u.[close], 
             u.[high], 
             u.[low], 
             u.[avg], 
             u.[volume]
        );

    END;