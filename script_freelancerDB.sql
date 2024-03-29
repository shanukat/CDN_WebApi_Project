USE [freelancers]
GO
/****** Object:  Table [dbo].[tblFreelancer]    Script Date: 1/17/2024 1:26:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblFreelancer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Mail] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Skillsets] [varchar](50) NULL,
	[Hobby] [varchar](50) NULL,
 CONSTRAINT [PK_tblFreelancer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblFreelancer] ON 

INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (1, N'Shanuka', N'shanukato@gmail.com', N'+601112262178', N'C#, ASP.NET', N'Chess')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (2, N'Gayani Chathu', N'gCha@gmail.com', N'+601232243123', N'php, drupal', N'reading')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (3, N'Rehansa', N'rehansa@gmail.com', N'01123411', N'javaScripy', N'Dancing')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (8, N'Arun Piyuimakl ', N'A12@gmail.com', N'+91239999233', N'Pythen, Microservices', N'Ball')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (11, N'Ruchira Perera', N'fddff.doe@example.com', N'+699221111111', N'C#, ASP.NET Core', N'HUB Bank')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (12, N'JohnDoe', N'john.doe@example.com', N'1234567890', N'C#, ASP.NET Core', N'Programming')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (1009, N'Thiwanka', N'thiwa@gmail.com', N'01112267123', N'C#, Asp.net, MVC', N'Playing Chess')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (1010, N'Chineesss', N'chanh@gmail.com', N'+6023332222', N'Kafka', N'Cricket')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (1012, N'11', N'abc@gmail.com', N'+601123054698', N'11', N'11')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (1013, N'wsws', N'aaaaa@gmail.com', N'+601111111111', N'11', N'11')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (1014, N'11', N'aa@gmail.com', N'+444444444444', N'11', N'11')
INSERT [dbo].[tblFreelancer] ([Id], [Username], [Mail], [PhoneNumber], [Skillsets], [Hobby]) VALUES (1016, N'Yohan', N'yha@ymail.com', N'+6011123332', N'Jquery', N'Chess')
SET IDENTITY_INSERT [dbo].[tblFreelancer] OFF
