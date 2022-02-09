CREATE PROCEDURE [dbo].[option_11]
(
     @start_dttm   DATETIME, 
     @end_dttm     DATETIME, 
     @passing_list NVARCHAR(1), 
     @att1         NVARCHAR(250), --Week
     @att2         NVARCHAR(250), --Low
     @att3         NVARCHAR(250), --High
     @att4         NVARCHAR(250), 
     @att5         NVARCHAR(250)
)
AS
    BEGIN

        --*****************************************
        --마지막 날 기준으로 지난 n주의 최하와 최고점 사이 (0~100%)
        --*****************************************
        IF @passing_list = 'Y'
            BEGIN
                --Reset Ranking
                UPDATE prediction
                   SET prediction.ranking = NULL;

                --Update
                WITH symbols
                     AS (SELECT c.symbol, 
                                c.rate, 
                                RANK() OVER(
                                ORDER BY c.rate DESC) AS ranking
                           FROM
                         (
                             SELECT a.symbol, 
                                    ((b.price - a.low_p) / (a.high_p - a.low_p) * 100) AS rate
                               FROM
                             (
                                 SELECT ds.symbol, 
                                        MAX(ds.price) AS high_p, 
                                        MIN(ds.price) AS low_p
                                   FROM daily_strong ds, 
                                        dbo.prediction p
                                  WHERE ds.date_hist >= DATEADD(week, -CAST(@att1 AS INT), @end_dttm)
                                        AND ds.date_hist <= @end_dttm
                                        AND ds.symbol = p.symbol
                                  GROUP BY ds.symbol
                             ) a, 
                             (
                                 SELECT ds.symbol, 
                                        ds.price
                                   FROM daily_strong ds, 
                                        dbo.prediction p
                                  WHERE ds.date_hist = @end_dttm
                                        AND ds.symbol = p.symbol
                             ) b
                              WHERE a.symbol = b.symbol
                                    AND ((b.price - a.low_p) / (a.high_p - a.low_p) * 100) >= CAST(@att2 AS FLOAT)
                                    AND ((b.price - a.low_p) / (a.high_p - a.low_p) * 100) <= CAST(@att3 AS FLOAT)
                                    AND (a.high_p - a.low_p) > 0
                         ) c)
                     UPDATE p
                        SET p.ranking = s.ranking, 
                            p.ratio1 = s.rate
                       FROM prediction p, symbols s
                      WHERE p.symbol = s.symbol;

                --Delete Not in Limit
                DELETE prediction
                 WHERE prediction.ranking IS NULL;
        END;
            ELSE
            BEGIN
                WITH symbols
                     AS (SELECT c.symbol, 
                                c.rate, 
                                RANK() OVER(
                                ORDER BY c.rate DESC) AS ranking
                           FROM
                         (
                             SELECT a.symbol, 
                                    ((b.price - a.low_p) / (a.high_p - a.low_p) * 100) AS rate
                               FROM
                             (
                                 SELECT ds.symbol, 
                                        MAX(ds.price) AS high_p, 
                                        MIN(ds.price) AS low_p
                                   FROM daily_strong ds
                                  WHERE ds.date_hist >= DATEADD(week, -CAST(@att1 AS INT), @end_dttm)
                                        AND ds.date_hist <= @end_dttm
                                  GROUP BY ds.symbol
                             ) a, 
                             (
                                 SELECT ds.symbol, 
                                        ds.price
                                   FROM daily_strong ds
                                  WHERE ds.date_hist = @end_dttm
                             ) b
                              WHERE a.symbol = b.symbol
                                    AND ((b.price - a.low_p) / (a.high_p - a.low_p) * 100) >= CAST(@att2 AS FLOAT)
                                    AND ((b.price - a.low_p) / (a.high_p - a.low_p) * 100) <= CAST(@att3 AS FLOAT)
                                    AND (a.high_p - a.low_p) > 0
                         ) c)
                     INSERT INTO prediction
                     (dbo.prediction.symbol, 
                      dbo.prediction.ranking, 
                      dob.prediction.ratio1
                     )
                     SELECT s.symbol, 
                            s.ranking, 
                            s.rate
                       FROM symbols s;
        END;
    END;