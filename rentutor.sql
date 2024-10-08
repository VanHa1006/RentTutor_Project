
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 8/23/2024 7:51:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryID] [int] NOT NULL,
	[TutorID] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[Image] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
	[LinkVideo] [nvarchar](max) NULL,
	[Hours] [int] NULL,
	[DiscountPrice] [decimal](18, 2) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 8/23/2024 7:51:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 8/23/2024 7:51:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/23/2024 7:51:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[OrderDate] [datetime] NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 8/23/2024 7:51:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tutors]    Script Date: 8/23/2024 7:51:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tutors](
	[TutorID] [int] NOT NULL,
	[Qualifications] [nvarchar](255) NULL,
	[Experience] [nvarchar](255) NULL,
	[Specialization] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[TutorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserApprovalLogs]    Script Date: 8/23/2024 7:51:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserApprovalLogs](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[TutorID] [int] NOT NULL,
	[AdminID] [int] NOT NULL,
	[Decision] [nvarchar](50) NOT NULL,
	[DecisionDate] [datetime] NULL,
	[Reason] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/23/2024 7:51:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](255) NULL,
	[Birthday] [date] NULL,
	[FullName] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (58, N'Web Development', N'Web Development có thể được chia thành ba hạng mục chính: Client-side (Frontend), Server-side (Backend) và Công nghệ cơ sở dữ liệu. ')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (59, N'Mobile Development', N'Focused on the creation of applications for mobile devices, including smartphones and tablets. It includes Android and iOS development, using language')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (19, N'ReactJS là gì?', N'ReactJS là một opensource được phát triển bởi Facebook, ra mắt vào năm 2013, bản thân nó là một thư viện Javascript được dùng để để xây dựng các tương tác với các thành phần trên website.', 58, 65, CAST(N'2024-08-22T21:36:12.717' AS DateTime), CAST(N'2024-08-22T21:36:12.717' AS DateTime), N'https://statics.cdn.200lab.io/2023/09/02-Tempalte--5-.png', N'Active', N'https://www.youtube.com/embed/46DuTN1qsIE?si=nPCU95bKa1VhtWFG', 3, CAST(199000.00 AS Decimal(18, 2)), CAST(299000.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (20, N'What is Node.js?', N'Node.js is an open source server environmentNode.js is freeNode.js runs on various platforms (Windows, Linux, Unix, Mac OS X, etc.)Node.js uses JavaScript on the server', 58, 65, CAST(N'2024-08-22T21:38:09.110' AS DateTime), CAST(N'2024-08-22T21:38:09.110' AS DateTime), N'https://media.licdn.com/dms/image/D4D12AQF1btzrvx64aQ/article-cover_image-shrink_720_1280/0/1698242399109?e=2147483647&v=beta&t=_OkJ8ffsiTF8-_AxICCpZXT176KkvOziJb0uKDDk3r0', N'Active', N'https://www.youtube.com/embed/7ThLutk-wh4?si=ukscpVJ0oSaZTvaL', 5, CAST(199000.00 AS Decimal(18, 2)), CAST(289000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice], [Status]) VALUES (161, 115, 20, CAST(289000.00 AS Decimal(18, 2)), 1, CAST(90000.00 AS Decimal(18, 2)), N'Studying')
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice], [Status]) VALUES (162, 115, 19, CAST(299000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), N'Done')
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice], [Status]) VALUES (163, 116, 19, CAST(299000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), N'Studying')
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice], [Status]) VALUES (164, 116, 20, CAST(289000.00 AS Decimal(18, 2)), 1, CAST(90000.00 AS Decimal(18, 2)), N'Done')
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice], [Status]) VALUES (165, 117, 20, CAST(289000.00 AS Decimal(18, 2)), 1, CAST(90000.00 AS Decimal(18, 2)), N'Disapprove')
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice], [Status]) VALUES (166, 117, 19, CAST(299000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), N'Disapprove')
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice], [Status]) VALUES (175, 123, 19, CAST(299000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), N'Done')
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (115, 63, CAST(N'2024-08-22T21:42:43.903' AS DateTime), CAST(190000.00 AS Decimal(18, 2)), N'Success')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (116, 63, CAST(N'2024-08-22T22:30:10.840' AS DateTime), CAST(190000.00 AS Decimal(18, 2)), N'Success')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (117, 63, CAST(N'2024-08-23T13:37:24.793' AS DateTime), CAST(190000.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (123, 103, CAST(N'2024-08-23T19:40:09.773' AS DateTime), CAST(100000.00 AS Decimal(18, 2)), N'Success')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[Students] ([StudentID]) VALUES (58)
INSERT [dbo].[Students] ([StudentID]) VALUES (63)
INSERT [dbo].[Students] ([StudentID]) VALUES (103)
GO
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (65, N'https://think.edu.vn/wp-content/uploads/2022/01/Certificate-la-gi.jpg', N'20 years', N'Mobile Development ')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (94, N'https://think.edu.vn/wp-content/uploads/2022/01/Certificate-la-gi.jpg', N'20 years', N'Website Development ')
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (11, N'admin', N'admin@example.com', N'123456', N'Admin', N'Active', CAST(N'2024-08-07T16:08:00.600' AS DateTime), CAST(N'2024-08-07T16:08:00.600' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Lê Văn Hà')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (58, N'Student', N'vanha10062000@gmail.com', N'123456', N'Student', N'Active', CAST(N'2024-08-22T20:56:28.347' AS DateTime), CAST(N'2024-08-22T20:56:28.350' AS DateTime), N'', N'', CAST(N'0001-01-01' AS Date), N'Văn Hà')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (63, N'Student', N'vajixi2525@amxyy.com', N'123456', N'Student', N'Active', CAST(N'2024-08-22T21:17:37.160' AS DateTime), CAST(N'2024-08-22T21:17:37.160' AS DateTime), N'', N'', CAST(N'0001-01-01' AS Date), N'Nguyen Van A')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (65, N'ThinhDev', N'cuongthinh5213@gmail.com', N'123456', N'Tutor', N'Active', CAST(N'2024-08-22T21:27:42.343' AS DateTime), CAST(N'2024-08-22T21:27:42.000' AS DateTime), N'0938291478', N'Cu Chi District ', CAST(N'2002-04-19' AS Date), N'Nguyen Cuong Thinh')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (94, N'demo', N'nguyencuongthinh1478@gmail.com', N'123456', N'Tutor', N'Active', CAST(N'2024-08-23T19:17:18.600' AS DateTime), CAST(N'2024-08-23T19:19:57.900' AS DateTime), N'0958694136', N'Dong Thap', CAST(N'2002-04-12' AS Date), N'Nguyen Cuong Thinh')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (103, N'Student', N'settmistyfvne198@gmail.com', N'123456', N'Student', N'Active', CAST(N'2024-08-23T19:37:21.513' AS DateTime), CAST(N'2024-08-23T19:37:21.513' AS DateTime), N'', N'', CAST(N'0001-01-01' AS Date), N'Nguyen Van A')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Courses] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Courses] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Courses] ADD  DEFAULT ((0.00)) FOR [DiscountPrice]
GO
ALTER TABLE [dbo].[Courses] ADD  DEFAULT ((0.00)) FOR [Price]
GO
ALTER TABLE [dbo].[Feedback] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [TotalPrice]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[UserApprovalLogs] ADD  DEFAULT (getdate()) FOR [DecisionDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Role]  DEFAULT ('DefaultRole') FOR [Role]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD FOREIGN KEY([TutorID])
REFERENCES [dbo].[Tutors] ([TutorID])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([StudentID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([StudentID])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([StudentID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Tutors]  WITH CHECK ADD FOREIGN KEY([TutorID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserApprovalLogs]  WITH CHECK ADD FOREIGN KEY([AdminID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserApprovalLogs]  WITH CHECK ADD FOREIGN KEY([TutorID])
REFERENCES [dbo].[Tutors] ([TutorID])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD CHECK  (([Rating]>=(1) AND [Rating]<=(5)))
GO
USE [master]
GO
ALTER DATABASE [RenTurtorToStudent] SET  READ_WRITE 
GO
