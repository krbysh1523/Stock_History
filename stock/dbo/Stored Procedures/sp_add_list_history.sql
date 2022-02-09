CREATE PROCEDURE [dbo].[sp_add_list_history]
(
     @start_dttm   DATETIME, 
     @end_dttm     DATETIME, 
     @option       INT, 
     @passing_list NVARCHAR(1), 
     @att1         NVARCHAR(250), 
     @att2         NVARCHAR(250), 
     @att3         NVARCHAR(250), 
     @att4         NVARCHAR(250), 
     @att5         NVARCHAR(250), 
     @hist_id      INT OUTPUT
)
AS
    BEGIN
        DECLARE 
               @option_desc NVARCHAR(250);
        SELECT @option_desc = l.l_name
          FROM dbo.lookup l
         WHERE l.l_type = 'Option'
               AND CAST(l.l_id AS INT) = @option;
        INSERT INTO dbo.list_history
        (HIST_DTTM, 
         START_DTTM, 
         END_DTTM, 
         PASS_LIST, 
         FILTER_OPTION, 
         att1, 
         att2, 
         att3, 
         att4, 
         att5, 
         option_desc
        )
        VALUES
        (
               GETDATE(), 
               @start_dttm, 
               @end_dttm, 
               @passing_list, 
               @option, 
               @att1, 
               @att2, 
               @att3, 
               @att4, 
               @att5, 
               @option_desc
        );
        SET @hist_id = SCOPE_IDENTITY();
    END;