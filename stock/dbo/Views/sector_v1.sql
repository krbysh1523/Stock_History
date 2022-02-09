CREATE VIEW sector_v1
AS
     SELECT a.sector, 
            a.date_hist, 
            a.price_chg, 
            a.idx_chg, 
            a.strong, 
            ROUND(AVG(a.strong) OVER(PARTITION BY a.sector
            ORDER BY a.date_hist ROWS BETWEEN 4 PRECEDING AND CURRENT ROW), 4) AS strong_sma5
     FROM
     (
         SELECT wl.sector, 
                ds.date_hist, 
                ROUND(AVG(ds.price_chg_perc), 4) AS price_chg, 
                ROUND(MIN(ds.idx_chg), 4) AS idx_chg, 
                ROUND(AVG(ds.price_chg_perc) - MIN(ds.idx_chg), 4) AS strong
         FROM dbo.daily_strong ds, 
              dbo.watch_list wl
         WHERE ds.symbol = wl.symbol
               AND wl.watch_type = 'R'
         GROUP BY wl.sector, 
                  ds.date_hist
     ) a;