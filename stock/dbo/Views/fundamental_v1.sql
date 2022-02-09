





CREATE VIEW [dbo].[fundamental_v1]
AS
     WITH earning_v
          AS (SELECT a.symbol, 
                     a.date_fiscal, 
                     a.date_hist, 
                     a.surprise, 
                     a.eps, 
                     a.est,
                     CASE
                         WHEN a.pre_eps = 0
                         THEN 0 ELSE ROUND((a.eps - a.pre_eps) / ABS(a.pre_eps) * 100, 2)
                     END AS perc_eps,
                     CASE
                         WHEN a.pre_est = 0
                         THEN 0 ELSE ROUND((a.est - a.pre_est) / ABS(a.pre_est) * 100, 2)
                     END AS perc_est
              FROM
              (
                  SELECT e.symbol, 
                         e.fiscaldateending AS date_fiscal, 
                         ISNULL(e.reporteddate, e.fiscaldateending) AS date_hist, 
                         e.reportedeps AS eps, 
                         LAG(e.reportedeps, 1, e.reportedeps) OVER(PARTITION BY e.symbol
                         ORDER BY e.fiscaldateending) AS pre_eps, 
                         e.estimatedeps AS est, 
                         LAG(e.reportedeps, 1, e.reportedeps) OVER(PARTITION BY e.symbol
                         ORDER BY e.fiscaldateending) AS pre_est, 
                         e.surprisepercentage AS surprise, 
                         LAG(e.reportedeps, 1, e.reportedeps) OVER(PARTITION BY e.symbol
                         ORDER BY e.fiscaldateending) AS pre_surprise
                  FROM dbo.earning e
              ) a),
          income_v
          AS (SELECT a.symbol, 
                     a.date_fiscal,
                     CASE
                         WHEN a.pre_totalincome = 0
                         THEN 0 ELSE ROUND((a.totalincome - a.pre_totalincome) / a.pre_totalincome * 100, 2)
                     END AS perc_total,
                     CASE
                         WHEN a.pre_after_expense = 0
                         THEN 0 ELSE ROUND((a.totalincome - a.pre_after_expense) / a.pre_after_expense * 100, 2)
                     END AS perc_after_expense,
                     CASE
                         WHEN a.pre_pure_profit = 0
                         THEN 0 ELSE ROUND((a.totalincome - a.pre_pure_profit) / a.pre_pure_profit * 100, 2)
                     END AS perc_profit
              FROM
              (
                  SELECT i.symbol, 
                         i.fiscaldateending AS date_fiscal, 
                         i.totalrevenue AS totalincome, 
                         LAG(i.totalrevenue, 1, i.totalrevenue) OVER(PARTITION BY i.symbol
                         ORDER BY i.fiscaldateending) AS pre_totalincome, 
                         i.grossprofit AS after_expense, 
                         LAG(i.grossprofit, 1, i.grossprofit) OVER(PARTITION BY i.symbol
                         ORDER BY i.fiscaldateending) AS pre_after_expense, 
                         i.netincome AS pure_profit, 
                         LAG(i.netincome, 1, i.netincome) OVER(PARTITION BY i.symbol
                         ORDER BY i.fiscaldateending) AS pre_pure_profit
                  FROM income i
              ) a),
          price
          AS (SELECT ds.symbol, 
                     ds.[date] AS date_hist, 
                     ds.[close] AS org_price, 
                     LAG(ds.adjusted_close, 30, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_pre_30, 
                     LAG(ds.adjusted_close, 20, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_pre_20, 
                     LAG(ds.adjusted_close, 10, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_pre_10, 
                     LAG(ds.adjusted_close, 5, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_pre_5, 
                     LAG(ds.adjusted_close, 3, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_pre_3, 
                     LAG(ds.adjusted_close, 1, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_pre_1, 
                     ds.adjusted_close AS price, 
                     LEAD(ds.adjusted_close, 1, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_next_1, 
                     LEAD(ds.adjusted_close, 3, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_next_3, 
                     LEAD(ds.adjusted_close, 5, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_next_5, 
                     LEAD(ds.adjusted_close, 10, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_next_10, 
                     LEAD(ds.adjusted_close, 20, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_next_20, 
                     LEAD(ds.adjusted_close, 30, -9999) OVER(PARTITION BY ds.symbol
                     ORDER BY ds.[date]) AS price_next_30
              FROM dbo.daily_source ds)
          SELECT e.symbol, 
                 e.date_hist, 
                 e.date_fiscal, 
                 e.eps, 
                 e.perc_eps, 
                 ROUND(AVG(e.perc_eps) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 3 PRECEDING AND CURRENT ROW), 2) AS perc_eps_1year, 
                 ROUND(AVG(e.perc_eps) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 11 PRECEDING AND CURRENT ROW), 2) AS perc_eps_3year, 
                 e.est, 
                 e.perc_est, 
                 ROUND(AVG(e.perc_est) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 3 PRECEDING AND CURRENT ROW), 2) AS perc_est_1year, 
                 ROUND(AVG(e.perc_est) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 11 PRECEDING AND CURRENT ROW), 2) AS perc_est_3year, 
                 e.surprise, 
                 ROUND(AVG(e.surprise) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 3 PRECEDING AND CURRENT ROW), 2) AS surprise_1year, 
                 ROUND(AVG(e.surprise) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 11 PRECEDING AND CURRENT ROW), 2) AS surprise_3year, 
                 i.perc_after_expense, 
                 ROUND(AVG(i.perc_after_expense) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 3 PRECEDING AND CURRENT ROW), 2) AS perc_after_expense_1year, 
                 ROUND(AVG(i.perc_after_expense) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 11 PRECEDING AND CURRENT ROW), 2) AS perc_after_expense_3year, 
                 i.perc_profit, 
                 ROUND(AVG(i.perc_profit) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 3 PRECEDING AND CURRENT ROW), 2) AS perc_profit_1year, 
                 ROUND(AVG(i.perc_profit) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 11 PRECEDING AND CURRENT ROW), 2) AS perc_profit_3year, 
                 i.perc_total, 
                 ROUND(AVG(i.perc_total) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 3 PRECEDING AND CURRENT ROW), 2) AS perc_total_1year, 
                 ROUND(AVG(i.perc_total) OVER(PARTITION BY e.symbol
                 ORDER BY e.date_hist ROWS BETWEEN 11 PRECEDING AND CURRENT ROW), 2) AS perc_total_3year, 
                 ROUND(e.eps / p.org_price * 100, 2) AS eps_close_rate,
                 CASE
                     WHEN p.price_pre_30 = 0
                     THEN 0 ELSE ROUND((p.price_pre_30 - p.price) / p.price * 100, 2)
                 END AS pre_30,
                 CASE
                     WHEN p.price_pre_20 = 0
                     THEN 0 ELSE ROUND((p.price_pre_20 - p.price) / p.price * 100, 2)
                 END AS pre_20,
                 CASE
                     WHEN p.price_pre_10 = 0
                     THEN 0 ELSE ROUND((p.price_pre_10 - p.price) / p.price * 100, 2)
                 END AS pre_10,
                 CASE
                     WHEN p.price_pre_5 = 0
                     THEN 0 ELSE ROUND((p.price_pre_5 - p.price) / p.price * 100, 2)
                 END AS pre_5,
                 CASE
                     WHEN p.price_pre_3 = 0
                     THEN 0 ELSE ROUND((p.price_pre_3 - p.price) / p.price * 100, 2)
                 END AS pre_3,
                 CASE
                     WHEN p.price_pre_1 = 0
                     THEN 0 ELSE ROUND((p.price_pre_1 - p.price) / p.price * 100, 2)
                 END AS pre_1,
                 CASE
                     WHEN p.price_next_1 = 0
                     THEN 0 ELSE ROUND((p.price_next_1 - p.price) / p.price * 100, 2)
                 END AS next_1,
                 CASE
                     WHEN p.price_next_3 = 0
                     THEN 0 ELSE ROUND((p.price_next_3 - p.price) / p.price * 100, 2)
                 END AS next_3,
                 CASE
                     WHEN p.price_next_5 = 0
                     THEN 0 ELSE ROUND((p.price_next_5 - p.price) / p.price * 100, 2)
                 END AS next_5,
                 CASE
                     WHEN p.price_next_10 = 0
                     THEN 0 ELSE ROUND((p.price_next_10 - p.price) / p.price * 100, 2)
                 END AS next_10,
                 CASE
                     WHEN p.price_next_20 = 0
                     THEN 0 ELSE ROUND((p.price_next_20 - p.price) / p.price * 100, 2)
                 END AS next_20,
                 CASE
                     WHEN p.price_next_30 = 0
                     THEN 0 ELSE ROUND((p.price_next_30 - p.price) / p.price * 100, 2)
                 END AS next_30
          FROM earning_v e
               LEFT OUTER JOIN income_v i ON e.date_fiscal = i.date_fiscal
                                             AND e.symbol = i.symbol, 
               price p
          WHERE e.symbol = p.symbol
                AND e.date_hist = p.date_hist;


