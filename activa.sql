USE [master]
GO
/****** Object:  Database [AdministrarAlbunes_Activa-IT]    Script Date: 11/07/2020 16:24:41 ******/
CREATE DATABASE [AdministrarAlbunes_Activa-IT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AdministrarAlbunes_Activa-IT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2019\MSSQL\DATA\AdministrarAlbunes_Activa-IT.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AdministrarAlbunes_Activa-IT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2019\MSSQL\DATA\AdministrarAlbunes_Activa-IT_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AdministrarAlbunes_Activa-IT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET ARITHABORT OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET  MULTI_USER 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET DELAYED_DURABILITY = DISABLED 
GO
USE [AdministrarAlbunes_Activa-IT]
GO
/****** Object:  Table [dbo].[Album]    Script Date: 11/07/2020 16:24:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[IdAlbum] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaConsulta] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED 
(
	[IdAlbum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cancion]    Script Date: 11/07/2020 16:24:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cancion](
	[IdCancion] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[IdAlbum] [int] NOT NULL,
	[Faborito] [bit] NOT NULL CONSTRAINT [DF_Cancion_Faborito]  DEFAULT ((0)),
	[Inapropiado] [bit] NOT NULL CONSTRAINT [DF_Cancion_Inapropiado]  DEFAULT ((0)),
	[NoVolverListar] [bit] NOT NULL CONSTRAINT [DF_Cancion_NoVolverListar]  DEFAULT ((0)),
 CONSTRAINT [PK_Cancion] PRIMARY KEY CLUSTERED 
(
	[IdCancion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/07/2020 16:24:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Apellido] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Album] ON 

INSERT [dbo].[Album] ([IdAlbum], [Id], [IdUsuario], [FechaConsulta]) VALUES (4, 48, 5, CAST(N'2020-07-11 15:57:11.9057892' AS DateTime2))
INSERT [dbo].[Album] ([IdAlbum], [Id], [IdUsuario], [FechaConsulta]) VALUES (5, 47, 5, CAST(N'2020-07-11 14:40:54.9171392' AS DateTime2))
INSERT [dbo].[Album] ([IdAlbum], [Id], [IdUsuario], [FechaConsulta]) VALUES (6, 1, 5, CAST(N'2020-07-11 16:16:14.3666551' AS DateTime2))
INSERT [dbo].[Album] ([IdAlbum], [Id], [IdUsuario], [FechaConsulta]) VALUES (7, 4, 5, CAST(N'2020-07-11 16:10:21.4776748' AS DateTime2))
INSERT [dbo].[Album] ([IdAlbum], [Id], [IdUsuario], [FechaConsulta]) VALUES (8, 6, 5, CAST(N'2020-07-11 16:16:10.6985255' AS DateTime2))
INSERT [dbo].[Album] ([IdAlbum], [Id], [IdUsuario], [FechaConsulta]) VALUES (9, 5, 4, CAST(N'2020-07-11 16:16:03.8926167' AS DateTime2))
INSERT [dbo].[Album] ([IdAlbum], [Id], [IdUsuario], [FechaConsulta]) VALUES (10, 7, 3, CAST(N'2020-07-11 16:15:18.1195632' AS DateTime2))
INSERT [dbo].[Album] ([IdAlbum], [Id], [IdUsuario], [FechaConsulta]) VALUES (11, 77, 3, CAST(N'2020-07-11 16:15:53.3180553' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Album] OFF
SET IDENTITY_INSERT [dbo].[Cancion] ON 

INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (1, 2351, 4, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (2, 2352, 4, 0, 1, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (3, 2378, 4, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (4, 2399, 4, 0, 0, 1)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (5, 2354, 4, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (6, 2353, 4, 0, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (7, 1, 6, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (8, 151, 7, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (9, 251, 8, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (10, 201, 9, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (11, 301, 10, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (12, 304, 10, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (13, 305, 10, 0, 1, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (14, 306, 10, 0, 0, 1)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (15, 303, 10, 0, 1, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (16, 307, 10, 1, 0, 0)
INSERT [dbo].[Cancion] ([IdCancion], [Id], [IdAlbum], [Faborito], [Inapropiado], [NoVolverListar]) VALUES (17, 3802, 11, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Cancion] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [Email], [Password]) VALUES (1, N'Miguel', N'Rodriguez', N'miiguell005@gmail.com', N'123456')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [Email], [Password]) VALUES (2, N'Pedro', N'Rodriguez', N'miiguell005@gmail.com', N'123456')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [Email], [Password]) VALUES (3, N'Juan', N'Rodriguez', N'miiguell005@gmail.com', N'123456')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [Email], [Password]) VALUES (4, N'Maria', N'Rodriguez', N'miiguell005@gmail.com', N'123456')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [Email], [Password]) VALUES (5, N'Rocio', N'Rodriguez', N'miiguell005@gmail.com', N'123456')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [Email], [Password]) VALUES (6, N'Stiven', N'Rodriguez', N'miiguell005@gmail.com', N'123456')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [Email], [Password]) VALUES (7, N'Ana', N'Rodriguez', N'miiguell005@gmail.com', N'123456')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [FK_Album_Usuario]
GO
ALTER TABLE [dbo].[Cancion]  WITH CHECK ADD  CONSTRAINT [FK_Cancion_Album] FOREIGN KEY([IdAlbum])
REFERENCES [dbo].[Album] ([IdAlbum])
GO
ALTER TABLE [dbo].[Cancion] CHECK CONSTRAINT [FK_Cancion_Album]
GO
USE [master]
GO
ALTER DATABASE [AdministrarAlbunes_Activa-IT] SET  READ_WRITE 
GO
