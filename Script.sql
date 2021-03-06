USE [master]
GO
/****** Object:  Database [sgrsv]    Script Date: 21/04/2021 01:22:58 a. m. ******/
CREATE DATABASE [sgrsv]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sgrsv', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sgrsv.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'sgrsv_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sgrsv_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [sgrsv] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sgrsv].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sgrsv] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sgrsv] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sgrsv] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sgrsv] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sgrsv] SET ARITHABORT OFF 
GO
ALTER DATABASE [sgrsv] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [sgrsv] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sgrsv] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sgrsv] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sgrsv] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sgrsv] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sgrsv] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sgrsv] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sgrsv] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sgrsv] SET  DISABLE_BROKER 
GO
ALTER DATABASE [sgrsv] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sgrsv] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sgrsv] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sgrsv] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sgrsv] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sgrsv] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sgrsv] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sgrsv] SET RECOVERY FULL 
GO
ALTER DATABASE [sgrsv] SET  MULTI_USER 
GO
ALTER DATABASE [sgrsv] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sgrsv] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sgrsv] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sgrsv] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [sgrsv] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [sgrsv] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'sgrsv', N'ON'
GO
ALTER DATABASE [sgrsv] SET QUERY_STORE = OFF
GO
USE [sgrsv]
GO
/****** Object:  Table [dbo].[Cargo]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cargo](
	[idCargo] [int] IDENTITY(1,1) NOT NULL,
	[tipoCargo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idCargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[conductor]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[conductor](
	[numLicenciaConducir] [varchar](10) NOT NULL,
	[telCelular] [varchar](10) NULL,
	[nombreCompleto] [varchar](75) NOT NULL,
	[fechaNacimiento] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[numLicenciaConducir] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Delegacion]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delegacion](
	[idDelegacion] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[correo] [varchar](40) NULL,
	[codigoPostal] [varchar](5) NULL,
	[calle] [varchar](30) NULL,
	[colonia] [varchar](30) NULL,
	[numero] [varchar](4) NULL,
	[tipo] [varchar](30) NULL,
	[idMunicipio] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idDelegacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dictamen]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dictamen](
	[folio] [int] NOT NULL,
	[descripcion] [varchar](250) NULL,
	[fechaHora] [date] NULL,
	[idReporte] [int] NULL,
	[username] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[folio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fotografia]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fotografia](
	[idFotografia] [int] IDENTITY(1,1) NOT NULL,
	[ruta] [varchar](100) NOT NULL,
	[idReporte] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idFotografia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[municipio]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[municipio](
	[idMunicipio] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReporteSiniestro]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReporteSiniestro](
	[idReporte] [int] IDENTITY(1,1) NOT NULL,
	[calle] [varchar](30) NOT NULL,
	[numero] [varchar](5) NOT NULL,
	[colonia] [varchar](30) NOT NULL,
	[idDelegacion] [int] NOT NULL,
	[username] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idReporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[username] [varchar](15) NOT NULL,
	[nombreCompleto] [varchar](20) NOT NULL,
	[password] [varchar](20) NULL,
	[idDelegacion] [int] NULL,
	[idCargo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehiculo]    Script Date: 21/04/2021 01:23:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehiculo](
	[numPlaca] [varchar](8) NOT NULL,
	[marca] [varchar](20) NOT NULL,
	[modelo] [varchar](20) NOT NULL,
	[color] [varchar](15) NOT NULL,
	[numPolizaSeguro] [varchar](20) NULL,
	[nombreAseguradora] [varchar](30) NULL,
	[ano] [varchar](4) NOT NULL,
	[numLicenciaConducir] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[numPlaca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [sgrsv] SET  READ_WRITE 
GO
