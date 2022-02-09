

-- Alter View daily_sma5_v1


CREATE VIEW [dbo].[daily_sma5_v1]
AS
     SELECT h1.symbol, 
			h1.[date] AS date_hist,
            h1.price, 
            h1.price_pre, 
            ROUND(((h1.price - h1.price_pre) / h1.price_pre) * 100, 2) AS price_chg_perc, 
            h1.sma5, 
            h1.sma5_pre, 
            ROUND(((h1.sma5 - h1.sma5_pre) / h1.sma5_pre) * 100, 2) AS sma5_chg_perc
     FROM
     (
         SELECT h.symbol, 
                h.[date], 
                h.[close] AS price, 
                LAG(h.[close], 1, NULL) OVER(PARTITION BY h.symbol
                ORDER BY h.[date]) AS price_pre, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 4 PRECEDING AND CURRENT ROW) AS sma5, 
                AVG(h.[close]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 5 PRECEDING AND 1 PRECEDING) AS sma5_pre
         FROM daily_history h
     ) h1;
