USE [TANKFARM]
GO
/****** Object:  Table [dbo].[CHOFERES]    Script Date: 2020-09-03 02:00:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CHOFERES](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NULL,
	[Telefono] [varchar](255) NULL,
	[IdCliente] [int] NULL,
	[IdIdioma] [int] NULL,
	[Gerente] [varchar](255) NULL,
	[Correo] [varchar](255) NULL,
	[Tag] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CONFIGURACION]    Script Date: 2020-09-03 02:00:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CONFIGURACION](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IPSERVIDOR] [varchar](255) NOT NULL,
	[IPCONTROLGAS1] [varchar](255) NOT NULL,
	[IPCONTROLGAS2] [varchar](255) NOT NULL,
	[IPPLC] [varchar](255) NOT NULL,
	[REPORTESPOR] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IDIOMA]    Script Date: 2020-09-03 02:00:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IDIOMA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Idioma] [varchar](255) NOT NULL,
	[Status] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[INBOX_CORREO]    Script Date: 2020-09-03 02:00:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[INBOX_CORREO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FECHA] [varchar](255) NULL,
	[MENSAJE] [varchar](255) NULL,
	[CORREO] [varchar](500) NULL,
	[IDUSER] [int] NULL,
	[STATUSMSJ] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ROL]    Script Date: 2020-09-03 02:00:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ROL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [varchar](255) NOT NULL,
	[Status] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[USUARIOS]    Script Date: 2020-09-03 02:00:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[USUARIOS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[UserName] [varchar](255) NULL,
	[Password] [varchar](255) NOT NULL,
	[IdRol] [int] NULL,
	[IdIdioma] [int] NULL,
	[Telefono] [varchar](255) NULL,
	[Correo] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
 CONSTRAINT [PK__USUARIOS__3214EC2787096F4C] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CHOFERES] ON 

INSERT [dbo].[CHOFERES] ([ID], [Nombre], [Telefono], [IdCliente], [IdIdioma], [Gerente], [Correo], [Tag], [Status]) VALUES (2, N'Gerardo Carrillo Haro', N'3322265558', 1473, 2, N'CarlosPonce', N'geraisc@hotmail.com', N'11', N'0')
INSERT [dbo].[CHOFERES] ([ID], [Nombre], [Telefono], [IdCliente], [IdIdioma], [Gerente], [Correo], [Tag], [Status]) VALUES (3, N'CArlos aretega', N'3322265558', 1500, 2, N'pepep', N'geraisc@hotmail.com', N'12', N'0')
INSERT [dbo].[CHOFERES] ([ID], [Nombre], [Telefono], [IdCliente], [IdIdioma], [Gerente], [Correo], [Tag], [Status]) VALUES (6, N'hsahassahsh', N'3322265558', 1500, 2, N'gvhjgjm', N'ghfjgj', N'13', N'0')
SET IDENTITY_INSERT [dbo].[CHOFERES] OFF
SET IDENTITY_INSERT [dbo].[IDIOMA] ON 

INSERT [dbo].[IDIOMA] ([ID], [Idioma], [Status]) VALUES (2, N'ESP', N'1')
INSERT [dbo].[IDIOMA] ([ID], [Idioma], [Status]) VALUES (3, N'ENG', N'1')
SET IDENTITY_INSERT [dbo].[IDIOMA] OFF
SET IDENTITY_INSERT [dbo].[ROL] ON 

INSERT [dbo].[ROL] ([ID], [Rol], [Status]) VALUES (1, N'ADMIN', N'2')
INSERT [dbo].[ROL] ([ID], [Rol], [Status]) VALUES (2, N'JEFE DE PRODUUCCION', N'2')
SET IDENTITY_INSERT [dbo].[ROL] OFF
SET IDENTITY_INSERT [dbo].[USUARIOS] ON 

INSERT [dbo].[USUARIOS] ([ID], [Nombre], [UserName], [Password], [IdRol], [IdIdioma], [Telefono], [Correo], [Status]) VALUES (1, N'MARIANA CARDENAS', N'gesel', N'123456', 1, 1, N'332', N'geraisd.com', N'1')
SET IDENTITY_INSERT [dbo].[USUARIOS] OFF
