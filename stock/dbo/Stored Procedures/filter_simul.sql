CREATE PROCEDURE [dbo].[filter_simul]
(
     @start_dttm DATETIME, 
     @end_dttm   DATETIME
)
AS
    BEGIN
        DECLARE 
               @d5  DATETIME, 
               @d10 DATETIME, 
               @d15 DATETIME, 
               @d20 DATETIME, 
               @d40 DATETIME, 
               @d60 DATETIME;
        SET @d5 = dbo.get_nth_day(@end_dttm, 5);
        SET @d10 = dbo.get_nth_day(@end_dttm, 10);
        SET @d15 = dbo.get_nth_day(@end_dttm, 15);
        SET @d20 = dbo.get_nth_day(@end_dttm, 20);
        SET @d40 = dbo.get_nth_day(@end_dttm, 40);
        SET @d60 = dbo.get_nth_day(@end_dttm, 60);
        UPDATE p
           SET p.rate_5 = c.rate_5, -- float
               p.rate_10 = c.rate_10, -- float
               p.rate_15 = c.rate_15, -- float
               p.rate_20 = c.rate_20, -- float
               p.rate_40 = c.rate_40, -- float
               p.rate_60 = c.rate_60
          FROM dbo.prediction p,
        (
            SELECT a.symbol, 
                   ROUND(((s1.next_5 - a.val_end) / a.val_end * 100), 1) AS rate_5, 
                   ROUND(((s2.next_10 - a.val_end) / a.val_end * 100), 1) AS rate_10, 
                   ROUND(((s3.next_15 - a.val_end) / a.val_end * 100), 1) AS rate_15, 
                   ROUND(((s4.next_20 - a.val_end) / a.val_end * 100), 1) AS rate_20, 
                   ROUND(((s5.next_40 - a.val_end) / a.val_end * 100), 1) AS rate_40, 
                   ROUND(((s6.next_60 - a.val_end) / a.val_end * 100), 1) AS rate_60
              FROM
            (
                SELECT ds.symbol, 
                       ds.price AS val_end
                  FROM daily_strong ds, 
                       dbo.prediction p
                 WHERE ds.symbol = p.symbol
                       AND ds.date_hist = @end_dttm
            ) a, 
            (
                SELECT ds.symbol, 
                       ds.price AS next_5
                  FROM daily_strong ds, 
                       dbo.prediction p
                 WHERE ds.symbol = p.symbol
                       AND ds.date_hist = @d5
            ) s1, 
            (
                SELECT ds.symbol, 
                       ds.price AS next_10
                  FROM daily_strong ds, 
                       dbo.prediction p
                 WHERE ds.symbol = p.symbol
                       AND ds.date_hist = @d10
            ) s2, 
            (
                SELECT ds.symbol, 
                       ds.price AS next_15
                  FROM daily_strong ds, 
                       dbo.prediction p
                 WHERE ds.symbol = p.symbol
                       AND ds.date_hist = @d15
            ) s3, 
            (
                SELECT ds.symbol, 
                       ds.price AS next_20
                  FROM daily_strong ds, 
                       dbo.prediction p
                 WHERE ds.symbol = p.symbol
                       AND ds.date_hist = @d20
            ) s4, 
            (
                SELECT ds.symbol, 
                       ds.price AS next_40
                  FROM daily_strong ds, 
                       dbo.prediction p
                 WHERE ds.symbol = p.symbol
                       AND ds.date_hist = @d40
            ) s5, 
            (
                SELECT ds.symbol, 
                       ds.price AS next_60
                  FROM daily_strong ds, 
                       dbo.prediction p
                 WHERE ds.symbol = p.symbol
                       AND ds.date_hist = @d60
            ) s6
             WHERE a.symbol = s1.symbol
                   AND a.symbol = s2.symbol
                   AND a.symbol = s3.symbol
                   AND a.symbol = s4.symbol
                   AND a.symbol = s5.symbol
                   AND a.symbol = s6.symbol
        ) c
         WHERE c.symbol = p.symbol;
        --Update total at Top
        DECLARE 
               @cnt       REAL, 
               @total     REAL, 
               @total_txt NVARCHAR(10), 
               @t5        REAL, 
               @t10       REAL, 
               @t15       REAL, 
               @t20       REAL, 
               @t40       REAL, 
               @t60       REAL, 
               @avg       REAL, 
               @avg_txt   NVARCHAR(10), 
               @a5        REAL, 
               @a10       REAL, 
               @a15       REAL, 
               @a20       REAL, 
               @a40       REAL, 
               @a60       REAL;
        SET @total_txt = '_TOTAL';
        SET @avg_txt = '_AVG';
        SELECT @cnt = COUNT(1)
          FROM dbo.prediction p
         WHERE p.symbol NOT IN(@avg_txt, @total_txt);
        --Get % of Positive
        SELECT @t5 = CASE
                         WHEN @cnt = 0
                         THEN 0
                         ELSE ROUND(100 - ((@cnt - COUNT(1)) / @cnt * 100), 0)
                     END
          FROM dbo.prediction p
         WHERE p.symbol NOT IN(@avg_txt, @total_txt)
        AND p.rate_5 > 0;
        SELECT @t10 = CASE
                          WHEN @cnt = 0
                          THEN 0
                          ELSE ROUND(100 - ((@cnt - COUNT(1)) / @cnt * 100), 0)
                      END
          FROM dbo.prediction p
         WHERE p.symbol NOT IN(@avg_txt, @total_txt)
        AND p.rate_10 > 0;
        SELECT @t15 = CASE
                          WHEN @cnt = 0
                          THEN 0
                          ELSE ROUND(100 - ((@cnt - COUNT(1)) / @cnt * 100), 0)
                      END
          FROM dbo.prediction p
         WHERE p.symbol NOT IN(@avg_txt, @total_txt)
        AND p.rate_15 > 0;
        SELECT @t20 = CASE
                          WHEN @cnt = 0
                          THEN 0
                          ELSE ROUND(100 - ((@cnt - COUNT(1)) / @cnt * 100), 0)
                      END
          FROM dbo.prediction p
         WHERE p.symbol NOT IN(@avg_txt, @total_txt)
        AND p.rate_20 > 0;
        SELECT @t40 = CASE
                          WHEN @cnt = 0
                          THEN 0
                          ELSE ROUND(100 - ((@cnt - COUNT(1)) / @cnt * 100), 0)
                      END
          FROM dbo.prediction p
         WHERE p.symbol NOT IN(@avg_txt, @total_txt)
        AND p.rate_40 > 0;
        SELECT @t60 = CASE
                          WHEN @cnt = 0
                          THEN 0
                          ELSE ROUND(100 - ((@cnt - COUNT(1)) / @cnt * 100), 0)
                      END
          FROM dbo.prediction p
         WHERE p.symbol NOT IN(@avg_txt, @total_txt)
        AND p.rate_60 > 0;
        --Insert/Update Total
        SELECT @total = COUNT(1)
          FROM dbo.prediction p
         WHERE p.symbol = @total_txt;
        IF @total > 1
            BEGIN
                UPDATE p
                   SET p.ranking = 0, 
                       p.ratio1 = @cnt, 
                       p.rate_5 = @t5, 
                       p.rate_10 = @t10, 
                       p.rate_15 = @t15, 
                       p.rate_20 = @t20, 
                       p.rate_40 = @t40, 
                       p.rate_60 = @t60
                  FROM prediction p
                 WHERE p.symbol = @total_txt;
        END;
            ELSE
            BEGIN
                INSERT INTO dbo.prediction
                (symbol, 
                 ranking, 
                 ratio1, 
                 rate_5, 
                 rate_10, 
                 rate_15, 
                 rate_20, 
                 rate_40, 
                 rate_60
                )
                VALUES
                (
                       @total_txt, 
                       -2, 
                       @cnt, 
                       @t5, 
                       @t10, 
                       @t15, 
                       @t20, 
                       @t40, 
                       @t60
                );
        END;
        --Insert/Update AVG
        SELECT @a5 = ROUND(AVG(p.rate_5), 0), 
               @a10 = ROUND(AVG(p.rate_10), 0), 
               @a15 = ROUND(AVG(p.rate_15), 0), 
               @a20 = ROUND(AVG(p.rate_20), 0), 
               @a40 = ROUND(AVG(p.rate_40), 0), 
               @a60 = ROUND(AVG(p.rate_60), 0)
          FROM dbo.prediction p
         WHERE p.symbol NOT IN(@avg_txt, @total_txt);
        SELECT @avg = COUNT(1)
          FROM dbo.prediction p
         WHERE p.symbol = @avg_txt;
        IF @total > 1
            BEGIN
                UPDATE p
                   SET p.ranking = 0, 
                       p.ratio1 = @cnt, 
                       p.rate_5 = @a5, 
                       p.rate_10 = @a10, 
                       p.rate_15 = @a15, 
                       p.rate_20 = @a20, 
                       p.rate_40 = @a40, 
                       p.rate_60 = @a60
                  FROM prediction p
                 WHERE p.symbol = @avg_txt;
        END;
            ELSE
            BEGIN
                INSERT INTO dbo.prediction
                (symbol, 
                 ranking, 
                 ratio1, 
                 rate_5, 
                 rate_10, 
                 rate_15, 
                 rate_20, 
                 rate_40, 
                 rate_60
                )
                VALUES
                (
                       @avg_txt, 
                       -1, 
                       @cnt, 
                       @a5, 
                       @a10, 
                       @a15, 
                       @a20, 
                       @a40, 
                       @a60
                );
        END;
    END;