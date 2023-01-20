USE [NPOSPARCK]
GO

/****** Object:  Table [dbo].[tests]    Script Date: 19.12.2022 19:08:25 ******/
DROP TABLE [dbo].[tests]
GO

/****** Object:  Table [dbo].[tests]    Script Date: 19.12.2022 19:08:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tests](
	[testid] [int] IDENTITY(1,1) NOT NULL,
	[testdate] [smalldatetime] NOT NULL,
	[blockname] [nvarchar](50) NOT NULL,
	[note] [nvarchar](200) NULL
) ON [PRIMARY]

GO


