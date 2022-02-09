CREATE PROCEDURE sp_get_prediction_history
(
     @hist_id INT
)
AS
    BEGIN
        SELECT ph.symbol, 
               ph.ranking, 
               ph.ratio1, 
               ph.ratio2, 
               ph.rate_5, 
               ph.rate_10, 
               ph.rate_15, 
               ph.rate_20, 
               ph.rate_40, 
               ph.rate_60
          FROM dbo.prediction_history ph
         WHERE ph.hist_id = @hist_id
        ORDER BY ph.ranking;
    END;