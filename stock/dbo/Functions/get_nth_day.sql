CREATE FUNCTION [dbo].[get_nth_day]
(
     @today DATE, 
     @nth   INT
)
RETURNS DATE
AS
     BEGIN
         DECLARE 
                @ret DATE, 
                @row INT, 
                @cnt INT;
         SET @ret = @today;
         SET @cnt = 1;
         WHILE @ret <= DATEADD(day, 1000, @today)
             BEGIN
                 SET @ret = DATEADD(day, 1, @ret);
                 SELECT @row = COUNT(1)
                   FROM dbo.daily_source ds
                  WHERE ds.symbol = 'SPY'
                        AND ds.[date] = @ret;
                 IF @row = 1
                     BEGIN
                         IF @cnt = @nth
                             BEGIN
                                 RETURN @ret;
                         END;
                             ELSE
                             BEGIN
                                 SET @cnt = @cnt + 1;
                         END;
                 END;
             END;
         RETURN @ret;
     END;