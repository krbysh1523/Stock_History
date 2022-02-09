CREATE FUNCTION fx_ret_low
(@value FLOAT, 
 @limit FLOAT
)
RETURNS FLOAT
AS
     BEGIN
         RETURN(@value - (@value * (@limit / 100)));
     END;