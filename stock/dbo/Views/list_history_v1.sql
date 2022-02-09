CREATE VIEW list_history_v1
AS
     SELECT lh.HIST_ID, 
            lh.HIST_DTTM, 
            lh.START_DTTM, 
            lh.END_DTTM, 
            lh.PASS_LIST, 
            lh.FILTER_OPTION, 
            lh.att1, 
            lh.att2, 
            lh.att3, 
            lh.att4, 
            lh.att5, 
            lh.option_desc, 
            ph.ranking, 
            ph.ratio1, 
            ph.ratio2, 
            ph.rate_5, 
            ph.rate_10, 
            ph.rate_15, 
            ph.rate_20, 
            ph.rate_40, 
            ph.rate_60
       FROM dbo.list_history lh
            LEFT OUTER JOIN dbo.prediction_history ph ON lh.HIST_ID = ph.hist_id
                                                         AND ph.symbol = '_TOTAL';