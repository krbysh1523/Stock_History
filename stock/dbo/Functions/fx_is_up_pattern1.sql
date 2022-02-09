CREATE FUNCTION fx_is_up_pattern1
(@dttm      DATE, 
 @symbol    NVARCHAR(50), 
 @diff_rate FLOAT
)
RETURNS CHAR(1)
AS
     BEGIN
         DECLARE @cnt INT;
         SELECT @cnt = COUNT(1)
         FROM
         (
             SELECT dc.symbol, 
                    dc.[open], 
                    dc.[close], 
                    ABS(dc.[open] - dc.[close]) AS diff
             FROM daily_history dc, 
                  dbo.daily_volume dv
             WHERE dc.[date] = @dttm
                   AND dc.symbol = @symbol
                   AND dc.symbol = dv.symbol
                   AND dc.[date] = dv.date_hist
                   AND dc.[open] < dc.[close]
                   AND dv.volume > dv.sma_005
         ) c, 
         (
             SELECT dc.symbol, 
                    dc.[open], 
                    dc.[close], 
                    ABS(dc.[open] - dc.[close]) AS diff
             FROM daily_history dc
             WHERE dc.[date] = dbo.get_pre_day(@dttm)
             AND dc.[open] > dc.[close]
             AND dc.symbol = @symbol
         ) p
         WHERE c.symbol = p.symbol
               AND c.[open] < p.[close]
               AND c.[close] > p.[open]
               AND (c.diff / p.diff) > @diff_rate;

         IF @cnt > 0
         BEGIN
             RETURN 'Y';
         END;

         RETURN 'N';
     END;