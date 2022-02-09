CREATE TABLE [dbo].[test_plan] (
    [test_id]       INT            IDENTITY (1, 1) NOT NULL,
    [plan_name]     NVARCHAR (250) NOT NULL,
    [test_seq]      INT            NOT NULL,
    [pass_list]     CHAR (1)       NOT NULL,
    [filter_option] INT            NOT NULL,
    [filter_att1]   NVARCHAR (250) NULL,
    [filter_att2]   NVARCHAR (250) NULL,
    [filter_att3]   NVARCHAR (250) NULL,
    [filter_att4]   NVARCHAR (250) NULL,
    [filter_att5]   NVARCHAR (250) NULL,
    [filter_desc]   NVARCHAR (250) NULL,
    CONSTRAINT [PK_test_plan] PRIMARY KEY CLUSTERED ([test_id] ASC)
);

