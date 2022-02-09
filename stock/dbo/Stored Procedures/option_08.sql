CREATE PROCEDURE [dbo].[option_08]
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

/*******************************************
	* 종료 일주일내에 장대 양봉이 있는 것 빼기
	******************************************/

        IF @passing_list = 'Y'
            BEGIN
                --Reset Ranking
                UPDATE prediction
                   SET prediction.ranking = NULL;

                --Update Ranking
                WITH symbols
                     AS (SELECT t.symbol, 
                                t.ratio1 * 100 AS ratio1, 
                                RANK() OVER(
                                ORDER BY(t.ratio1) DESC) AS ranking
                           FROM
                         (
                             SELECT a.symbol,
                                    CASE
                                        WHEN a.sma_20 = 0
                                        THEN 0
                                        ELSE(b.max_7 - a.sma_20) / a.sma_20
                                    END AS ratio1
                               FROM
                             (
                                 SELECT d.symbol, 
                                        AVG(d.high_low_perc) * 100 AS sma_20
                                   FROM daily_analysis d, 
                                        prediction p
                                  WHERE d.symbol = p.symbol
                                        AND d.[date] BETWEEN DATEADD(month, -1, @end_dttm) AND @end_dttm
                                  GROUP BY d.symbol
                             ) a, 
                             (
                                 SELECT d.symbol, 
                                        MAX(d.high_low_perc) * 100 AS max_7
                                   FROM daily_analysis d, 
                                        prediction p
                                  WHERE d.symbol = p.symbol
                                        AND d.[date] BETWEEN DATEADD(day, -7, @end_dttm) AND @end_dttm
                                        AND d.dir = 'D'
                                  GROUP BY d.symbol
                             ) b
                              WHERE a.symbol = b.symbol
                         ) t)
                     UPDATE p
                        SET p.ranking = s.ranking, 
                            p.ratio1 = s.ratio1
                       FROM dbo.prediction p, symbols s
                      WHERE p.symbol = s.symbol
                            AND s.ratio1 < CAST(@att1 AS INT);

                --Delete Not in Limit
                DELETE prediction
                 WHERE prediction.ranking IS NULL;
        END;
            ELSE
            BEGIN
                INSERT INTO dbo.prediction
                (symbol, 
                 ranking, 
                 ratio1
                )
                SELECT *
                  FROM
                (
                    SELECT a.symbol, 
                           RANK() OVER(
                           ORDER BY((b.max_7 - a.sma_20) / a.sma_20) DESC) AS ranking, 
                           ((b.max_7 - a.sma_20) / a.sma_20) * 100 AS ratio1
                      FROM
                    (
                        SELECT d.symbol, 
                               AVG(d.high_low_perc) * 100 AS sma_20
                          FROM daily_analysis d
                         WHERE d.[date] BETWEEN DATEADD(month, -1, @end_dttm) AND @end_dttm
                         GROUP BY d.symbol
                    ) a, 
                    (
                        SELECT d.symbol, 
                               MAX(d.high_low_perc) * 100 AS max_7
                          FROM daily_analysis d
                         WHERE d.[date] BETWEEN DATEADD(day, -7, @end_dttm) AND @end_dttm
                               AND d.dir = 'D'
                         GROUP BY d.symbol
                    ) b
                     WHERE a.symbol = b.symbol
                ) t
                 WHERE t.ratio1 < CAST(@att1 AS INT);
        END;
    END;