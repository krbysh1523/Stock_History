
CREATE FUNCTION [dbo].[fx_lower]
(@p1 FLOAT, 
 @p2 FLOAT
)
RETURNS FLOAT
AS
     BEGIN
         IF @p1 >= @p2
         BEGIN
             RETURN @p2;
         END;
         RETURN @p1;
     END;
