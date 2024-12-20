USE [EmployeeDB]
GO
/****** Object:  Table [dbo].[City]    Script Date: 17/12/2024 12:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] NOT NULL,
	[Name] [varchar](250) NULL,
	[StateId] [int] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 17/12/2024 12:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] NOT NULL,
	[Name] [varchar](250) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 17/12/2024 12:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](250) NULL,
	[Mobile] [varchar](20) NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
	[TextAbout] [varchar](250) NULL,
	[PhotoAddress] [varchar](max) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (1, N'Sonipat', 5)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (2, N'Gurugram', 5)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (3, N'Amritsar', 1)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (4, N'Jalandhar', 1)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (5, N'Haridwar', 2)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (6, N'Roorkee', 2)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (7, N'Muzaffarnagar', 3)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (8, N'Meerut', 3)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (9, N'Mandi', 4)
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (10, N'Dharamshala', 4)
GO
INSERT [dbo].[State] ([Id], [Name]) VALUES (1, N'Punjab')
INSERT [dbo].[State] ([Id], [Name]) VALUES (2, N'Uttrakhand')
INSERT [dbo].[State] ([Id], [Name]) VALUES (3, N'Uttar Pradesh')
INSERT [dbo].[State] ([Id], [Name]) VALUES (4, N'Himachal')
INSERT [dbo].[State] ([Id], [Name]) VALUES (5, N'Hariyana')
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [Name], [Email], [Mobile], [StateId], [CityId], [TextAbout], [PhotoAddress]) VALUES (2, N'Gaurav Rajput', N'Gaurav@gmail.com', N'9876543621', 2, 6, N'Hi ', N'/Content/PSP-4111_aac01157-6e9a-4c2f-9545-bfa60b02b542.jpg')
INSERT [dbo].[Student] ([Id], [Name], [Email], [Mobile], [StateId], [CityId], [TextAbout], [PhotoAddress]) VALUES (3, N'Saurabh Kumar', N'Surabh@gmail.com', N'9875642130', 1, 3, N'Hi Saurabh this Side', N'/Content/Eklavya_58f73206-6af8-44e7-95ee-59240beca152.jpeg')
INSERT [dbo].[Student] ([Id], [Name], [Email], [Mobile], [StateId], [CityId], [TextAbout], [PhotoAddress]) VALUES (4, N'Nitish Chauhan', N'reportnitishvats@gmail.com', N'8791165298', 1, 3, N'Hi THis is nitish', N'/Content/NitishPic1_a1875589-1340-4be3-a052-99d341924000.jpeg')
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_State]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_City]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_State]
GO
