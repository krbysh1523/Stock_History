CREATE PROCEDURE [dbo].[option_03]
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
        --검색 종료 일주일 거래량이 검색 종료 한달보다 높은 거래량 순위
        --*****************************************
        IF @passing_list = 'Y'
            BEGIN
                --Reset Ranking
                UPDATE prediction
                   SET prediction.ranking = NULL;

                --Update
                UPDATE p
                   SET p.ranking = s.ranking, 
                       p.ratio1 = s.vol_rate
                  FROM prediction p,
                (
                    SELECT r.symbol, 
                           ROW_NUMBER() OVER(
                           ORDER BY r.vol_rate DESC) AS ranking, 
                           r.vol_rate
                      FROM
                    (
                        SELECT dv.symbol, 
                               dv.sma_005, 
                               dv.sma_020,
                               CASE
                                   WHEN dv.sma_020 = 0
                                   THEN 0
                                   ELSE ROUND((dv.sma_005 - dv.sma_020) / dv.sma_020 * 100, 2)
                               END AS vol_rate
                          FROM dbo.daily_volume dv, 
                               prediction p
                         WHERE dv.date_hist = @end_dttm
                               AND dv.sma_005 >= dv.sma_020
                               AND dv.symbol = p.symbol
                    ) r
                ) s
                 WHERE p.symbol = s.symbol
                       AND s.ranking <= CAST(@att1 AS INT);

                --Delete Not in Limit
                DELETE prediction
                 WHERE prediction.ranking IS NULL;
        END;
            ELSE
            BEGIN
                INSERT INTO prediction
                (dbo.prediction.symbol, 
                 dbo.prediction.ranking, 
                 dbo.prediction.ratio1
                )
                SELECT s.symbol, 
                       s.ranking, 
                       s.ratio1
                  FROM
                (
                    SELECT r.symbol, 
                           ROW_NUMBER() OVER(
                           ORDER BY r.ratio1 DESC) AS ranking, 
                           r.ratio1
                      FROM
                    (
                        SELECT dv.symbol, 
                               dv.sma_005, 
                               dv.sma_020,
                               CASE
                                   WHEN dv.sma_020 = 0
                                   THEN 0
                                   ELSE ROUND((dv.sma_005 - dv.sma_020) / dv.sma_020 * 100, 2)
                               END AS ratio1
                          FROM dbo.daily_volume dv
                         WHERE dv.date_hist = @end_dttm
                               AND dv.sma_005 >= dv.sma_020
                    ) r
                ) s
                 WHERE s.ranking <= CAST(@att1 AS INT);
        END;
    END;