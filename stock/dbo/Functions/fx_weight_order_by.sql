CREATE FUNCTION [dbo].[fx_weight_order_by]
(@col1        FLOAT, 
 @col1_weight FLOAT = 50, 
 @col2        FLOAT, 
 @col2_weight FLOAT = 50, 
 @col3        FLOAT = NULL, 
 @col3_weight FLOAT = 0, 
 @col4        FLOAT = NULL, 
 @col4_weight FLOAT = 0, 
 @col5        FLOAT = NULL, 
 @col5_weight FLOAT = 0
)
RETURNS FLOAT
AS
     BEGIN
         IF @col3 IS NULL
         BEGIN
             RETURN(@col1 * (@col1_weight / 100)) + (@col2 * (@col2_weight / 100));
         END;
         ELSE
         BEGIN
             IF @col4 IS NULL
             BEGIN
                 RETURN(@col1 * (@col1_weight / 100)) + (@col2 * (@col2_weight / 100)) + (@col3 * (@col3_weight / 100));
             END;
             ELSE
             BEGIN
                 IF @col5 IS NULL
                 BEGIN
                     RETURN(@col1 * (@col1_weight / 100)) + (@col2 * (@col2_weight / 100)) + (@col3 * (@col3_weight / 100)) + (@col4 * (@col4_weight / 100));
                 END;
             END;
         END;

         RETURN(@col1 * (@col1_weight / 100)) + (@col2 * (@col2_weight / 100)) + (@col3 * (@col3_weight / 100)) + (@col4 * (@col4_weight / 100)) + (@col5 * (@col5_weight / 100));
     END;