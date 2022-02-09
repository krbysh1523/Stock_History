
















CREATE VIEW [dbo].[daily_strong_v1]
AS
     WITH ind
          AS (SELECT s.date_hist, 
                     s.s_price, 
                     s.s_chg, 
                     s.s_sma5, 
                     q.q_price, 
                     q.q_chg, 
                     q.q_sma5, 
                     d.d_price, 
                     d.d_chg, 
                     d.d_sma5
              FROM
              (
                  SELECT 'SPY' AS i_spy, 
                         s.date_hist, 
                         s.price AS s_price, 
                         s.price_chg_perc AS s_chg, 
                         s.sma5 AS s_sma5
                  FROM daily_sma5 s
                  WHERE s.symbol = 'SPY'
              ) s, 
              (
                  SELECT 'QQQ' AS i_spy, 
                         s.date_hist, 
                         s.price AS q_price, 
                         s.price_chg_perc AS q_chg, 
                         s.sma5 AS q_sma5
                  FROM daily_sma5 s
                  WHERE s.symbol = 'QQQ'
              ) q, 
              (
                  SELECT 'DIA' AS i_spy, 
                         s.date_hist, 
                         s.price AS d_price, 
                         s.price_chg_perc AS d_chg, 
                         s.sma5 AS d_sma5
                  FROM daily_sma5 s
                  WHERE s.symbol = 'DIA'
              ) d
              WHERE s.date_hist = q.date_hist
                    AND s.date_hist = d.date_hist)
          SELECT s.symbol, 
                 s.date_hist, 
                 s.price, 
                 s.price_chg_perc, 
				 s.sma5,
                 s.sma5_chg_perc, 
                 i.s_price AS spy_price, 
                 i.s_chg AS spy_chg, 
                 i.s_sma5 AS spy_sma5, 
                 (s.price_chg_perc - ISNULL(i.s_chg, 0)) AS spy_diff, 
                 (s.sma5_chg_perc - ISNULL(i.s_sma5, 0)) AS spy_diff_sma5, 
                 i.q_price AS qqq_price, 
                 i.q_chg AS qqq_chg, 
                 i.q_sma5 AS qqq_sma5, 
                 (s.price_chg_perc - ISNULL(i.q_chg, 0)) AS qqq_diff, 
                 (s.sma5_chg_perc - ISNULL(i.q_sma5, 0)) AS qqq_diff_sma5, 
                 i.d_price AS dia_price, 
                 i.d_chg AS dia_chg, 
                 i.d_sma5 AS dia_sma5, 
                 (s.price_chg_perc - ISNULL(i.d_chg, 0)) AS dia_diff, 
                 (s.sma5_chg_perc - ISNULL(i.d_sma5, 0)) AS dia_diff_sma5, 
                 ROUND(((i.s_price + i.q_price + i.d_price) / 3), 4) AS idx_price, 
                 ROUND(((i.s_sma5 + i.q_sma5 + i.d_sma5) / 3), 4) AS idx_sma5, 
                 ROUND(((i.s_chg + q_chg + d_chg) / 3), 4) AS idx_chg, 
                 ROUND(s.price_chg_perc - ((i.s_chg + q_chg + d_chg) / 3), 4) AS idx_diff, 
                 ROUND(s.sma5_chg_perc - ((i.s_sma5 + q_sma5 + d_sma5) / 3), 4) AS idx_diff_sma5
          FROM ind i, 
               daily_sma5 s, 
               watch_list w
          WHERE s.symbol = w.symbol
                AND i.date_hist = s.date_hist;


