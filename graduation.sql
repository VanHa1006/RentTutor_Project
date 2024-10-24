USE [master]
GO
/****** Object:  Database [RenTurtorToStudent]    Script Date: 8/20/2024 8:10:40 PM ******/
CREATE DATABASE [RenTurtorToStudent]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RenTurtorToStudent', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RenTurtorToStudent.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RenTurtorToStudent_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RenTurtorToStudent_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [RenTurtorToStudent] SET AUTO_CLOSE OFF 
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
ALTER DATABASE [RenTurtorToStudent] SET RECOVERY FULL 
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
EXEC sys.sp_db_vardecimal_storage_format N'RenTurtorToStudent', N'ON'
GO
ALTER DATABASE [RenTurtorToStudent] SET QUERY_STORE = ON
GO
ALTER DATABASE [RenTurtorToStudent] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RenTurtorToStudent]
GO
/****** Object:  User [Sendo_Havan*127.0.0.0*1]    Script Date: 8/20/2024 8:10:40 PM ******/
CREATE USER [Sendo_Havan*127.0.0.0*1] FOR LOGIN [Sendo_Havan*127.0.0.0*1] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [Sendo_Havan*127.0.0.0*1]
GO
ALTER ROLE [db_datareader] ADD MEMBER [Sendo_Havan*127.0.0.0*1]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [Sendo_Havan*127.0.0.0*1]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [Sendo_Havan*127.0.0.0*1]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 8/20/2024 8:10:41 PM ******/
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
/****** Object:  Table [dbo].[Courses]    Script Date: 8/20/2024 8:10:41 PM ******/
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
/****** Object:  Table [dbo].[Feedback]    Script Date: 8/20/2024 8:10:41 PM ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 8/20/2024 8:10:41 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/20/2024 8:10:41 PM ******/
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
/****** Object:  Table [dbo].[Students]    Script Date: 8/20/2024 8:10:41 PM ******/
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
/****** Object:  Table [dbo].[Tutors]    Script Date: 8/20/2024 8:10:41 PM ******/
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
/****** Object:  Table [dbo].[UserApprovalLogs]    Script Date: 8/20/2024 8:10:41 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 8/20/2024 8:10:41 PM ******/
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

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (36, N'Web Development ', N'Web development is the work involved in developing a website for the Internet (World Wide Web) or an intranet (a private network).[1] ')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (37, N'Mobile Development ', N'Good')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (39, N'Programming Languages', N'Programming languages are described in terms of their syntax (form) and semantics (meaning), usually defined by a formal language. ')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (51, N'Web Development ', N'Software testing can provide objective, independent information about the quality of software and the risk of its failure to a user or sponsor')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (57, N'No-Code Development Courses', N'Explore top courses and programs in No-Code Development. Enhance your skills with expert-led lessons from industry leaders. ')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (3, N'Java', N'Description for Course 2', 36, 9, CAST(N'2024-08-12T19:58:11.607' AS DateTime), CAST(N'2024-08-12T19:58:11.607' AS DateTime), N'https://i.pinimg.com/564x/3f/f3/38/3ff338fded7cab6c231606b35ebe18ab.jpg', N'Active', N'https://www.youtube.com/embed/l9AzO1FMgM8?si=pBxvjg9hsnPUxMZT', 5, CAST(30.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (4, N'Python', N'Description for Course 3', 51, 34, CAST(N'2024-08-12T19:58:11.607' AS DateTime), CAST(N'2024-08-12T19:58:11.607' AS DateTime), N'https://i.pinimg.com/736x/6d/16/a3/6d16a301e656ab223942728e9e293e8b.jpg', N'Active', N'https://www.youtube.com/embed/x7X9w_GIm1s?si=NKjg0o-UxzhKPquF', 2, CAST(19.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (5, N'NodeJS', N'Description for Course 4', 36, 35, CAST(N'2024-08-12T19:58:11.607' AS DateTime), CAST(N'2024-08-12T19:58:11.607' AS DateTime), N'https://i0.wp.com/blog.knoldus.com/wp-content/uploads/2021/09/nodejs.png?fit=512%2C256&ssl=1', N'Active', N'https://www.youtube.com/embed/BwM1V4_dl14?si=RljYLn5nzhc_C0_5', 3, CAST(26.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (6, N'React', N'Description for Course 5', 36, 8, CAST(N'2024-08-12T19:58:11.607' AS DateTime), CAST(N'2024-08-12T19:58:11.607' AS DateTime), N'https://i.pinimg.com/564x/d9/c7/88/d9c78817e77664018c43e0a10c65a7fb.jpg', N'Active', N'https://www.youtube.com/embed/Tn6-PIqc4UM?si=CGNMUB7JcR35qijm', 6, CAST(15.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (7, N'Java Spring', N'Azure Sphere offers a secured hardware solution for end to end IoT device security. Know how you can safeguard your IoT devices with Azure Sphere', 37, 9, CAST(N'2024-08-12T19:58:11.607' AS DateTime), CAST(N'2024-08-12T19:58:11.607' AS DateTime), N'https://i.pinimg.com/564x/55/6b/67/556b676a1250b55200fb4f6733d86c37.jpg', N'Active', N'https://www.youtube.com/embed/mNsV7-nmCI0?si=YaB2ytvbgSRT5qGx', 5, CAST(17.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (8, N'Azure IoT - The Complete Guide', N'Become an Azure IoT Expert by learning and practicing all the Azure IoT capabilities from a certified Azure architect', 37, 34, CAST(N'2024-08-12T19:58:11.607' AS DateTime), CAST(N'2024-08-12T19:58:11.607' AS DateTime), N'https://i.pinimg.com/564x/37/89/16/3789168c9e142916237fda424299659f.jpg', N'Active', N'https://www.youtube.com/embed/1ENiVwk8idM?si=bA1Rqz9uSr2zzOnC', 4, CAST(18.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (9, N'Javascript for Beginners
', N'Learn javascript online and supercharge your web design with this Javascript for beginners training course.', 37, 35, CAST(N'2024-08-12T19:58:11.607' AS DateTime), CAST(N'2024-08-12T19:58:11.607' AS DateTime), N'https://i.pinimg.com/564x/fd/42/af/fd42af8a3cacf086abbcf7bf874eb24d.jpg', N'Active', N'https://www.youtube.com/embed/DHjqpvDnNGE?si=3NuaIT7cdgK6Iltp', 2, CAST(20.00 AS Decimal(18, 2)), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (10, N'Laravel 11 - Making a Complete Travel Agency Website ', N'Learning the development of a complete dynamic travel agency management website with Laravel 11', 37, 8, CAST(N'2024-08-12T19:58:11.000' AS DateTime), CAST(N'2024-08-20T18:06:53.917' AS DateTime), N'https://i.pinimg.com/736x/49/76/4d/49764d6858f87906e10f0ce452707964.jpg', N'Active', N'https://www.youtube.com/embed/rIfdg_Ot-LI?si=b02dcqLpRgHvh-23', 5, CAST(13.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (11, N'Android Material Design', N'Implementing Material Design Feature at the end of this course will be a piece of cake for students', 37, 9, CAST(N'2024-08-12T19:58:11.607' AS DateTime), CAST(N'2024-08-12T19:58:11.607' AS DateTime), N'https://i.pinimg.com/236x/2a/79/20/2a79208aa190f13978ec21f46c933a1e.jpg', N'Active', N'https://www.youtube.com/embed/0uxfXwXAx10?si=OxuBo-AQeImqpRt5', 1, CAST(22.00 AS Decimal(18, 2)), CAST(260.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (14, N'Game Developer', N'Một số người vẫn nghĩ chỉ cần thích chơi game là đã có thể làm lập trình game. Liệu điều này có đúng, cùng tìm hiểu nội dung bên dưới để có câu trả lời nhé!', 39, 8, CAST(N'2024-08-20T15:10:36.947' AS DateTime), CAST(N'2024-08-20T15:10:36.947' AS DateTime), N'https://tuhoclaptrinh.edu.vn/upload/post/2023/03/08/lap-trinh-game-can-nhung-ky-nang-gi-20230308101337-512383.jpg', N'Active', N'https://www.youtube.com/embed/Ibjm2KHfymo?si=YciRL7dIPx0AOXPT', 25, CAST(59.00 AS Decimal(18, 2)), CAST(900.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (16, N'Vuejs', N'An approachable, performant and versatile framework for building web user interfaces.', 36, 8, CAST(N'2024-08-20T17:36:31.620' AS DateTime), CAST(N'2024-08-20T17:36:31.620' AS DateTime), N'https://images.viblo.asia/94f4ac67-bebd-4d2e-9a39-2562525e74c3.jpeg', N'Active', N'https://www.youtube.com/embed/Vg9n_YRGPIY?si=ZZj5vigyiZYaDe8w', 5, CAST(59.00 AS Decimal(18, 2)), CAST(99.00 AS Decimal(18, 2)))
INSERT [dbo].[Courses] ([CourseID], [CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price]) VALUES (17, N'No-Code', N'Explore top courses and programs in No-Code Development. Enhance your skills with expert-led lessons from industry leaders.', 57, 8, CAST(N'2024-08-20T17:43:01.900' AS DateTime), CAST(N'2024-08-20T17:43:01.900' AS DateTime), N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSvmhSVx62vYccqNeQm8V4lQ0NoBkOUzLz9Xw&s', N'Active', N'https://www.youtube.com/embed/a5e-F7O5lxs?si=O5MalJyaxrw-xKSn', 2, CAST(31.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (35, 17, 7, CAST(250.00 AS Decimal(18, 2)), 1, CAST(233.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (36, 17, 8, CAST(200.00 AS Decimal(18, 2)), 1, CAST(182.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (37, 18, 6, CAST(200.00 AS Decimal(18, 2)), 1, CAST(185.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (38, 18, 11, CAST(260.00 AS Decimal(18, 2)), 1, CAST(238.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (39, 18, 10, CAST(400.00 AS Decimal(18, 2)), 1, CAST(387.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (40, 19, 5, CAST(300.00 AS Decimal(18, 2)), 1, CAST(274.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (41, 19, 10, CAST(400.00 AS Decimal(18, 2)), 1, CAST(387.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (42, 19, 11, CAST(260.00 AS Decimal(18, 2)), 1, CAST(238.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (43, 20, 5, CAST(300.00 AS Decimal(18, 2)), 1, CAST(274.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (44, 20, 6, CAST(200.00 AS Decimal(18, 2)), 1, CAST(185.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (45, 21, 7, CAST(250.00 AS Decimal(18, 2)), 1, CAST(233.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (46, 21, 8, CAST(200.00 AS Decimal(18, 2)), 1, CAST(182.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (47, 22, 6, CAST(200.00 AS Decimal(18, 2)), 1, CAST(185.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (48, 22, 10, CAST(400.00 AS Decimal(18, 2)), 1, CAST(387.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (49, 23, 9, CAST(150.00 AS Decimal(18, 2)), 1, CAST(130.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (50, 23, 5, CAST(300.00 AS Decimal(18, 2)), 1, CAST(274.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (51, 24, 6, CAST(200.00 AS Decimal(18, 2)), 1, CAST(185.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (52, 24, 8, CAST(200.00 AS Decimal(18, 2)), 1, CAST(182.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (53, 25, 7, CAST(250.00 AS Decimal(18, 2)), 1, CAST(233.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (54, 26, 4, CAST(250.00 AS Decimal(18, 2)), 1, CAST(231.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (55, 27, 4, CAST(250.00 AS Decimal(18, 2)), 1, CAST(231.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (56, 28, 8, CAST(200.00 AS Decimal(18, 2)), 1, CAST(182.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (57, 29, 4, CAST(250.00 AS Decimal(18, 2)), 1, CAST(231.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (58, 29, 9, CAST(150.00 AS Decimal(18, 2)), 1, CAST(130.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (59, 30, 14, CAST(900.00 AS Decimal(18, 2)), 1, CAST(841.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (60, 31, 4, CAST(250.00 AS Decimal(18, 2)), 2, CAST(231.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (62, 33, 5, CAST(300.00 AS Decimal(18, 2)), 1, CAST(274.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (63, 33, 6, CAST(200.00 AS Decimal(18, 2)), 1, CAST(185.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (64, 34, 5, CAST(300.00 AS Decimal(18, 2)), 1, CAST(274.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (65, 35, 7, CAST(250.00 AS Decimal(18, 2)), 1, CAST(233.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (66, 36, 6, CAST(200.00 AS Decimal(18, 2)), 1, CAST(185.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (67, 36, 5, CAST(300.00 AS Decimal(18, 2)), 1, CAST(274.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice]) VALUES (68, 37, 4, CAST(250.00 AS Decimal(18, 2)), 1, CAST(231.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (17, 1, CAST(N'2024-08-19T16:07:54.523' AS DateTime), CAST(415.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (18, 1, CAST(N'2024-08-19T16:26:06.943' AS DateTime), CAST(810.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (19, 1, CAST(N'2024-08-19T16:26:47.167' AS DateTime), CAST(899.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (20, 1, CAST(N'2024-08-19T16:27:01.787' AS DateTime), CAST(459.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (21, 1, CAST(N'2024-08-19T16:35:03.263' AS DateTime), CAST(415.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (22, 1, CAST(N'2024-08-19T16:47:04.930' AS DateTime), CAST(572.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (23, 1, CAST(N'2024-08-19T16:48:06.153' AS DateTime), CAST(404.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (24, 1, CAST(N'2024-08-19T19:30:33.653' AS DateTime), CAST(367.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (25, 1, CAST(N'2024-08-19T20:10:16.950' AS DateTime), CAST(233.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (26, 1, CAST(N'2024-08-20T12:20:47.613' AS DateTime), CAST(231.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (27, 1, CAST(N'2024-08-20T12:37:51.843' AS DateTime), CAST(231.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (28, 4, CAST(N'2024-08-20T12:40:02.210' AS DateTime), CAST(182.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (29, 3, CAST(N'2024-08-20T13:02:50.243' AS DateTime), CAST(361.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (30, 1, CAST(N'2024-08-20T16:47:59.560' AS DateTime), CAST(841.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (31, 1, CAST(N'2024-08-20T17:38:24.263' AS DateTime), CAST(231.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (33, 3, CAST(N'2024-08-20T19:11:51.403' AS DateTime), CAST(459.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (34, 1, CAST(N'2024-08-20T19:51:33.597' AS DateTime), CAST(274.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (35, 1, CAST(N'2024-08-20T19:53:41.853' AS DateTime), CAST(233.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (36, 1, CAST(N'2024-08-20T19:54:50.000' AS DateTime), CAST(459.00 AS Decimal(18, 2)), N'Pending')
INSERT [dbo].[Orders] ([OrderID], [StudentID], [OrderDate], [TotalAmount], [Status]) VALUES (37, 1, CAST(N'2024-08-20T19:56:14.683' AS DateTime), CAST(231.00 AS Decimal(18, 2)), N'Pending')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[Students] ([StudentID]) VALUES (1)
INSERT [dbo].[Students] ([StudentID]) VALUES (2)
INSERT [dbo].[Students] ([StudentID]) VALUES (3)
INSERT [dbo].[Students] ([StudentID]) VALUES (4)
INSERT [dbo].[Students] ([StudentID]) VALUES (5)
INSERT [dbo].[Students] ([StudentID]) VALUES (42)
GO
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (8, N'https://www.coursera.support/sfc/servlet.shepherd/version/renditionDownload?rendition=ORIGINAL_Jpg&versionId=0681U00000N66dW&operationContext=CHATTER&contentId=05T1U000020zLzZ', N'Experience1', N'Specialization1')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (9, N'https://www.coursera.support/sfc/servlet.shepherd/version/renditionDownload?rendition=ORIGINAL_Jpg&versionId=0681U00000N66dW&operationContext=CHATTER&contentId=05T1U000020zLzZ', N'Experience1', N'Specialization1')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (34, N'https://www.coursera.support/sfc/servlet.shepherd/version/renditionDownload?rendition=ORIGINAL_Jpg&versionId=0681U00000N66dW&operationContext=CHATTER&contentId=05T1U000020zLzZ', N'Experience1', N'Dev C++, Duyệt Chứ nói gì nữa')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (35, N'https://68.media.tumblr.com/fa217af735e039c5eca4b3d4a969192a/tumblr_inline_mm6pnbwDPS1qz4rgp.png', N'Sendo Nuôi Ta Lớn Khôn', N'Dev C++, Duyệt Chứ nói gì nữa')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (36, N'https://68.media.tumblr.com/fa217af735e039c5eca4b3d4a969192a/tumblr_inline_mm6pnbwDPS1qz4rgp.png', N'Sendo Nuôi Ta Lớn Khôn', N'Dev C++, Duyệt Chứ nói gì nữa')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (37, N'https://68.media.tumblr.com/fa217af735e039c5eca4b3d4a969192a/tumblr_inline_mm6pnbwDPS1qz4rgp.png', N'Sendo Nuôi Ta Lớn Khôn', N'Dev C++, Duyệt Chứ nói gì nữa')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (38, N'https://68.media.tumblr.com/fa217af735e039c5eca4b3d4a969192a/tumblr_inline_mm6pnbwDPS1qz4rgp.png', N'Sendo Nuôi Ta Lớn Khôn', N'Duyệt đi em cho anh 1 đêm thật nongo choáy')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (39, N'https://www.digitalbabaa.com/public/uploads/all/OhuFBFxlxx3krgkTxlQEey9JrcBhMF96xIrCvFkc.jpg', N'Sendo Nuôi Ta Lớn Khôn', N'Dev C++, Duyệt Chứ nói gì nữa')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (40, N'https://www.coursera.support/sfc/servlet.shepherd/version/renditionDownload?rendition=ORIGINAL_Jpg&versionId=0681U00000N66dW&operationContext=CHATTER&contentId=05T1U000020zLzZ', N'Sendo Nuôi Ta Lớn Khôn', N'Dev React.')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (41, N'https://68.media.tumblr.com/fa217af735e039c5eca4b3d4a969192a/tumblr_inline_mm6pnbwDPS1qz4rgp.png', N'Sendo Nuôi Ta Lớn Khôn', N'Dev React, Duyệt Chứ nói gì nữa')
INSERT [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization]) VALUES (52, N'https://68.media.tumblr.com/fa217af735e039c5eca4b3d4a969192a/tumblr_inline_mm6pnbwDPS1qz4rgp.png', N'4 years', N'.Net')
GO
SET IDENTITY_INSERT [dbo].[UserApprovalLogs] ON 

INSERT [dbo].[UserApprovalLogs] ([LogID], [TutorID], [AdminID], [Decision], [DecisionDate], [Reason]) VALUES (21, 36, 1, N'Reject request', CAST(N'2024-08-10T14:24:22.687' AS DateTime), N'Error information Qualifications. Please checking form')
INSERT [dbo].[UserApprovalLogs] ([LogID], [TutorID], [AdminID], [Decision], [DecisionDate], [Reason]) VALUES (22, 37, 1, N'Xin từ chối', CAST(N'2024-08-10T16:39:01.197' AS DateTime), N'thiếu thông tin')
INSERT [dbo].[UserApprovalLogs] ([LogID], [TutorID], [AdminID], [Decision], [DecisionDate], [Reason]) VALUES (23, 38, 1, N'Reject request', CAST(N'2024-08-10T16:49:18.530' AS DateTime), N'M ngu')
INSERT [dbo].[UserApprovalLogs] ([LogID], [TutorID], [AdminID], [Decision], [DecisionDate], [Reason]) VALUES (24, 38, 1, N'Xin từ chối', CAST(N'2024-08-10T16:50:30.613' AS DateTime), N'nguu')
INSERT [dbo].[UserApprovalLogs] ([LogID], [TutorID], [AdminID], [Decision], [DecisionDate], [Reason]) VALUES (25, 39, 1, N'Hết slot rồi', CAST(N'2024-08-11T13:38:12.047' AS DateTime), N'Ngu')
SET IDENTITY_INSERT [dbo].[UserApprovalLogs] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (1, N'student1', N'student1@example.com', N'123456', N'Student', N'Active', CAST(N'2024-08-07T16:08:00.000' AS DateTime), CAST(N'2024-08-19T08:18:28.000' AS DateTime), N'223-456-7890', N'Thu Duc City Address', CAST(N'2000-07-02' AS Date), N'Lê Văn Hà')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (2, N'student2', N'student2@example.com', N'123456', N'Student', N'Active', CAST(N'2024-08-07T16:08:00.000' AS DateTime), CAST(N'2024-08-13T23:46:01.180' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Nguyễn Cường Thịnh')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (3, N'student3', N'student3@example.com', N'123456', N'Student', N'Active', CAST(N'2024-08-07T16:08:00.000' AS DateTime), CAST(N'2024-08-10T14:08:10.000' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Lê Văn Hà')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (4, N'student4', N'student4@example.com', N'123456', N'Student', N'Active', CAST(N'2024-08-07T16:08:00.000' AS DateTime), CAST(N'2024-08-13T21:26:49.000' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Nguyễn Cường Thịnh')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (5, N'student5', N'student5@example.com', N'123456', N'Student', N'Active', CAST(N'2024-08-07T16:08:00.583' AS DateTime), CAST(N'2024-08-07T16:08:00.583' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Lê Văn Hà')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (6, N'tutor1', N'tutor1@example.com', N'123456', N'Student', N'Active', CAST(N'2024-08-07T16:08:00.597' AS DateTime), CAST(N'2024-08-07T16:08:00.597' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Nguyễn Cường Thịnh')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (7, N'tutor2', N'tutor2@example.com', N'123456', N'Student', N'Active', CAST(N'2024-08-07T16:08:00.000' AS DateTime), CAST(N'2024-08-19T14:50:52.880' AS DateTime), N'03264951785', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Lê Văn Hà')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (8, N'tutor3', N'tutor3@example.com', N'123456', N'Tutor', N'Active', CAST(N'2024-08-07T16:08:00.597' AS DateTime), CAST(N'2024-08-13T19:08:39.000' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Nguyễn Cường Thịnh')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (9, N'tutor4', N'tutor4@example.com', N'123456', N'Tutor', N'Active', CAST(N'2024-08-07T16:08:00.597' AS DateTime), CAST(N'2024-08-13T20:56:45.000' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Lê Văn Hà')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (11, N'admin1', N'admin1@example.com', N'123456', N'Admin', N'Active', CAST(N'2024-08-07T16:08:00.600' AS DateTime), CAST(N'2024-08-07T16:08:00.600' AS DateTime), N'223-456-7890', N'Thu Duc City', CAST(N'1981-07-02' AS Date), N'Lê Văn Hà')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (27, N'Hà Dev', N'tungphanv2868@gmail.com', N'123456', N'Student', N'Active', CAST(N'2024-08-08T20:02:27.947' AS DateTime), CAST(N'2024-08-08T20:02:27.947' AS DateTime), N'0123456789', N'11 Lê Hồng Phong, Quận 11, TPHCM', CAST(N'2000-10-06' AS Date), N'Hà Văn Lê')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (34, N'Hà Dev', N'tungpvse140688@fpt.edu.vn', N'123456', N'Tutor', N'Active', CAST(N'2024-08-10T11:59:50.823' AS DateTime), CAST(N'2024-08-13T20:56:32.000' AS DateTime), N'0123456789', N'Bình Tân District', CAST(N'2000-10-06' AS Date), N'Ha Tong ')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (35, N'Tùng Bê Đê', N'tungpvse140688@fpt.edu.vn', N'123456', N'Tutor', N'Active', CAST(N'2024-08-10T12:56:33.147' AS DateTime), CAST(N'2024-08-10T12:56:33.147' AS DateTime), N'0123456789', N'Bình Tân District', CAST(N'2000-11-12' AS Date), N'Tùng Phan')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (36, N'Hà Developer', N'vanha10062000@gmail.com', N'123456', N'Student', N'Disactive', CAST(N'2024-08-10T14:21:30.000' AS DateTime), CAST(N'2024-08-10T14:21:30.000' AS DateTime), N'0123456789', N'Kon Tum City', CAST(N'2000-11-12' AS Date), N'Hà Văn')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (37, N'Thịnh gấu', N'cuongthinh5213@gmail.com', N'123456', N'Student', N'Disactive', CAST(N'2024-08-10T16:36:44.000' AS DateTime), CAST(N'2024-08-12T10:42:31.000' AS DateTime), N'0123456789', N'11 Lê Quang Diêu, Củ Chi, TP HCM', CAST(N'2000-10-06' AS Date), N'Thịnh Nguyễn')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (38, N'Hà văn', N'gevori8461@almaxen.com', N'123456', N'Student', N'Active', CAST(N'2024-08-10T16:48:57.737' AS DateTime), CAST(N'2024-08-10T16:48:57.737' AS DateTime), N'0123456789', N'11 Lê Quang Diêu, Củ Chi, TP HCM', CAST(N'2000-10-06' AS Date), N'Văn Hà FPT')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (39, N'Ha KonTum', N'tk22042301@gmail.com', N'123456', N'Student', N'Active', CAST(N'2024-08-11T13:37:52.290' AS DateTime), CAST(N'2024-08-11T13:37:52.290' AS DateTime), N'0123456789', N'11 Lê Quang Diêu, Củ Chi, TP HCM', CAST(N'2000-11-11' AS Date), N'Hà Văn Lê')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (40, N'Thịnh gấu', N'gigol56028@albarulo.com', N'123456', N'Tutor', N'Active', CAST(N'2024-08-13T21:05:45.867' AS DateTime), CAST(N'2024-08-13T21:05:45.000' AS DateTime), N'0123456789', N'11 Lê Quang Diêu, Củ Chi, TP HCM', CAST(N'2000-11-11' AS Date), N'Cường Thịnh')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (41, N'Thịnh gấu', N'ThinhCute@gmail.com', N'123456', N'Tutor', N'Active', CAST(N'2024-08-13T22:27:17.020' AS DateTime), CAST(N'2024-08-13T22:27:17.000' AS DateTime), N'0123456789', N'11 Lê Quang Diêu, Củ Chi, TP HCM', CAST(N'2000-11-12' AS Date), N'Hà Văn Lê')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (42, N'Student', N'ThinhNguyen@gmail.com', N'123456', N'Student', N'Active', CAST(N'2024-08-13T22:38:08.457' AS DateTime), CAST(N'2024-08-13T22:38:08.457' AS DateTime), N'0123456789', N'11 Lê Quang Diêu, Củ Chi, TP HCM', CAST(N'0001-01-01' AS Date), N'Thịnh Nguyễn')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [PasswordHash], [Role], [Status], [CreatedAt], [UpdatedAt], [Phone], [Address], [Birthday], [FullName]) VALUES (52, N'ThinhNguyen', N'licoci4614@brinkc.com', N'123456', N'Tutor', N'Active', CAST(N'2024-08-20T09:36:58.007' AS DateTime), CAST(N'2024-08-20T09:36:58.007' AS DateTime), N'0123456789', N'11 Thang Loi', CAST(N'2000-11-12' AS Date), N'Thinh')
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
