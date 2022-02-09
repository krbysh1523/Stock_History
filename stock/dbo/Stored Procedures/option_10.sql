CREATE PROCEDURE [dbo].[option_10]
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
        --각 날짜별 인덱스 비교해서 +가 가장 많은 기준 (SMA5)
        --*****************************************
        IF @passing_list = 'Y'
            BEGIN
                --Reset Ranking
                UPDATE prediction
                   SET prediction.ranking = NULL;

                --Update
                WITH symbols
                     AS (SELECT a.symbol, 
                                RANK() OVER(
                                ORDER BY a.plus DESC) AS ranking
                           FROM
                         (
                             SELECT ds.symbol, 
                                    COUNT(*) AS plus
                               FROM daily_strong ds, 
                                    prediction p
                              WHERE ds.date_hist >= @start_dttm
                                    AND ds.date_hist <= @end_dttm
                                    AND ds.idx_diff_sma5 > 0
                                    AND ds.symbol = p.symbol
                              GROUP BY ds.symbol
                         ) a)
                     UPDATE p
                        SET p.ranking = s.ranking
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
                     AS (SELECT a.symbol, 
                                RANK() OVER(
                                ORDER BY a.plus DESC) AS ranking
                           FROM
                         (
                             SELECT ds.symbol, 
                                    COUNT(*) AS plus
                               FROM daily_strong ds
                              WHERE ds.date_hist >= @start_dttm
                                    AND ds.date_hist <= @end_dttm
                                    AND ds.idx_diff_sma5 > 0
                              GROUP BY ds.symbol
                         ) a)
                     INSERT INTO prediction
                     (dbo.prediction.symbol, 
                      dbo.prediction.ranking
                     )
                     SELECT s.symbol, 
                            s.ranking
                       FROM symbols s
                      WHERE s.ranking <= CAST(@att1 AS INT);
        END;
    END;