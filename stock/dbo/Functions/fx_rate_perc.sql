CREATE FUNCTION dbo.fx_rate_perc
(@p1 FLOAT, 
 @p2 FLOAT
)
RETURNS FLOAT
AS
     BEGIN
         IF @p2 = 0
         BEGIN
             RETURN 0;
         END;

         RETURN ROUND((@p1 - @p2) / @p2 * 100, 2);

     END;