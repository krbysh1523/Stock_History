CREATE FUNCTION fx_ret_high
(@value FLOAT, 
 @limit FLOAT
)
RETURNS FLOAT
AS
     BEGIN
         RETURN(@value + (@value * (@limit / 100)));
     END;