USE [NPOSPARCK]
GO

/****** Object:  Table [dbo].[parameters]    Script Date: 19.12.2022 19:06:30 ******/
DROP TABLE [dbo].[parameters]
GO

/****** Object:  Table [dbo].[parameters]    Script Date: 19.12.2022 19:06:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[parameters](
	[parameterid] [int] IDENTITY(1,1) NOT NULL,
	[testid] [int] NOT NULL,
	[parametername] [nvarchar](200) NOT NULL,
	[requiredvalue] [decimal](18, 3) NOT NULL,
	[measuredvalue] [decimal](18, 3) NOT NULL
) ON [PRIMARY]

GO


