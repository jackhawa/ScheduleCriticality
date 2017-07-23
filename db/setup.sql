CREATE DATABASE CEP;
GO
USE [CEP]
GO
/****** Object:  Table [dbo].[Activities]    Script Date: 3/26/2017 6:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AggressiveProductivityRate] [real] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ProcessId] [int] NOT NULL,
	[SafeProductivityRate] [real] NOT NULL,
	[Units] [real] NOT NULL,
	[UnitDelta] [real] NOT NULL,
	[Dependencies] [nvarchar](max) NULL,
	[DurationFunction] [int] NULL,
	[StartToFinish] [real] NOT NULL,
	[AggressiveDuration] [real] NOT NULL DEFAULT ((0.0000000000000000e+000)),
	[Duration] [real] NOT NULL DEFAULT ((0.0000000000000000e+000)),
	[inputProdRate] [bit] NOT NULL DEFAULT ((0)),
	[Section] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Links]    Script Date: 3/26/2017 6:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Links](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DownwardActivity] [int] NOT NULL,
	[TimePeriod] [real] NOT NULL,
	[UpwardActivity] [int] NOT NULL,
 CONSTRAINT [PK_Links] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Processes]    Script Date: 3/26/2017 6:28:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Processes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Processes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Activities] ON 

INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2041, 2, N'A1', 4, 1, 15, 0, N'', 1, 0, 7.5, 15, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2042, 3, N'A2', 4, 2, 10, 0, N'2041', 1, 0, 3.33333325, 5, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2043, 3, N'A3', 4, 1.5, 15, 0, N'2042', 1, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2044, 2.5, N'A4', 4, 1, 15, 0, N'2043', 1, 0, 6, 15, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2045, 2.5, N'B1', 5, 1, 10, -15, N'2041', 1, 1, 4, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2046, 3, N'B2', 5, 2, 10, 0, N'2042,2045', 0, 0, 3.33333325, 5, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2047, 2, N'B3', 5, 1, 15, 0, N'2046,2043', 0, 0, 7.5, 15, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2048, 2.5, N'B4', 5, 1.5, 10, 0, N'2047,2044', 0, 0, 4, 6.66666651, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2049, 3, N'C1', 7, 2, 15, -10, N'2045', 1, 2.97, 5, 7.5, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2050, 3.5, N'C2', 7, 2, 10, 0, N'2046,2049', 0, 0, 2.857143, 5, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2051, 2, N'C3', 7, 1, 15, -10, N'2050,2047', 0, 0, 7.5, 15, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2052, 2.5, N'C4', 7, 1, 10, 0, N'2051,2048', 0, 0, 4, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2053, 2, N'AA1', 4, 1, 15, 0, N'', 1, 0, 7.5, 15, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2054, 3, N'AA2', 4, 2, 10, 0, N'2053', 1, 0, 3.33333325, 5, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2055, 3, N'AA3', 4, 1.5, 15, 0, N'2054', 1, 0, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2056, 2.5, N'AA4', 4, 1, 15, 0, N'2055', 1, 0, 6, 15, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2057, 2.5, N'BB1', 5, 1, 10, -15, N'2053', 1, 1, 4, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2058, 3, N'BB2', 5, 2, 10, 0, N'2054,2057', 0, 0, 3.33333325, 5, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2059, 2, N'BB3', 5, 1, 15, 0, N'2058,2055', 0, 0, 7.5, 15, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2060, 2.5, N'BB4', 5, 1.5, 10, 0, N'2059,2056', 0, 0, 4, 6.66666651, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2061, 3, N'CC1', 7, 2, 15, -10, N'2057', 1, 2.97, 5, 7.5, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2062, 3.5, N'CC2', 7, 2, 10, 0, N'2058,2061', 0, 0, 2.857143, 5, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2063, 2, N'CC3', 7, 1, 15, -10, N'2062,2059', 0, 0, 7.5, 15, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2064, 2.5, N'CC4', 7, 1, 10, 0, N'2063,2060', 0, 0, 4, 10, 1, 1)
SET IDENTITY_INSERT [dbo].[Activities] OFF
SET IDENTITY_INSERT [dbo].[Links] ON 

INSERT [dbo].[Links] ([Id], [DownwardActivity], [TimePeriod], [UpwardActivity]) VALUES (5, 142, 5, 50)
SET IDENTITY_INSERT [dbo].[Links] OFF
SET IDENTITY_INSERT [dbo].[Processes] ON 

INSERT [dbo].[Processes] ([Id], [Name]) VALUES (4, N'Walls')
INSERT [dbo].[Processes] ([Id], [Name]) VALUES (5, N'Foundation')
INSERT [dbo].[Processes] ([Id], [Name]) VALUES (7, N'Slabs')
INSERT [dbo].[Processes] ([Id], [Name]) VALUES (8, N'Concrete')
INSERT [dbo].[Processes] ([Id], [Name]) VALUES (9, N'Roof')
SET IDENTITY_INSERT [dbo].[Processes] OFF