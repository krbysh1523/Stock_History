CREATE PROCEDURE [dbo].[option_01]
(
     @start_dttm   DATETIME, 
     @end_dttm     DATETIME, 
     @passing_list NVARCHAR(1), 
     @att1         NVARCHAR(250), 
     @att2         NVARCHAR(250), 
     @att3         NVARCHAR(250), 
     @att4         NVARCHAR(250), 
     @att5         NVARCHAR(250)
)
AS
    BEGIN

        --*****************************************
        --기간 내 인덱스보다 상승률이 높은 종목만 선정
        --*****************************************
        DECLARE 
               @idx_rate FLOAT;
        SELECT @idx_rate = ((e.end_idx - s.start_idx) / s.start_idx)
          FROM
        (
            SELECT ds.idx_sma5 AS start_idx
              FROM daily_strong ds
             WHERE ds.symbol = 'AAPL'
                   AND ds.date_hist = @start_dttm
        ) s, 
        (
            SELECT ds.idx_sma5 AS end_idx
              FROM daily_strong ds
             WHERE ds.symbol = 'AAPL'
                   AND ds.date_hist = @end_dttm
        ) e;
        IF @passing_list = 'Y'
            BEGIN
                --Reset Ranking
                UPDATE prediction
                   SET prediction.ranking = NULL;

                --Update
                WITH symbols
                     AS (SELECT ss.symbol, 
                                RANK() OVER(
                                ORDER BY ss.symbol_rate DESC) AS ranking, 
                                ROUND(ss.symbol_rate * 100, 0) AS symbol_rate
                           FROM
                         (
                             SELECT s.symbol, 
                                    ((e.end_rate - s.start_rate) / s.start_rate) AS symbol_rate
                               FROM
                             (
                                 SELECT dh.symbol, 
                                        dh.sma5 AS start_rate
                                   FROM daily_strong dh, 
                                        dbo.symbol_att sa
                                  WHERE dh.date_hist = @start_dttm
                                        AND dh.symbol = sa.symbol
                             ) s, 
                             (
                                 SELECT dh.symbol, 
                                        dh.sma5 AS end_rate
                                   FROM daily_strong dh, 
                                        dbo.symbol_att sa
                                  WHERE dh.date_hist = @end_dttm
                                        AND dh.symbol = sa.symbol
                             ) e
                              WHERE s.symbol = e.symbol
                         ) ss
                          WHERE ss.symbol_rate >= @idx_rate)
                     UPDATE p
                        SET p.ranking = s.ranking, 
                            p.ratio1 = s.symbol_rate
                       FROM prediction p, symbols s
                      WHERE p.symbol = s.symbol
                            AND s.ranking <= CAST(@att1 AS INT);

                --Delete Not in Limit
                DELETE prediction
                 WHERE prediction.ranking IS NULL;
        END;
            ELSE
            BEGIN
                WITH symbols
                     AS (SELECT ss.symbol, 
                                RANK() OVER(
                                ORDER BY ss.symbol_rate DESC) AS ranking, 
                                ROUND(ss.symbol_rate * 100, 0) AS symbol_rate
                           FROM
                         (
                             SELECT s.symbol, 
                                    ((e.end_rate - s.start_rate) / s.start_rate) AS symbol_rate
                               FROM
                             (
                                 SELECT dh.symbol, 
                                        dh.sma5 AS start_rate
                                   FROM daily_strong dh, 
                                        dbo.symbol_att sa
                                  WHERE dh.date_hist = @start_dttm
                                        AND dh.symbol = sa.symbol
                             ) s, 
                             (
                                 SELECT dh.symbol, 
                                        dh.sma5 AS end_rate
                                   FROM daily_strong dh, 
                                        dbo.symbol_att sa
                                  WHERE dh.date_hist = @end_dttm
                                        AND dh.symbol = sa.symbol
                             ) e
                              WHERE s.symbol = e.symbol
                         ) ss
                          WHERE ss.symbol_rate >= @idx_rate)
                     INSERT INTO prediction
                     (dbo.prediction.symbol, 
                      dbo.prediction.ranking, 
                      dbo.prediction.ratio1
                     )
                     SELECT s.symbol, 
                            s.ranking, 
                            s.symbol_rate
                       FROM symbols s
                      WHERE s.ranking <= CAST(@att1 AS INT);
        END;
    END;