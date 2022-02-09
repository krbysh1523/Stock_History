
CREATE VIEW [dbo].[watch_list_v1]
AS
     SELECT w.symbol, 
            w.watch_type, 
            w.company, 
            w.industry, 
            w.sector, 
     (
         SELECT MAX(ds.[date])
         FROM dbo.daily_source ds
         WHERE w.symbol = ds.symbol
     ) AS last_updated, 
     (
         SELECT MAX(e.fiscalDateEnding)
         FROM dbo.earning e
         WHERE w.symbol = e.symbol
     ) AS last_earning
     FROM watch_list w; 
