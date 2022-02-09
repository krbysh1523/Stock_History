CREATE PROCEDURE [dbo].[filter_main]
(
     @start_dttm      DATETIME, 
     @end_dttm        DATETIME, 
     @option          INT           = 1, 
     @passing_list    NVARCHAR(1)   = 'N', --Passing Symbol List from UI
     @passing_symbols NVARCHAR(MAX), 
     @att1            NVARCHAR(250), 
     @att2            NVARCHAR(250), 
     @att3            NVARCHAR(250), 
     @att4            NVARCHAR(250), 
     @att5            NVARCHAR(250), 
     @is_simul        NVARCHAR(1)   = 'N'
)
AS
    BEGIN
        PRINT 'Delete Temp Table';
        --Empty Data
        DELETE FROM dbo.prediction;

        --***********************************
        --필터를 이용해서 종목을 선정
        --***********************************
        IF @passing_list = 'Y'
            BEGIN
                PRINT 'Add by Passing';
                INSERT INTO prediction(symbol)
                SELECT value
                  FROM STRING_SPLIT(@passing_symbols, ',');
        END;
        PRINT 'Rank Option ' + CAST(@option AS NVARCHAR(10));
        --***********************************
        --Rank 선정
        --***********************************
        DECLARE 
               @sp_rank NVARCHAR(1000);
        SET @sp_rank = 'exec dbo.option_' + format(@option, '00') + ' @start_dttm, @end_dttm, @passing_list, @att1, @att2, @att3, @att4, @att5';
        EXEC sp_executesql @sp_rank, 
                           N'@start_dttm datetime,@end_dttm datetime,@passing_list nvarchar(1),@att1 nvarchar(250),@att2 nvarchar(250),@att3 nvarchar(250),@att4 nvarchar(250),@att5 nvarchar(250)', 
                           @start_dttm = @start_dttm, 
                           @end_dttm = @end_dttm, 
                           @passing_list = @passing_list, 
                           @att1 = @att1, 
                           @att2 = @att2, 
                           @att3 = @att3, 
                           @att4 = @att4, 
                           @att5 = @att5;
        GOTO simulation;
        Simulation:
        --***********************************
        --Simulation
        --***********************************
        IF @is_simul = 'Y'
            BEGIN
                PRINT 'Simulation...';
                EXEC dbo.filter_simul @start_dttm = @start_dttm, 
                                      @end_dttm = @end_dttm;
        END;
        PRINT 'Save to History';
        DECLARE 
               @hist_id INT;
        EXEC dbo.sp_add_list_history @start_dttm, 
                                     @end_dttm, 
                                     @option, 
                                     @passing_list, 
                                     @att1, 
                                     @att2, 
                                     @att3, 
                                     @att4, 
                                     @att5, 
                                     @hist_id OUTPUT;
        INSERT INTO dbo.prediction_history
        SELECT @hist_id, 
               p.*
          FROM dbo.prediction p;
        EXEC dbo.sp_prediction_history_cleanup;
        PRINT 'Display List with Ranking';
        SELECT *
          FROM dbo.prediction p
        ORDER BY p.ranking;
    END;