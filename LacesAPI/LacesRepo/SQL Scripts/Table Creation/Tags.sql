﻿CREATE TABLE [dbo].[Tags] (
	  [TagId]		[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [ProductId]	[int]			NOT NULL
	, [Description]	[varchar](40)	NOT NULL
  CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED
(
	[TagId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO