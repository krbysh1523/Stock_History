
CREATE VIEW [dbo].[daily_candle_v1]
AS
     SELECT aa.symbol, 
            aa.date_hist, 
            aa.direction,
            CASE
                WHEN aa.size_sma240 <= 40
                THEN 'Doji'
                WHEN aa.size_sma240 >= 160
                THEN 'Long' ELSE 'Normal'
            END size,
            CASE
                WHEN aa.head >= 60
                THEN 'Head'
                WHEN aa.tail >= 60
                THEN 'Tail'
                WHEN aa.center >= 80
                THEN 'Full' ELSE 'Normal'
            END candle
     FROM
     (
         SELECT a.symbol, 
                a.date_hist, 
                a.direction, 
                a.head, 
                a.center, 
                a.tail,
                CASE
                    WHEN a.range_avg_5 = 0
                    THEN 0 ELSE ROUND(a.range_day / a.range_avg_5 * 100, 2)
                END AS size_sma5,
                CASE
                    WHEN a.range_avg_10 = 0
                    THEN 0 ELSE ROUND(a.range_day / a.range_avg_10 * 100, 2)
                END AS size_sma10,
                CASE
                    WHEN a.range_avg_20 = 0
                    THEN 0 ELSE ROUND(a.range_day / a.range_avg_20 * 100, 2)
                END AS size_sma20,
                CASE
                    WHEN a.range_avg_30 = 0
                    THEN 0 ELSE ROUND(a.range_day / a.range_avg_30 * 100, 2)
                END AS size_sma30,
                CASE
                    WHEN a.range_avg_60 = 0
                    THEN 0 ELSE ROUND(a.range_day / a.range_avg_60 * 100, 2)
                END AS size_sma60,
                CASE
                    WHEN a.range_avg_120 = 0
                    THEN 0 ELSE ROUND(a.range_day / a.range_avg_120 * 100, 2)
                END AS size_sma120,
                CASE
                    WHEN a.range_avg_240 = 0
                    THEN 0 ELSE ROUND(a.range_day / a.range_avg_240 * 100, 2)
                END AS size_sma240
         FROM
         (
             SELECT dh.symbol, 
                    dh.[date] AS date_hist,
                    CASE
                        WHEN dh.[open] < dh.[close]
                        THEN 'Up'
                        WHEN dh.[open] = dh.[close]
                        THEN 'Even' ELSE 'Down'
                    END direction, 
                    (dh.[high] - dh.[low]) AS range_day,
                    CASE
                        WHEN(dh.[high] - dh.[low]) = 0
                        THEN 0 ELSE ROUND(ABS(dh.[high] - dbo.fx_greater(dh.[open], dh.[close])) / (dh.[high] - dh.[low]) * 100, 2)
                    END AS head,
                    CASE
                        WHEN(dh.[high] - dh.[low]) = 0
                        THEN 0 ELSE ROUND(ABS(dh.[open] - dh.[close]) / (dh.[high] - dh.[low]) * 100, 2)
                    END AS center,
                    CASE
                        WHEN(dh.[high] - dh.[low]) = 0
                        THEN 0 ELSE ROUND(ABS(dbo.fx_lower(dh.[open], dh.[close]) - dh.[low]) / (dh.[high] - dh.[low]) * 100, 2)
                    END AS tail, 
                    AVG(dh.[high] - dh.[low]) OVER(PARTITION BY dh.symbol
         ORDER BY dh.[date] ROWS BETWEEN 4 PRECEDING AND CURRENT ROW) AS range_avg_5, 
                    AVG(dh.[high] - dh.[low]) OVER(PARTITION BY dh.symbol
         ORDER BY dh.[date] ROWS BETWEEN 9 PRECEDING AND CURRENT ROW) AS range_avg_10, 
                    AVG(dh.[high] - dh.[low]) OVER(PARTITION BY dh.symbol
         ORDER BY dh.[date] ROWS BETWEEN 19 PRECEDING AND CURRENT ROW) AS range_avg_20, 
                    AVG(dh.[high] - dh.[low]) OVER(PARTITION BY dh.symbol
         ORDER BY dh.[date] ROWS BETWEEN 29 PRECEDING AND CURRENT ROW) AS range_avg_30, 
                    AVG(dh.[high] - dh.[low]) OVER(PARTITION BY dh.symbol
         ORDER BY dh.[date] ROWS BETWEEN 59 PRECEDING AND CURRENT ROW) AS range_avg_60, 
                    AVG(dh.[high] - dh.[low]) OVER(PARTITION BY dh.symbol
         ORDER BY dh.[date] ROWS BETWEEN 119 PRECEDING AND CURRENT ROW) AS range_avg_120, 
                    AVG(dh.[high] - dh.[low]) OVER(PARTITION BY dh.symbol
         ORDER BY dh.[date] ROWS BETWEEN 239 PRECEDING AND CURRENT ROW) AS range_avg_240
             FROM dbo.daily_history AS dh
         ) a
     ) aa;
