CREATE PROCEDURE [dbo].[sp_prediction_history_cleanup]
AS
    BEGIN
        DECLARE 
               @history_row INT;

        --Set Max History
        SET @history_row = 100;

        --Delete List History older than Max
        DELETE lh
          FROM dbo.list_history lh
         WHERE lh.hist_id IN
        (
            SELECT pha.hist_id
              FROM
            (
                SELECT lh.HIST_ID, 
                       RANK() OVER(
                       ORDER BY lh.HIST_ID DESC) AS hist_rank
                  FROM dbo.list_history lh
            ) pha
             WHERE pha.hist_rank > @history_row
        );
        --Delete History older than Max
        DELETE ph
          FROM dbo.prediction_history ph
         WHERE ph.hist_id IN
        (
            SELECT pha.hist_id
              FROM
            (
                SELECT lh.HIST_ID, 
                       RANK() OVER(
                       ORDER BY lh.HIST_ID DESC) AS hist_rank
                  FROM dbo.list_history lh
            ) pha
             WHERE pha.hist_rank > @history_row
        );
    END;