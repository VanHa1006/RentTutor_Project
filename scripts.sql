USE [master]
GO
/****** Object:  Database [RenTurtorToStudent]    Script Date: 8/6/2024 8:17:18 PM ******/
CREATE DATABASE [RenTurtorToStudent]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RenTurtorToStudent', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\RenTurtorToStudent.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RenTurtorToStudent_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\RenTurtorToStudent_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [RenTurtorToStudent] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RenTurtorToStudent].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RenTurtorToStudent] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET ARITHABORT OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [RenTurtorToStudent] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RenTurtorToStudent] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RenTurtorToStudent] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RenTurtorToStudent] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RenTurtorToStudent] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RenTurtorToStudent] SET  MULTI_USER 
GO
ALTER DATABASE [RenTurtorToStudent] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RenTurtorToStudent] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RenTurtorToStudent] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RenTurtorToStudent] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RenTurtorToStudent] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RenTurtorToStudent] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RenTurtorToStudent] SET QUERY_STORE = ON
GO
ALTER DATABASE [RenTurtorToStudent] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RenTurtorToStudent]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 8/6/2024 8:17:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
/****** Object:  Table [dbo].[Courses]    Script Date: 8/6/2024 8:17:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[TutorID] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 8/6/2024 8:17:18 PM ******/
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
	[TotalPrice]  AS ([UnitPrice]*[Quantity]),
PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/6/2024 8:17:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[OrderDate] [datetime] NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserApprovalLogs]    Script Date: 8/6/2024 8:17:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserApprovalLogs](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Users]    Script Date: 8/6/2024 8:17:18 PM ******/
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
	[Qualifications] [nvarchar](255) NULL,
	[Experience] [nvarchar](255) NULL,
	[Specialization] [nvarchar](255) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[FullName] [nvarchar](255) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](500) NULL,
	[Birthday] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [Qualifications], [Experience], [Specialization], [CreatedAt], [UpdatedAt], [FullName], [Phone], [Address], [Birthday]) VALUES (1, N'AdminUser', N'admin@example.com', N'hashed_password_here', N'Admin', N'Active', NULL, NULL, NULL, CAST(N'2024-08-06T13:28:31.437' AS DateTime), CAST(N'2024-08-06T13:28:31.437' AS DateTime), N'Lê Văn Hà', N'0364463482', N'11 Thắng Lợi, Quận , Thành Phố Hồ Chí Minh', CAST(N'2000-11-11' AS Date))
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [Qualifications], [Experience], [Specialization], [CreatedAt], [UpdatedAt], [FullName], [Phone], [Address], [Birthday]) VALUES (2, N'StudentUser', N'student@example.com', N'hashed_password_here', N'Student', N'Active', NULL, NULL, NULL, CAST(N'2024-08-06T13:28:31.437' AS DateTime), CAST(N'2024-08-06T13:28:31.437' AS DateTime), N'Nguyễn Cường Thịnh', N'0123456789', N'33 Xô Viết, Quận Bình Thạnh Thành Phố Hồ Chí Minh', CAST(N'2003-12-10' AS Date))
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [Qualifications], [Experience], [Specialization], [CreatedAt], [UpdatedAt], [FullName], [Phone], [Address], [Birthday]) VALUES (3, N'TutorUser', N'tutor@example.com', N'hashed_password_here', N'Tutor', N'Active', N'Bachelor of Science', N'5 years teaching', N'Mathematics', CAST(N'2024-08-06T13:28:31.437' AS DateTime), CAST(N'2024-08-06T13:28:31.437' AS DateTime), N'Is Tutor', N'0123456789', N'65 Lê Văn Chí, Thành Phố Thủ Đức', CAST(N'2002-10-11' AS Date))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Courses] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Courses] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[UserApprovalLogs] ADD  DEFAULT (getdate()) FOR [DecisionDate]
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
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserApprovalLogs]  WITH CHECK ADD FOREIGN KEY([AdminID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserApprovalLogs]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
USE [master]
GO
ALTER DATABASE [RenTurtorToStudent] SET  READ_WRITE 
GO
