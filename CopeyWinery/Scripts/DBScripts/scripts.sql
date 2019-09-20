USE [model]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 9/19/2019 4:00:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[Activity_Id] [int] IDENTITY(1,1) NOT NULL,
	[Activity_name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Activity_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Labor]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Labor](
	[Id_labor] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_labor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lane]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lane](
	[Id_lane] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[plant_quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_lane] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id_location] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_location] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location_lane_Xref]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location_lane_Xref](
	[Id_location_lane] [int] IDENTITY(1,1) NOT NULL,
	[Id_location] [int] NULL,
	[Id_lane] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_location_lane] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Task_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Date] [datetime] NULL,
	[Number_hours] [int] NULL,
	[Hour_type] [varchar](15) NULL,
	[Quantity] [int] NULL,
	[Unit] [varchar](10) NULL,
	[User] [int] NULL,
	[Activity] [int] NULL,
	[Labor] [int] NULL,
	[Location] [int] NULL,
	[Lane] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Task_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActivation]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActivation](
	[UserId] [int] NOT NULL,
	[ActivationCode] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserActivation] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Activity] ON 

INSERT [dbo].[Activity] ([Activity_Id], [Activity_name]) VALUES (1, N'Uchuva')
INSERT [dbo].[Activity] ([Activity_Id], [Activity_name]) VALUES (2, N'Uva')
SET IDENTITY_INSERT [dbo].[Activity] OFF
SET IDENTITY_INSERT [dbo].[Labor] ON 

INSERT [dbo].[Labor] ([Id_labor], [Name]) VALUES (1, N'Poda')
INSERT [dbo].[Labor] ([Id_labor], [Name]) VALUES (2, N'Chapia')
SET IDENTITY_INSERT [dbo].[Labor] OFF
SET IDENTITY_INSERT [dbo].[Lane] ON 

INSERT [dbo].[Lane] ([Id_lane], [Name], [plant_quantity]) VALUES (1, N'calle 0', 9)
INSERT [dbo].[Lane] ([Id_lane], [Name], [plant_quantity]) VALUES (2, N'calle1', 4)
SET IDENTITY_INSERT [dbo].[Lane] OFF
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([Id_location], [Name]) VALUES (1, N'lote5')
INSERT [dbo].[Location] ([Id_location], [Name]) VALUES (2, N'Lote1')
SET IDENTITY_INSERT [dbo].[Location] OFF
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (1, N'0', CAST(N'2019-09-18T00:00:00.000' AS DateTime), 69, N'Extraordinaria', NULL, NULL, 2, 1, 1, 2, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (3, N'0', CAST(N'9999-09-10T00:00:00.000' AS DateTime), 9, N'Extraordinaria', NULL, NULL, NULL, 2, 2, 2, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (4, N'0', CAST(N'9999-09-10T00:00:00.000' AS DateTime), 9, N'Extraordinaria', NULL, NULL, NULL, 2, 2, 2, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (5, N'0', CAST(N'9999-09-09T00:00:00.000' AS DateTime), 9, N'Ordinaria', NULL, NULL, 2, 1, 1, NULL, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (6, N'0', CAST(N'4444-04-04T00:00:00.000' AS DateTime), 3, N'Ordinaria', NULL, NULL, 1, 1, 1, 1, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (7, N'0', CAST(N'4444-04-04T00:00:00.000' AS DateTime), 6, N'Ordinaria', NULL, NULL, NULL, 1, 1, 1, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (9, N'0', CAST(N'9999-09-09T00:00:00.000' AS DateTime), 9, N'Ordinaria', NULL, NULL, 2, 1, 2, 2, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (10, N'0', CAST(N'4444-04-04T00:00:00.000' AS DateTime), 5, N'Ordinaria', NULL, NULL, 1, 1, 1, 1, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (11, N'0', CAST(N'9999-09-09T00:00:00.000' AS DateTime), 4, N'Ordinaria', NULL, NULL, 1, 1, 1, 1, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (12, N'0', CAST(N'2019-09-11T00:00:00.000' AS DateTime), 8, N'Ordinaria', NULL, NULL, 1, 1, 1, 1, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (13, N'0', CAST(N'2019-09-06T00:00:00.000' AS DateTime), 7, N'Ordinaria', NULL, NULL, 1, 1, 1, 1, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (1002, N'0', CAST(N'2019-09-04T00:00:00.000' AS DateTime), 6, N'Extraordinaria', NULL, NULL, 1, 2, 2, 2, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (1003, N'0', CAST(N'2019-09-11T00:00:00.000' AS DateTime), 4, N'Ordinaria', NULL, NULL, 1, 1, 1, 1, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [Quantity], [Unit], [User], [Activity], [Labor], [Location], [Lane]) VALUES (1004, N'0', CAST(N'2019-09-02T00:00:00.000' AS DateTime), 5, N'Ordinaria', NULL, NULL, 1, 1, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[Task] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [CreatedDate], [LastLoginDate], [RoleId]) VALUES (1, N'Admin', N'12345', N'mudassar@aspsnippets.com', CAST(N'2019-09-02T17:57:32.193' AS DateTime), CAST(N'2019-09-19T01:42:02.207' AS DateTime), 1)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [CreatedDate], [LastLoginDate], [RoleId]) VALUES (2, N'Mudassar', N'12345', N'mudassar@aspsnippets.com', CAST(N'2019-09-02T17:57:32.193' AS DateTime), CAST(N'2019-09-19T00:00:06.610' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Location_lane_Xref]  WITH CHECK ADD FOREIGN KEY([Id_lane])
REFERENCES [dbo].[Lane] ([Id_lane])
GO
ALTER TABLE [dbo].[Location_lane_Xref]  WITH CHECK ADD FOREIGN KEY([Id_location])
REFERENCES [dbo].[Location] ([Id_location])
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([Activity])
REFERENCES [dbo].[Activity] ([Activity_Id])
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([Labor])
REFERENCES [dbo].[Labor] ([Id_labor])
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([Lane])
REFERENCES [dbo].[Lane] ([Id_lane])
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([Location])
REFERENCES [dbo].[Location] ([Id_location])
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
/****** Object:  StoredProcedure [dbo].[Insert_User]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Insert_User 'Mudassar2', '12345', 'mudassar@aspsnippets.com'
CREATE PROCEDURE [dbo].[Insert_User]
	@Username NVARCHAR(20),
	@Password NVARCHAR(20),
	@Email NVARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT UserId FROM Users WHERE Username = @Username)
	BEGIN
		SELECT -1 -- Username exists.
	END
	ELSE IF EXISTS(SELECT UserId FROM Users WHERE Email = @Email)
	BEGIN
		SELECT -2 -- Email exists.
	END
	ELSE
	BEGIN
		INSERT INTO [Users]
			   ([Username]
			   ,[Password]
			   ,[Email]
			   ,[CreatedDate])
		VALUES
			   (@Username
			   ,@Password
			   ,@Email
			   ,GETDATE())
		
		SELECT SCOPE_IDENTITY() -- UserId			   
     END
END

GO
/****** Object:  StoredProcedure [dbo].[Validate_User]    Script Date: 9/19/2019 4:00:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[Validate_User] 'Mudassar', '12345'
CREATE  PROCEDURE [dbo].[Validate_User]
	@Username NVARCHAR(20),
	@Password NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @UserId INT, @LastLoginDate DATETIME, @RoleId INT
	
	SELECT @UserId = UserId, @LastLoginDate = LastLoginDate, @RoleId = RoleId 
	FROM Users WHERE Username = @Username AND [Password] = @Password
	
	IF @UserId IS NOT NULL
	BEGIN
		IF NOT EXISTS(SELECT UserId FROM UserActivation WHERE UserId = @UserId)
		BEGIN
			UPDATE Users
			SET LastLoginDate =  GETDATE()
			WHERE UserId = @UserId
			
			SELECT @UserId [UserId], 
					(SELECT RoleName FROM Roles 
					 WHERE RoleId = @RoleId) [Roles] -- User Valid
		END
		ELSE
		BEGIN
			SELECT -2 [UserId], '' [Roles]-- User not activated.
		END
	END
	ELSE
	BEGIN
		SELECT -1 [UserId], '' [Roles] -- User invalid.
	END
END
GO
