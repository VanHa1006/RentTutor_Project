USE [master]
GO
/****** Object:  Database [RenTurtorToStudent]    Script Date: 8/19/2024 8:45:33 PM ******/
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
/****** Object:  User [Sendo_Havan*127.0.0.0*1]    Script Date: 8/19/2024 8:45:34 PM ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 8/19/2024 8:45:34 PM ******/
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
/****** Object:  Table [dbo].[Courses]    Script Date: 8/19/2024 8:45:35 PM ******/
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
/****** Object:  Table [dbo].[Feedback]    Script Date: 8/19/2024 8:45:35 PM ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 8/19/2024 8:45:35 PM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 8/19/2024 8:45:35 PM ******/
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
/****** Object:  Table [dbo].[Students]    Script Date: 8/19/2024 8:45:35 PM ******/
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
/****** Object:  Table [dbo].[Tutors]    Script Date: 8/19/2024 8:45:35 PM ******/
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
/****** Object:  Table [dbo].[UserApprovalLogs]    Script Date: 8/19/2024 8:45:35 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 8/19/2024 8:45:35 PM ******/
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


-- Insert data into Categories
INSERT INTO [dbo].[Categories] ([CategoryName], [Description])
VALUES 
('Programming', 'Courses related to software development and programming languages.'),
('Data Science', 'Courses focused on data analysis, machine learning, and AI.'),
('Design', 'Courses on graphic design, UX/UI, and creative tools.');

-- Insert data into Users
INSERT INTO [dbo].[Users] ([Username], [Email], [PasswordHash], [Role], [Status], [Phone], [Address], [Birthday], [FullName])
VALUES 
('thinhnc', 'thinh@example.com', '123456', 'Student', 'Active', '123-456-7890', '123 Main St, Anytown, USA', '1990-01-01', 'John Doe'),
('havan', 'havan@example.com', '123456', 'Tutor', 'Active', '234-567-8901', '456 Elm St, Anytown, USA', '1985-02-14', 'Jane Smith'),
('admin_user', 'admin@example.com', '123456', 'Admin', 'Active', '345-678-9012', '789 Oak St, Anytown, USA', '1975-05-23', 'Admin User');

-- Insert data into Tutors
INSERT INTO [dbo].[Tutors] ([TutorID], [Qualifications], [Experience], [Specialization])
VALUES 
((SELECT [UserID] FROM [dbo].[Users] WHERE [Username] = 'jane_smith'), 'PhD in Computer Science', '10 years teaching programming', 'Software Development'),
((SELECT [UserID] FROM [dbo].[Users] WHERE [Username] = 'admin_user'), 'Master in Data Science', '5 years in data analysis', 'Data Science');

-- Insert data into Students
INSERT INTO [dbo].[Students] ([StudentID])
VALUES 
((SELECT [UserID] FROM [dbo].[Users] WHERE [Username] = 'thinhnc'));

-- Insert data into Courses
INSERT INTO [dbo].[Courses] ([CourseName], [Description], [CategoryID], [TutorID], [CreatedAt], [UpdatedAt], [Image], [Status], [LinkVideo], [Hours], [DiscountPrice], [Price])
VALUES 
('Introduction to Python', 'Learn Python from scratch.', 
    (SELECT [CategoryID] FROM [dbo].[Categories] WHERE [CategoryName] = 'Programming'), 
    (SELECT [TutorID] FROM [dbo].[Tutors] WHERE [Qualifications] = 'PhD in Computer Science'),
    GETDATE(), GETDATE(), 'python_intro.jpg', 'Available', 'http://example.com/python_intro', 10, 0.00, 100.00),
('Data Analysis with R', 'Master data analysis with R programming.',
    (SELECT [CategoryID] FROM [dbo].[Categories] WHERE [CategoryName] = 'Data Science'),
    (SELECT [TutorID] FROM [dbo].[Tutors] WHERE [Specialization] = 'Data Science'),
    GETDATE(), GETDATE(), 'r_analysis.jpg', 'Available', 'http://example.com/r_analysis', 15, 50.00, 200.00);

-- Insert data into Orders
INSERT INTO [dbo].[Orders] ([StudentID], [OrderDate], [TotalAmount], [Status])
VALUES 
((SELECT [StudentID] FROM [dbo].[Students] WHERE [StudentID] = 
    (SELECT [UserID] FROM [dbo].[Users] WHERE [Username] = 'thinhnc')), 
    GETDATE(), 150.00, 'Completed');

-- Insert data into OrderDetails
INSERT INTO [dbo].[OrderDetails] ([OrderID], [CourseID], [UnitPrice], [Quantity], [TotalPrice])
VALUES 
((SELECT [OrderID] FROM [dbo].[Orders] WHERE [StudentID] = 
    (SELECT [StudentID] FROM [dbo].[Students] WHERE [StudentID] = 
    (SELECT [UserID] FROM [dbo].[Users] WHERE [Username] = 'thinhnc'))),
    (SELECT [CourseID] FROM [dbo].[Courses] WHERE [CourseName] = 'Introduction to Python'), 100.00, 1, 100.00),
((SELECT [OrderID] FROM [dbo].[Orders] WHERE [StudentID] = 
    (SELECT [StudentID] FROM [dbo].[Students] WHERE [StudentID] = 
    (SELECT [UserID] FROM [dbo].[Users] WHERE [Username] = 'thinhnc'))),
    (SELECT [CourseID] FROM [dbo].[Courses] WHERE [CourseName] = 'Data Analysis with R'), 50.00, 1, 50.00);

-- Insert data into Feedback
INSERT INTO [dbo].[Feedback] ([StudentID], [CourseID], [Rating], [Comment], [CreatedAt])
VALUES 
((SELECT [StudentID] FROM [dbo].[Students] WHERE [StudentID] = 
    (SELECT [UserID] FROM [dbo].[Users] WHERE [Username] = 'thinhnc')),
    (SELECT [CourseID] FROM [dbo].[Courses] WHERE [CourseName] = 'Introduction to Python'), 5, 'Great course!', GETDATE()),
((SELECT [StudentID] FROM [dbo].[Students] WHERE [StudentID] = 
    (SELECT [UserID] FROM [dbo].[Users] WHERE [Username] = 'thinhnc')),
    (SELECT [CourseID] FROM [dbo].[Courses] WHERE [CourseName] = 'Data Analysis with R'), 4, 'Very informative.', GETDATE());

-- Insert data into UserApprovalLogs
INSERT INTO [dbo].[UserApprovalLogs] ([TutorID], [AdminID], [Decision], [DecisionDate], [Reason])
VALUES 
((SELECT [TutorID] FROM [dbo].[Tutors] WHERE [Qualifications] = 'PhD in Computer Science'), 
    (SELECT [UserID] FROM [dbo].[Users] WHERE [Role] = 'Admin'), 
    'Approved', GETDATE(), 'High qualifications'),
((SELECT [TutorID] FROM [dbo].[Tutors] WHERE [Specialization] = 'Data Science'), 
    (SELECT [UserID] FROM [dbo].[Users] WHERE [Role] = 'Admin'), 
    'Approved', GETDATE(), 'Relevant experience');
