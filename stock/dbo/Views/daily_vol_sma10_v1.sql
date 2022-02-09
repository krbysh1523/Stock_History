


-- Alter View daily_vol_sma10_v1


CREATE VIEW [dbo].[daily_vol_sma10_v1]
AS
     SELECT h1.symbol, 
			h1.[date] AS date_hist,
            h1.volume, 
            h1.volume_pre,             
            case when h1.volume_pre is null or h1.volume_pre = 0 then null
			else ROUND(((h1.volume - h1.volume_pre) / h1.volume_pre) * 100, 2) end AS volume_chg_perc, 
            h1.sma10, 
            h1.sma10_pre, 
            case when h1.sma10_pre is null or h1.sma10_pre = 0 then null
            else ROUND(((h1.sma10 - h1.sma10_pre) / h1.sma10_pre) * 100, 2) end AS sma10_chg_perc
     FROM
     (
         SELECT h.symbol, 
                h.[date], 
                h.[volume] AS volume, 
                LAG(h.[volume], 1, NULL) OVER(PARTITION BY h.symbol
                ORDER BY h.[date]) AS volume_pre, 
                AVG(h.[volume]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 9 PRECEDING AND CURRENT ROW) AS sma10, 
                AVG(h.[volume]) OVER(PARTITION BY h.symbol
                ORDER BY h.[date] ROWS BETWEEN 10 PRECEDING AND 1 PRECEDING) AS sma10_pre
         FROM daily_history h
     ) h1;
