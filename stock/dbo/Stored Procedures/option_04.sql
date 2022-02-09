CREATE PROCEDURE [dbo].[option_04]
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
	* Get SMA5 Diff of End Date - Start Date 
	* Rank Them 
	* @att1 = Rank Limit
	******************************************/

        IF @passing_list = 'Y'
            BEGIN
                --Reset Ranking
                UPDATE prediction
                   SET prediction.ranking = NULL;

                --Update
                UPDATE p
                   SET p.ranking = c1.ranking, 
                       p.ratio1 = c1.rate
                  FROM dbo.prediction p,
                (
                    SELECT RANK() OVER(
                           ORDER BY c.rate DESC) AS ranking, 
                           c.symbol, 
                           c.rate
                      FROM
                    (
                        SELECT a.symbol, 
                               ROUND(((b.val_end - a.val_start) / a.val_start * 100), 2) AS rate
                          FROM
                        (
                            SELECT ds.symbol, 
                                   ds.sma5 AS val_start
                              FROM daily_strong ds, 
                                   dbo.prediction p
                             WHERE ds.symbol = p.symbol
                                   AND ds.date_hist = @start_dttm
                        ) a, 
                        (
                            SELECT ds.symbol, 
                                   ds.sma5 AS val_end
                              FROM daily_strong ds, 
                                   dbo.prediction p
                             WHERE ds.symbol = p.symbol
                                   AND ds.date_hist = @end_dttm
                        ) b
                         WHERE a.symbol = b.symbol
                    ) c
                ) c1
                 WHERE p.symbol = c1.symbol
                       AND c1.ranking <= CAST(@att1 AS INT);

                --Delete Not in Limit
                DELETE prediction
                 WHERE prediction.ranking IS NULL;
        END;
            ELSE
            BEGIN
                INSERT INTO dbo.prediction
                (ranking, 
                 symbol, 
                 ratio1
                )
                SELECT *
                  FROM
                (
                    SELECT RANK() OVER(
                           ORDER BY c.rate DESC) AS ranking, 
                           c.symbol, 
                           c.rate
                      FROM
                    (
                        SELECT a.symbol, 
                               ROUND(((b.val_end - a.val_start) / a.val_start * 100), 2) AS rate
                          FROM
                        (
                            SELECT ds.symbol, 
                                   ds.sma5 AS val_start
                              FROM daily_strong ds
                             WHERE ds.date_hist = @start_dttm
                        ) a, 
                        (
                            SELECT ds.symbol, 
                                   ds.sma5 AS val_end
                              FROM daily_strong ds
                             WHERE ds.date_hist = @end_dttm
                        ) b
                         WHERE a.symbol = b.symbol
                    ) c
                ) c1
                 WHERE c1.ranking <= CAST(@att1 AS INT);
        END;
    END;