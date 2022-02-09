CREATE FUNCTION fx_greater
(@p1 FLOAT, 
 @p2 FLOAT
)
RETURNS FLOAT
AS
     BEGIN
         IF @p1 >= @p2
         BEGIN
             RETURN @p1;
         END;
         RETURN @p2;
     END;