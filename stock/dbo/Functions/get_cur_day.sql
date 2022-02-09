CREATE FUNCTION [dbo].[get_cur_day]
(@today DATE
)
RETURNS DATE
AS
     BEGIN

         DECLARE @ret DATE, @cnt INT;

         SET @ret = @today;

         WHILE @ret <= DATEADD(day, 30, @today)
         BEGIN

             SELECT @cnt = COUNT(1)
             FROM dbo.daily_source ds
             WHERE ds.symbol = 'SPY'
                   AND ds.[date] = @ret;

             IF @cnt > 0
             BEGIN
                 RETURN @ret;
             END;

             SET @ret = DATEADD(day, 1, @ret);
         END;

         RETURN @ret;

     END;