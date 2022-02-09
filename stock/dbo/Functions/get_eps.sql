CREATE FUNCTION get_eps
(@eps_type NVARCHAR(10), 
 @symbol   NVARCHAR(50), 
 @date     DATE
)
RETURNS FLOAT
AS
     BEGIN
         DECLARE @ret FLOAT;
         IF @eps_type = 'fiscal'
             BEGIN
                 SET @ret = ISNULL(
                 (
                     SELECT TOP 1 
                            e.reportedeps
                     FROM earning e
                     WHERE e.symbol = @symbol
                           AND @date >= e.fiscaldateending
                           AND @date < e.fiscaldateending
                 ), 0);
         END;
             ELSE
             IF @eps_type = 'report'
                 BEGIN
                     SET @ret = ISNULL(
                     (
                         SELECT TOP 1 
                                e.reportedeps
                         FROM earning e
                         WHERE e.symbol = @symbol
                               AND @date >= e.reporteddate
                               AND @date < e.reporteddate
                     ), 0);
             END;
         RETURN @ret;
     END;
