CREATE PROCEDURE [dbo].[process_volume](@symbol NVARCHAR(50))
AS
    BEGIN
        --First Data
        DECLARE @first FLOAT;

        SET @first =
        (
            SELECT TOP 1 
                   h.[close]
            FROM daily_history h
            WHERE h.symbol = @symbol
            ORDER BY 
                     date
        );
		
        -- create base table
        BEGIN TRY
            DROP TABLE #temp_data;
        END TRY
        BEGIN CATCH
            PRINT '##TEMP_DATA not available to drop';
        END CATCH;

        SELECT 
               [date], 
               [symbol], 
               [close] closed, 
               volume, 
               ROW_NUMBER() OVER(
               ORDER BY 
                        [date]) [row_number], 
               0 obv
        INTO 
             #temp_data
        FROM daily_history
        WHERE symbol = @symbol
        ORDER BY 
                 row_number;

        --Calculate
        DECLARE @max_row_number INT=
        (
            SELECT 
                   MAX(row_number)
            FROM #temp_data
        );
        DECLARE @current_row_number INT= 2;
        DECLARE @prev_volume FLOAT, @cur_volume FLOAT, @prev_price FLOAT, @cur_price FLOAT;

        SET @prev_price = @first;
        SET @prev_volume = 0;
        --While Loop
        WHILE @current_row_number <= @max_row_number
            BEGIN
                SELECT 
                       @cur_volume = volume
                FROM #temp_data
                WHERE row_number = @current_row_number;

                IF @prev_price < @cur_price
                    BEGIN
                        UPDATE #temp_data
                          SET 
                              obv = @prev_volume + @cur_volume
                        WHERE 
                              row_number = @current_row_number;
                END;
                    ELSE
                    IF @prev_price > @cur_price
                        BEGIN
                            UPDATE #temp_data
                              SET 
                                  obv = @prev_volume - @cur_volume
                            WHERE 
                                  row_number = @current_row_number;
                    END;
                        ELSE
                        BEGIN
                            UPDATE #temp_data
                              SET 
                                  obv = @prev_volume + @cur_volume
                            WHERE 
                                  row_number = @current_row_number;
                    END;

                SET @prev_volume = @cur_volume;
                SET @prev_price = @cur_price;
                SET @current_row_number = @current_row_number + 1;
            END;

        --Delete Table
        DELETE daily_volume
        WHERE 
              symbol = @symbol;

        --
        INSERT INTO daily_volume
               SELECT 
                      symbol, 
                      [date], 
                      obv, 
                      0
               FROM #temp_data;
    END;