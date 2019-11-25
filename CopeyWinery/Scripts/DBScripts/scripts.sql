USE [admin1_cw]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 9/27/2019 10:45:35 AM ******/
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
/****** Object:  Table [dbo].[ExtendedAttribute]    Script Date: 9/27/2019 10:45:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtendedAttribute](
	[Id_ExtAttr] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_ExtAttr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Labor]    Script Date: 9/27/2019 10:45:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Labor](
	[Id_labor] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Id_ExtAttr] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_labor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 9/27/2019 10:45:36 AM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 9/27/2019 10:45:36 AM ******/
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
/****** Object:  Table [dbo].[Task]    Script Date: 9/27/2019 10:45:36 AM ******/
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
	[UserId] [int] NOT NULL,
	[Activity_Id] [int] NOT NULL,
	[Id_labor] [int] NOT NULL,
	[Id_location] [int] NOT NULL,
	[Ext_Attr_Labor_Value] [varchar] (50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Task_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActivation]    Script Date: 9/27/2019 10:45:36 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 9/27/2019 10:45:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](30) NULL,
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
INSERT [dbo].[Activity] ([Activity_Id], [Activity_name]) VALUES (6, N'Uvas')
INSERT [dbo].[Activity] ([Activity_Id], [Activity_name]) VALUES (11, N'Caminos')
SET IDENTITY_INSERT [dbo].[Activity] OFF
SET IDENTITY_INSERT [dbo].[ExtendedAttribute] ON 

INSERT [dbo].[ExtendedAttribute] ([Id_ExtAttr], [Name]) VALUES (1, N'Numero calle')
INSERT [dbo].[ExtendedAttribute] ([Id_ExtAttr], [Name]) VALUES (2, N'Numero de plantas')
INSERT [dbo].[ExtendedAttribute] ([Id_ExtAttr], [Name]) VALUES (1003, N'Litros')
SET IDENTITY_INSERT [dbo].[ExtendedAttribute] OFF
SET IDENTITY_INSERT [dbo].[Labor] ON 

INSERT [dbo].[Labor] ([Id_labor], [Name], [Id_ExtAttr]) VALUES (3, N'Deshierba', 1)
INSERT [dbo].[Labor] ([Id_labor], [Name], [Id_ExtAttr]) VALUES (6, N'Poda', NULL)
INSERT [dbo].[Labor] ([Id_labor], [Name], [Id_ExtAttr]) VALUES (1002, N'Chapia', 1)
SET IDENTITY_INSERT [dbo].[Labor] OFF
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([Id_location], [Name]) VALUES (1, N'lote 999')
INSERT [dbo].[Location] ([Id_location], [Name]) VALUES (2, N'Lote1')
INSERT [dbo].[Location] ([Id_location], [Name]) VALUES (5, N'Lote 8')
INSERT [dbo].[Location] ([Id_location], [Name]) VALUES (6, N'Lote 98')
SET IDENTITY_INSERT [dbo].[Location] OFF
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'User')

SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [UserId], [Activity_Id], [Id_labor], [Id_location], [Ext_Attr_Labor_Value]) VALUES (2, N'0', CAST(N'2019-09-04T00:00:00.000' AS DateTime), 7, N'Ordinaria', 1, 1, 3, 1, NULL)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [UserId], [Activity_Id], [Id_labor], [Id_location], [Ext_Attr_Labor_Value]) VALUES (3, N'0', CAST(N'2019-09-05T00:00:00.000' AS DateTime), 8, N'Ordinaria', 1, 1, 3, 1, 8)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [UserId], [Activity_Id], [Id_labor], [Id_location], [Ext_Attr_Labor_Value]) VALUES (2002, N'0', CAST(N'2019-09-20T00:00:00.000' AS DateTime), 8, N'Ordinaria', 7, 6, 3, 1, 89)
INSERT [dbo].[Task] ([Task_Id], [Name], [Date], [Number_hours], [Hour_type], [UserId], [Activity_Id], [Id_labor], [Id_location], [Ext_Attr_Labor_Value]) VALUES (2003, N'0', CAST(N'2019-09-03T00:00:00.000' AS DateTime), 6, N'Ordinaria', 1, 1, 3, 5, 8)
SET IDENTITY_INSERT [dbo].[Task] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [Email], [CreatedDate], [LastLoginDate], [RoleId]) VALUES (1, N'ange', N'12345', N'Angelica', N'mudassar@aspsnippets.com', CAST(N'2019-09-02T17:57:32.193' AS DateTime), CAST(N'2019-09-22T17:19:05.780' AS DateTime), 1)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [Email], [CreatedDate], [LastLoginDate], [RoleId]) VALUES (7, N'andres', N'123456', N'Andres Serrano', NULL, CAST(N'2019-09-22T22:58:43.000' AS DateTime), CAST(N'2019-09-22T17:00:20.893' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Labor]  WITH CHECK ADD  CONSTRAINT [FK_Labor_Ext] FOREIGN KEY([Id_ExtAttr])
REFERENCES [dbo].[ExtendedAttribute] ([Id_ExtAttr])
GO
ALTER TABLE [dbo].[Labor] CHECK CONSTRAINT [FK_Labor_Ext]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Activity] FOREIGN KEY([Activity_Id])
REFERENCES [dbo].[Activity] ([Activity_Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Activity]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Labor] FOREIGN KEY([Id_labor])
REFERENCES [dbo].[Labor] ([Id_labor])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Labor]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Location] FOREIGN KEY([Id_location])
REFERENCES [dbo].[Location] ([Id_location])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Location]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO

CREATE NONCLUSTERED INDEX IX_Task_TaskID
ON [dbo].[Task] (Task_Id)
GO
/****** Object:  StoredProcedure [dbo].[Insert_User]    Script Date: 9/27/2019 10:45:38 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Validate_User]    Script Date: 9/27/2019 10:45:38 AM ******/
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
