CREATE PROCEDURE [dbo].[option_06]
(
     @start_dttm   DATETIME, 
     @end_dttm     DATETIME, 
     @passing_list NVARCHAR(1), 
     @att1         NVARCHAR(250), --Largest SMA
     @att2         NVARCHAR(250), 
     @att3         NVARCHAR(250), 
     @att4         NVARCHAR(250), 
     @att5         NVARCHAR(250) -- Smallest SMA
)
AS
    BEGIN

/*******************************************
	* 검색 종료날 종가 기준으로 SMA 순서 (SMA5 > SMA10 > SMA20)
	******************************************/

        IF @passing_list = 'Y'
            BEGIN
                --Reset Ranking
                UPDATE prediction
                   SET prediction.ranking = NULL;

                --Update Ranking
                UPDATE p
                   SET p.ranking = t.ranking
                  FROM dbo.prediction p,
                (
                    SELECT s.symbol, 
                           1 AS ranking
                      FROM
                    (
                        SELECT a.symbol,
                               CASE
                                   WHEN @att1 = 'S5'
                                   THEN a.sma_005
                                   WHEN @att1 = 'S10'
                                   THEN a.sma_010
                                   WHEN @att1 = 'S20'
                                   THEN a.sma_020
                                   WHEN @att1 = 'S30'
                                   THEN a.sma_030
                                   WHEN @att1 = 'S60'
                                   THEN a.sma_060
                                   WHEN @att1 = 'S120'
                                   THEN a.sma_120
                                   WHEN @att1 = 'S240'
                                   THEN a.sma_240
                                   ELSE-9991
                               END AS att1,
                               CASE
                                   WHEN @att2 = 'S5'
                                   THEN a.sma_005
                                   WHEN @att2 = 'S10'
                                   THEN a.sma_010
                                   WHEN @att2 = 'S20'
                                   THEN a.sma_020
                                   WHEN @att2 = 'S30'
                                   THEN a.sma_030
                                   WHEN @att2 = 'S60'
                                   THEN a.sma_060
                                   WHEN @att2 = 'S120'
                                   THEN a.sma_120
                                   WHEN @att2 = 'S240'
                                   THEN a.sma_240
                                   ELSE-9992
                               END AS att2,
                               CASE
                                   WHEN @att3 = 'S5'
                                   THEN a.sma_005
                                   WHEN @att3 = 'S10'
                                   THEN a.sma_010
                                   WHEN @att3 = 'S20'
                                   THEN a.sma_020
                                   WHEN @att1 = 'S30'
                                   THEN a.sma_030
                                   WHEN @att3 = 'S60'
                                   THEN a.sma_060
                                   WHEN @att3 = 'S120'
                                   THEN a.sma_120
                                   WHEN @att3 = 'S240'
                                   THEN a.sma_240
                                   ELSE-9993
                               END AS att3,
                               CASE
                                   WHEN @att4 = 'S5'
                                   THEN a.sma_005
                                   WHEN @att4 = 'S10'
                                   THEN a.sma_010
                                   WHEN @att4 = 'S20'
                                   THEN a.sma_020
                                   WHEN @att4 = 'S30'
                                   THEN a.sma_030
                                   WHEN @att4 = 'S60'
                                   THEN a.sma_060
                                   WHEN @att4 = 'S120'
                                   THEN a.sma_120
                                   WHEN @att4 = 'S240'
                                   THEN a.sma_240
                                   ELSE-9994
                               END AS att4,
                               CASE
                                   WHEN @att5 = 'S5'
                                   THEN a.sma_005
                                   WHEN @att5 = 'S10'
                                   THEN a.sma_010
                                   WHEN @att5 = 'S20'
                                   THEN a.sma_020
                                   WHEN @att5 = 'S30'
                                   THEN a.sma_030
                                   WHEN @att5 = 'S60'
                                   THEN a.sma_060
                                   WHEN @att5 = 'S120'
                                   THEN a.sma_120
                                   WHEN @att5 = 'S240'
                                   THEN a.sma_240
                                   ELSE-9995
                               END AS att5
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
                             WHERE ds.date_hist = @end_dttm
                                   AND ds.symbol = p.symbol
                        ) a
                    ) s
                     WHERE s.att1 > s.att2
                           AND s.att2 > s.att3
                           AND s.att3 > s.att4
                           AND s.att4 > s.att5
                ) t
                 WHERE p.symbol = t.symbol;

                --Delete Not in Limit
                DELETE prediction
                 WHERE prediction.ranking IS NULL;
        END;
            ELSE
            BEGIN
                INSERT INTO dbo.prediction
                (symbol, 
                 ranking
                )
                SELECT s.symbol, 
                       1 AS ranking
                  FROM
                (
                    SELECT a.symbol,
                           CASE
                               WHEN @att1 = 'S5'
                               THEN a.sma_005
                               WHEN @att1 = 'S10'
                               THEN a.sma_010
                               WHEN @att1 = 'S20'
                               THEN a.sma_020
                               WHEN @att1 = 'S30'
                               THEN a.sma_030
                               WHEN @att1 = 'S60'
                               THEN a.sma_060
                               WHEN @att1 = 'S120'
                               THEN a.sma_120
                               WHEN @att1 = 'S240'
                               THEN a.sma_240
                               ELSE-9991
                           END AS att1,
                           CASE
                               WHEN @att2 = 'S5'
                               THEN a.sma_005
                               WHEN @att2 = 'S10'
                               THEN a.sma_010
                               WHEN @att2 = 'S20'
                               THEN a.sma_020
                               WHEN @att2 = 'S30'
                               THEN a.sma_030
                               WHEN @att2 = 'S60'
                               THEN a.sma_060
                               WHEN @att2 = 'S120'
                               THEN a.sma_120
                               WHEN @att2 = 'S240'
                               THEN a.sma_240
                               ELSE-9992
                           END AS att2,
                           CASE
                               WHEN @att3 = 'S5'
                               THEN a.sma_005
                               WHEN @att3 = 'S10'
                               THEN a.sma_010
                               WHEN @att3 = 'S20'
                               THEN a.sma_020
                               WHEN @att1 = 'S30'
                               THEN a.sma_030
                               WHEN @att3 = 'S60'
                               THEN a.sma_060
                               WHEN @att3 = 'S120'
                               THEN a.sma_120
                               WHEN @att3 = 'S240'
                               THEN a.sma_240
                               ELSE-9993
                           END AS att3,
                           CASE
                               WHEN @att4 = 'S5'
                               THEN a.sma_005
                               WHEN @att4 = 'S10'
                               THEN a.sma_010
                               WHEN @att4 = 'S20'
                               THEN a.sma_020
                               WHEN @att4 = 'S30'
                               THEN a.sma_030
                               WHEN @att4 = 'S60'
                               THEN a.sma_060
                               WHEN @att4 = 'S120'
                               THEN a.sma_120
                               WHEN @att4 = 'S240'
                               THEN a.sma_240
                               ELSE-9994
                           END AS att4,
                           CASE
                               WHEN @att5 = 'S5'
                               THEN a.sma_005
                               WHEN @att5 = 'S10'
                               THEN a.sma_010
                               WHEN @att5 = 'S20'
                               THEN a.sma_020
                               WHEN @att5 = 'S30'
                               THEN a.sma_030
                               WHEN @att5 = 'S60'
                               THEN a.sma_060
                               WHEN @att5 = 'S120'
                               THEN a.sma_120
                               WHEN @att5 = 'S240'
                               THEN a.sma_240
                               ELSE-9995
                           END AS att5
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
                         WHERE ds.date_hist = @end_dttm
                               AND ds.symbol = p.symbol
                    ) a
                ) s
                 WHERE s.att1 > s.att2
                       AND s.att2 > s.att3
                       AND s.att3 > s.att4
                       AND s.att4 > s.att5;
        END;
    END;