CREATE PROCEDURE [dbo].[option_02]
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
        --종료 기준 SMA별로 시가 총액 랭크
        --*****************************************
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
                                ROUND(ss.symbol_rate / 1000000, 0) AS symbol_rate
                           FROM
                         (
                             SELECT s.symbol,
                                    CASE
                                        WHEN @att1 = 'S5'
                                        THEN s.t005
                                        WHEN @att1 = 'S10'
                                        THEN s.t010
                                        WHEN @att1 = 'S20'
                                        THEN s.t020
                                        WHEN @att1 = 'S30'
                                        THEN s.t030
                                        WHEN @att1 = 'S60'
                                        THEN s.t060
                                        WHEN @att1 = 'S120'
                                        THEN s.t120
                                        WHEN @att1 = 'S240'
                                        THEN s.t240
                                        ELSE-9999
                                    END AS symbol_rate
                               FROM
                             (
                                 SELECT dc.symbol, 
                                        (dc.sma_005 * dv.sma_005) AS t005, 
                                        (dc.sma_010 * dv.sma_010) AS t010, 
                                        (dc.sma_020 * dv.sma_020) AS t020, 
                                        (dc.sma_030 * dv.sma_030) AS t030, 
                                        (dc.sma_060 * dv.sma_060) AS t060, 
                                        (dc.sma_120 * dv.sma_120) AS t120, 
                                        (dc.sma_240 * dv.sma_240) AS t240
                                   FROM dbo.daily_close dc, 
                                        dbo.daily_volume dv, 
                                        prediction p
                                  WHERE dc.symbol = dv.symbol
                                        AND dc.symbol = p.symbol
                                        AND dc.date_hist = dv.date_hist
                                        AND dc.date_hist = @end_dttm
                             ) s
                         ) ss)
                     UPDATE p
                        SET p.ranking = s.ranking, 
                            p.ratio1 = s.symbol_rate
                       FROM prediction p, symbols s
                      WHERE p.symbol = s.symbol
                            AND s.ranking <= CAST(@att2 AS INT);

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
                                ROUND(ss.symbol_rate / 1000000, 0) AS symbol_rate
                           FROM
                         (
                             SELECT s.symbol,
                                    CASE
                                        WHEN @att1 = 'S5'
                                        THEN s.t005
                                        WHEN @att1 = 'S10'
                                        THEN s.t010
                                        WHEN @att1 = 'S20'
                                        THEN s.t020
                                        WHEN @att1 = 'S30'
                                        THEN s.t030
                                        WHEN @att1 = 'S60'
                                        THEN s.t060
                                        WHEN @att1 = 'S120'
                                        THEN s.t120
                                        WHEN @att1 = 'S240'
                                        THEN s.t240
                                        ELSE-9999
                                    END AS symbol_rate
                               FROM
                             (
                                 SELECT dc.symbol, 
                                        (dc.sma_005 * dv.sma_005) AS t005, 
                                        (dc.sma_010 * dv.sma_010) AS t010, 
                                        (dc.sma_020 * dv.sma_020) AS t020, 
                                        (dc.sma_030 * dv.sma_030) AS t030, 
                                        (dc.sma_060 * dv.sma_060) AS t060, 
                                        (dc.sma_120 * dv.sma_120) AS t120, 
                                        (dc.sma_240 * dv.sma_240) AS t240
                                   FROM dbo.daily_close dc, 
                                        dbo.daily_volume dv
                                  WHERE dc.symbol = dv.symbol
                                        AND dc.date_hist = dv.date_hist
                                        AND dc.date_hist = @end_dttm
                             ) s
                         ) ss)
                     INSERT INTO prediction
                     (dbo.prediction.symbol, 
                      dbo.prediction.ranking, 
                      dbo.prediction.ratio1
                     )
                     SELECT s.symbol, 
                            s.ranking, 
                            s.symbol_rate
                       FROM symbols s
                      WHERE s.ranking <= CAST(@att2 AS INT);
        END;
    END;