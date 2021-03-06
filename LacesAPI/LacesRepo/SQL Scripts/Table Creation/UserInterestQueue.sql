﻿CREATE TABLE [dbo].[UserInterestQueue] (
	  [UserInterestQueueId]	[int]		IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [UserId]				[int]		NOT NULL
	, [ProductId]			[int]		NOT NULL
	, [Interested]			[bit]		NOT NULL
	, [CreatedDate]			[datetime]	NOT NULL
	, [UpdatedDate]			[datetime]	NOT NULL
  CONSTRAINT [PK_UserInterestQueue] PRIMARY KEY CLUSTERED
(
	[UserInterestQueueId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO