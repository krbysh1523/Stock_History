CREATE PROCEDURE [dbo].[option_05]
(
     @start_dttm   DATETIME, 
     @end_dttm     DATETIME, 
     @passing_list NVARCHAR(1), 
     @att1         NVARCHAR(250), --SMA5,10,20...
     @att2         NVARCHAR(250), --Lower Limit
     @att3         NVARCHAR(250), --Upper Limit
     @att4         NVARCHAR(250), 
     @att5         NVARCHAR(250)
)
AS
    BEGIN

/*******************************************
	* 기간 내 종가 (SMA5,10,20...) 상승률을 Limit사이로 제한함
	******************************************/

        IF @passing_list = 'Y'
            BEGIN
                --Reset Ranking
                UPDATE prediction
                   SET prediction.ranking = NULL;

                --Update Ranking
                UPDATE p
                   SET p.ranking = t.ranking, 
                       p.ratio1 = t.ratio1
                  FROM dbo.prediction p,
                (
                    SELECT ss.symbol, 
                           RANK() OVER(
                           ORDER BY ss.ratio1 DESC) AS ranking, 
                           ss.ratio1
                      FROM
                    (
                        SELECT s.symbol,
                               CASE
                                   WHEN @att1 = 'S5'
                                   THEN SMA_005
                                   WHEN @att1 = 'S10'
                                   THEN SMA_010
                                   WHEN @att1 = 'S20'
                                   THEN SMA_020
                                   WHEN @att1 = 'S30'
                                   THEN SMA_030
                                   WHEN @att1 = 'S60'
                                   THEN SMA_060
                                   WHEN @att1 = 'S120'
                                   THEN SMA_120
                                   WHEN @att1 = 'S240'
                                   THEN SMA_240
                                   ELSE 0
                               END AS ratio1
                          FROM
                        (
                            SELECT a.symbol,
                                   CASE
                                       WHEN b.SMA_005 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_005 - a.SMA_005) / a.SMA_005 * 100, 2)
                                   END AS SMA_005,
                                   CASE
                                       WHEN b.SMA_010 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_010 - a.SMA_010) / a.SMA_010 * 100, 2)
                                   END AS SMA_010,
                                   CASE
                                       WHEN b.SMA_020 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_020 - a.SMA_020) / a.SMA_020 * 100, 2)
                                   END AS SMA_020,
                                   CASE
                                       WHEN b.SMA_030 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_030 - a.SMA_030) / a.SMA_030 * 100, 2)
                                   END AS SMA_030,
                                   CASE
                                       WHEN b.SMA_060 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_060 - a.SMA_005) / a.SMA_060 * 100, 2)
                                   END AS SMA_060,
                                   CASE
                                       WHEN b.SMA_120 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_120 - a.SMA_120) / a.SMA_120 * 100, 2)
                                   END AS SMA_120,
                                   CASE
                                       WHEN b.SMA_240 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_240 - a.SMA_240) / a.SMA_240 * 100, 2)
                                   END AS SMA_240
                              FROM
                            (
                                SELECT ds.symbol, 
                                       ds.SMA_005, 
                                       ds.SMA_010, 
                                       ds.SMA_020, 
                                       ds.SMA_030, 
                                       ds.SMA_060, 
                                       ds.SMA_120, 
                                       ds.SMA_240
                                  FROM dbo.daily_close ds, 
                                       dbo.prediction p
                                 WHERE ds.date_hist = @start_dttm
                                       AND ds.symbol = p.symbol
                            ) a, 
                            (
                                SELECT ds.symbol, 
                                       ds.SMA_005, 
                                       ds.SMA_010, 
                                       ds.SMA_020, 
                                       ds.SMA_030, 
                                       ds.SMA_060, 
                                       ds.SMA_120, 
                                       ds.SMA_240
                                  FROM dbo.daily_close ds, 
                                       dbo.prediction p
                                 WHERE ds.date_hist = @end_dttm
                                       AND ds.symbol = p.symbol
                            ) b
                             WHERE a.symbol = b.symbol
                        ) s
                    ) ss
                ) t
                 WHERE p.symbol = t.symbol
                       AND t.ratio1 >= CAST(@att2 AS INT)
                       AND t.ratio1 <= CAST(@att3 AS INT);

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
                SELECT t.symbol, 
                       t.ranking, 
                       t.ratio1
                  FROM
                (
                    SELECT ss.symbol, 
                           RANK() OVER(
                           ORDER BY ss.ratio1 DESC) AS ranking, 
                           ss.ratio1
                      FROM
                    (
                        SELECT s.symbol,
                               CASE
                                   WHEN @att1 = 'S5'
                                   THEN SMA_005
                                   WHEN @att1 = 'S10'
                                   THEN SMA_010
                                   WHEN @att1 = 'S20'
                                   THEN SMA_020
                                   WHEN @att1 = 'S30'
                                   THEN SMA_030
                                   WHEN @att1 = 'S60'
                                   THEN SMA_060
                                   WHEN @att1 = 'S120'
                                   THEN SMA_120
                                   WHEN @att1 = 'S240'
                                   THEN SMA_240
                                   ELSE 0
                               END AS ratio1
                          FROM
                        (
                            SELECT a.symbol,
                                   CASE
                                       WHEN b.SMA_005 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_005 - a.SMA_005) / a.SMA_005 * 100, 2)
                                   END AS SMA_005,
                                   CASE
                                       WHEN b.SMA_010 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_010 - a.SMA_010) / a.SMA_010 * 100, 2)
                                   END AS SMA_010,
                                   CASE
                                       WHEN b.SMA_020 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_020 - a.SMA_020) / a.SMA_020 * 100, 2)
                                   END AS SMA_020,
                                   CASE
                                       WHEN b.SMA_030 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_030 - a.SMA_030) / a.SMA_030 * 100, 2)
                                   END AS SMA_030,
                                   CASE
                                       WHEN b.SMA_060 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_060 - a.SMA_005) / a.SMA_060 * 100, 2)
                                   END AS SMA_060,
                                   CASE
                                       WHEN b.SMA_120 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_120 - a.SMA_120) / a.SMA_120 * 100, 2)
                                   END AS SMA_120,
                                   CASE
                                       WHEN b.SMA_240 = 0
                                       THEN 0
                                       ELSE ROUND((b.SMA_240 - a.SMA_240) / a.SMA_240 * 100, 2)
                                   END AS SMA_240
                              FROM
                            (
                                SELECT ds.symbol, 
                                       ds.SMA_005, 
                                       ds.SMA_010, 
                                       ds.SMA_020, 
                                       ds.SMA_030, 
                                       ds.SMA_060, 
                                       ds.SMA_120, 
                                       ds.SMA_240
                                  FROM dbo.daily_close ds
                                 WHERE ds.date_hist = @start_dttm
                            ) a, 
                            (
                                SELECT ds.symbol, 
                                       ds.SMA_005, 
                                       ds.SMA_010, 
                                       ds.SMA_020, 
                                       ds.SMA_030, 
                                       ds.SMA_060, 
                                       ds.SMA_120, 
                                       ds.SMA_240
                                  FROM dbo.daily_close ds
                                 WHERE ds.date_hist = @end_dttm
                            ) b
                             WHERE a.symbol = b.symbol
                        ) s
                    ) ss
                ) t
                 WHERE t.ratio1 >= CAST(@att2 AS INT)
                       AND t.ratio1 <= CAST(@att3 AS INT);
        END;
    END;