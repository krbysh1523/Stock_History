CREATE FUNCTION get_pre_day
(@today DATE
)
RETURNS DATE
AS
     BEGIN

         DECLARE @ret DATE, @cnt INT;

         SET @ret = @today;

         WHILE @ret >= dateadd(day, -30, @today)
         BEGIN
             SET @ret = DATEADD(day, -1, @ret);

             SELECT @cnt = COUNT(1)
             FROM dbo.daily_source ds
             WHERE ds.symbol = 'SPY'
                   AND ds.[date] = @ret;

             IF @cnt > 0
             BEGIN
                 RETURN @ret;
             END;
         END;

		 RETURN @ret

     END;