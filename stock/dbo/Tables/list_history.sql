CREATE TABLE [dbo].[list_history] (
    [HIST_ID]       INT            IDENTITY (1, 1) NOT NULL,
    [HIST_DTTM]     DATETIME       NOT NULL,
    [START_DTTM]    DATE           NOT NULL,
    [END_DTTM]      DATE           NOT NULL,
    [PASS_LIST]     CHAR (1)       NOT NULL,
    [FILTER_OPTION] INT            NOT NULL,
    [att1]          NVARCHAR (250) NULL,
    [att2]          NVARCHAR (250) NULL,
    [att3]          NVARCHAR (250) NULL,
    [att4]          NVARCHAR (250) NULL,
    [att5]          NVARCHAR (250) NULL,
    [option_desc]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_LIST_HISTORY] PRIMARY KEY CLUSTERED ([HIST_ID] ASC)
);

