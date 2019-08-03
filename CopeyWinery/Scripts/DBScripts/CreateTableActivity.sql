USE [model]
GO
--************LANE**********************
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Lane](
	[Id_lane] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[plant_quantity] [int]
PRIMARY KEY CLUSTERED 
(
	[Id_lane] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




--***************Location************************

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Location](
	[Id_location] [int]  IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id_location] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--************************[Location_lane_Xref]****************************


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Location_lane_Xref](
	[Id_location_lane] [int] IDENTITY(1,1) NOT NULL,
	[Id_location] [int] FOREIGN KEY REFERENCES [Location](Id_location),
	[Id_lane] [int] FOREIGN KEY REFERENCES [Lane](Id_lane)

PRIMARY KEY CLUSTERED 
(
	[Id_location_lane] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



--********************Labor****************************

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Labor](
	[Id_labor] [int]  IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id_labor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



--*******************************Activity***********************

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Activity](
	[Activity_Id] [int] IDENTITY(1,1) NOT NULL,
	[Activity_name] [varchar](255) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Activity_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



--*******************************Task***********************

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Task](
	[Task_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Date] [DateTime],
	[Number_hours] [int],
	[Hour_type] [varchar] (2),
	[Quantity] [int],
	[Unit] [varchar](10),
	[Activity] [int] FOREIGN KEY REFERENCES [Activity](Activity_Id),
	[Labor] [int] FOREIGN KEY REFERENCES [Labor](Id_Labor),
	[Location] [int] FOREIGN KEY REFERENCES [Location](Id_Location),
	[Lane] [int] FOREIGN KEY REFERENCES [Lane](Id_Lane),
PRIMARY KEY CLUSTERED 
(
	[Task_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO





