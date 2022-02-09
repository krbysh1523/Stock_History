CREATE VIEW [dbo].[daily_volume_v1]
AS
     SELECT h1.symbol, 
			h1.[date] AS date_hist,
            h1.volume, 
            h1.sma_005, 
            h1.sma_010, 
            h1.sma_020, 
            h1.sma_030, 
            h1.sma_060,
            h1.sma_120, 
            h1.sma_240
     FROM
     (
         SELECT h.symbol, 
                h.[date], 
                h.volume, 
                AVG(h.volume) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 4 PRECEDING AND CURRENT ROW ) AS sma_005, 
                AVG(h.volume) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 9 PRECEDING AND CURRENT ROW) AS sma_010, 
                AVG(h.volume) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 19 PRECEDING AND CURRENT ROW) AS sma_020, 
                AVG(h.volume) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 29 PRECEDING AND CURRENT ROW) AS sma_030, 
                AVG(h.volume) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 59 PRECEDING AND CURRENT ROW) AS sma_060, 
                AVG(h.volume) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 119 PRECEDING AND CURRENT ROW) AS sma_120, 
                AVG(h.volume) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 239 PRECEDING AND CURRENT ROW) AS sma_240
         FROM daily_history h
     ) h1;
