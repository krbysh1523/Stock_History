CREATE TABLE [dbo].[lookup] (
    [l_type]  NVARCHAR (50)  NOT NULL,
    [l_id]    NVARCHAR (50)  NOT NULL,
    [l_name]  NVARCHAR (250) NULL,
    [l_order] INT            NULL,
    [l_att1]  NVARCHAR (250) NULL,
    [l_att2]  NVARCHAR (250) NULL,
    [l_att3]  NVARCHAR (250) NULL,
    [l_att4]  NVARCHAR (250) NULL,
    [l_att5]  NVARCHAR (250) NULL,
    CONSTRAINT [PK_lookup] PRIMARY KEY CLUSTERED ([l_type] ASC, [l_id] ASC)
);

