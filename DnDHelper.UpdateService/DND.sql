CREATE TABLE [dbo].[DnDEntities](
	[Id] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[LastChange] [bigint] NOT NULL,
	[DnDRepositoryId] [int] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_dbo.DnDEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DnDRepositories]    Script Date: 2014-10-25 18:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DnDRepositories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.DnDRepositories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DnDSettings]    Script Date: 2014-10-25 18:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DnDSettings](
	[Id] [nvarchar](128) NOT NULL,
	[StringValue] [nvarchar](max) NULL,
	[IntValue] [int] NULL,
	[LongValue] [bigint] NULL,
 CONSTRAINT [PK_dbo.DnDSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_DnDRepositoryId]    Script Date: 2014-10-25 18:04:03 ******/
CREATE NONCLUSTERED INDEX [IX_DnDRepositoryId] ON [dbo].[DnDEntities]
(
	[DnDRepositoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DnDEntities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DnDEntities_dbo.DnDRepositories_DnDRepositoryId] FOREIGN KEY([DnDRepositoryId])
REFERENCES [dbo].[DnDRepositories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DnDEntities] CHECK CONSTRAINT [FK_dbo.DnDEntities_dbo.DnDRepositories_DnDRepositoryId]
GO
