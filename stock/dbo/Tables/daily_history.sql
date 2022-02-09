CREATE TABLE [dbo].[daily_history] (
    [symbol] NVARCHAR (50) NOT NULL,
    [date]   DATE          NOT NULL,
    [open]   FLOAT (53)    NULL,
    [close]  FLOAT (53)    NULL,
    [high]   FLOAT (53)    NULL,
    [low]    FLOAT (53)    NULL,
    [volume] FLOAT (53)    NULL,
    [avg]    FLOAT (53)    NULL,
    [seq]    AS            (concat([symbol],'_',CONVERT([varchar],[date],(112)))),
    CONSTRAINT [PK_daily] PRIMARY KEY CLUSTERED ([symbol] ASC, [date] ASC)
);

