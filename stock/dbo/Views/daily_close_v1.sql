


-- Alter View daily_sma5_v1


CREATE VIEW [dbo].[daily_close_v1]
AS
     SELECT h1.symbol, 
            h1.[date] AS date_hist, 
            h1.price, 
            h1.sma_005, 
            h1.sma_010, 
            h1.sma_020, 
            h1.sma_030, 
            h1.sma_060, 
            h1.sma_120, 
            h1.sma_240, 
            h1.direction, 
            h1.head, 
            h1.center, 
            h1.tail,
            CASE
                WHEN h1.head >= 60
                THEN 'Head'
                WHEN h1.tail >= 60
                THEN 'Tail'
                WHEN h1.center >= 80
                THEN 'Full' ELSE 'Normal'
            END candle,
            CASE
                WHEN h1.range_avg_5 = 0
                THEN 0 ELSE ROUND(h1.range_day / h1.range_avg_5 * 100, 2)
            END AS size_sma5,
            CASE
                WHEN h1.range_avg_10 = 0
                THEN 0 ELSE ROUND(h1.range_day / h1.range_avg_10 * 100, 2)
            END AS size_sma10,
            CASE
                WHEN h1.range_avg_20 = 0
                THEN 0 ELSE ROUND(h1.range_day / h1.range_avg_20 * 100, 2)
            END AS size_sma20,
            CASE
                WHEN h1.range_avg_30 = 0
                THEN 0 ELSE ROUND(h1.range_day / h1.range_avg_30 * 100, 2)
            END AS size_sma30,
            CASE
                WHEN h1.range_avg_60 = 0
                THEN 0 ELSE ROUND(h1.range_day / h1.range_avg_60 * 100, 2)
            END AS size_sma60,
            CASE
                WHEN h1.range_avg_120 = 0
                THEN 0 ELSE ROUND(h1.range_day / h1.range_avg_120 * 100, 2)
            END AS size_sma120,
            CASE
                WHEN h1.range_avg_240 = 0
                THEN 0 ELSE ROUND(h1.range_day / h1.range_avg_240 * 100, 2)
            END AS size_sma240
     FROM
     (
         SELECT h.symbol, 
                h.[date], 
                h.[close] AS price, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 4 PRECEDING AND CURRENT ROW) AS sma_005, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 9 PRECEDING AND CURRENT ROW) AS sma_010, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 19 PRECEDING AND CURRENT ROW) AS sma_020, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 29 PRECEDING AND CURRENT ROW) AS sma_030, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 59 PRECEDING AND CURRENT ROW) AS sma_060, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 119 PRECEDING AND CURRENT ROW) AS sma_120, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 239 PRECEDING AND CURRENT ROW) AS sma_240,
                CASE
                    WHEN h.[open] < h.[close]
                    THEN 'Up'
                    WHEN h.[open] = h.[close]
                    THEN 'Even' ELSE 'Down'
                END direction, 
                (h.[high] - h.[low]) AS range_day,
                CASE
                    WHEN(h.[high] - h.[low]) = 0
                    THEN 0 ELSE ROUND(ABS(h.[high] - dbo.fx_greater(h.[open], h.[close])) / (h.[high] - h.[low]) * 100, 2)
                END AS head,
                CASE
                    WHEN(h.[high] - h.[low]) = 0
                    THEN 0 ELSE ROUND(ABS(h.[open] - h.[close]) / (h.[high] - h.[low]) * 100, 2)
                END AS center,
                CASE
                    WHEN(h.[high] - h.[low]) = 0
                    THEN 0 ELSE ROUND(ABS(dbo.fx_lower(h.[open], h.[close]) - h.[low]) / (h.[high] - h.[low]) * 100, 2)
                END AS tail, 
                AVG(h.[high] - h.[low]) OVER(PARTITION BY h.symbol
     ORDER BY h.[date] ROWS BETWEEN 4 PRECEDING AND CURRENT ROW) AS range_avg_5, 
                AVG(h.[high] - h.[low]) OVER(PARTITION BY h.symbol
     ORDER BY h.[date] ROWS BETWEEN 9 PRECEDING AND CURRENT ROW) AS range_avg_10, 
                AVG(h.[high] - h.[low]) OVER(PARTITION BY h.symbol
     ORDER BY h.[date] ROWS BETWEEN 19 PRECEDING AND CURRENT ROW) AS range_avg_20, 
                AVG(h.[high] - h.[low]) OVER(PARTITION BY h.symbol
     ORDER BY h.[date] ROWS BETWEEN 29 PRECEDING AND CURRENT ROW) AS range_avg_30, 
                AVG(h.[high] - h.[low]) OVER(PARTITION BY h.symbol
     ORDER BY h.[date] ROWS BETWEEN 59 PRECEDING AND CURRENT ROW) AS range_avg_60, 
                AVG(h.[high] - h.[low]) OVER(PARTITION BY h.symbol
     ORDER BY h.[date] ROWS BETWEEN 119 PRECEDING AND CURRENT ROW) AS range_avg_120, 
                AVG(h.[high] - h.[low]) OVER(PARTITION BY h.symbol
     ORDER BY h.[date] ROWS BETWEEN 239 PRECEDING AND CURRENT ROW) AS range_avg_240
         FROM daily_history h
     ) h1;
