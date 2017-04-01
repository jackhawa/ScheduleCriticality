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

INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (36, 2, N'A1', 4, 1, 10, 0, N'', 1, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (37, 2, N'A2', 4, 1, 10, 0, N'36', 1, 0, 6, 12, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (38, 2, N'A3', 4, 1, 10, 0, N'37', 1, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (39, 3, N'B1', 5, 1.5, 10, -10, N'36', 1, 3.34, 3.33333325, 6.66666651, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (40, 2, N'B2', 5, 1, 10, 0, N'37,39', 0, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (41, 2, N'B3', 5, 1, 10, 0, N'40,38', 0, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (42, 2, N'C1', 7, 1, 10, -10, N'39', 1, 3.33, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (43, 3, N'C2', 7, 1.5, 10, 0, N'40,42', 0, 0, 3.33333325, 6.66666651, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (44, 1, N'C3', 7, 0.5, 10, -10, N'41,43', 0, 0, 10, 20, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (45, 2.4, N'D1', 8, 1.2, 10, -10, N'42', 1, 0, 4.16666651, 8.333333, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (46, 0.6, N'D2', 8, 0.3, 10, 0, N'45,43', 0, 0, 16.666666, 33.3333321, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (47, 2, N'D3', 8, 1, 10, 0, N'46,44', 0, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (48, 1.6, N'E1', 9, 0.8, 10, -10, N'45', 1, 20.83, 6.25, 12.5, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (49, 2, N'E2', 9, 1, 10, -10, N'46,48', 0, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (50, 1, N'E3', 9, 0.5, 10, 0, N'49,47', 0, 0, 10, 20, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (57, 2, N'A4', 4, 1, 10, 0, N'38', 1, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (58, 2, N'B4', 5, 1, 10, 0, N'41', 1, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (59, 2, N'C4', 7, 1, 15, 0, N'44', 1, 0, 7.5, 15, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (60, 2, N'D4', 8, 1, 10, 0, N'47', 1, 0, 5, 10, 1, 0)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (136, 2, N'A1', 4, 1, 10, 0, N'', 1, 0, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (137, 2, N'A2', 4, 1, 10, 0, N'136', 1, 0, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (138, 2, N'A3', 4, 1, 7, 0, N'137', 1, 0, 3.5, 7, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (139, 3, N'B1', 5, 1.5, 10, -10, N'136', 1, 3.34, 3.33333325, 6.66666651, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (140, 2, N'B2', 5, 1, 10, 0, N'137,139', 0, 0, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (141, 2, N'B3', 5, 1, 10, 0, N'140,138', 0, 0, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (142, 2, N'C1', 7, 1, 10, -10, N'139', 1, 3.33, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (143, 3, N'C2', 7, 1.5, 10, 0, N'140,142', 0, 0, 3.33333325, 6.66666651, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (144, 1, N'C3', 7, 0.5, 10, -10, N'141,143', 0, 0, 10, 20, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (145, 2.4, N'D1', 8, 1.2, 10, -10, N'142', 1, 0, 4.16666651, 8.333333, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (146, 0.6, N'D2', 8, 0.3, 10, 0, N'145,143', 0, 0, 16.666666, 33.3333321, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (147, 2, N'D3', 8, 1, 10, 0, N'146,144', 0, 0, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (148, 1.6, N'E1', 9, 0.8, 10, -10, N'145', 1, 20.83, 6.25, 12.5, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (149, 2, N'E2', 9, 1, 10, -10, N'146,148', 0, 0, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (150, 1, N'E3', 9, 0.5, 10, 0, N'149,147', 0, 0, 10, 20, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (157, 2, N'A4', 4, 1, 10, 0, N'138', 1, 0, 5, 10, 1, 1)
INSERT [dbo].[Activities] ([Id], [AggressiveProductivityRate], [Name], [ProcessId], [SafeProductivityRate], [Units], [UnitDelta], [Dependencies], [DurationFunction], [StartToFinish], [AggressiveDuration], [Duration], [inputProdRate], [Section]) VALUES (2039, 1, N'D5', 8, 1, 10, 0, N'60', 1, 0, 10, 10, 1, 0)
SET IDENTITY_INSERT [dbo].[Activities] OFF
SET IDENTITY_INSERT [dbo].[Links] ON 

INSERT [dbo].[Links] ([Id], [DownwardActivity], [TimePeriod], [UpwardActivity]) VALUES (5, 142, 5, 50)
SET IDENTITY_INSERT [dbo].[Links] OFF
SET IDENTITY_INSERT [dbo].[Processes] ON 

INSERT [dbo].[Processes] ([Id], [Name]) VALUES (4, N'Walls')
INSERT [dbo].[Processes] ([Id], [Name]) VALUES (5, N'Floor')
INSERT [dbo].[Processes] ([Id], [Name]) VALUES (7, N'Ceiling')
INSERT [dbo].[Processes] ([Id], [Name]) VALUES (8, N'Windows')
INSERT [dbo].[Processes] ([Id], [Name]) VALUES (9, N'Roof')
SET IDENTITY_INSERT [dbo].[Processes] OFF
