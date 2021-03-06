USE [sgsrv]
GO
/****** Object:  Table [dbo].[cargo]    Script Date: 10/05/2021 10:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cargo](
	[idCargo] [int] IDENTITY(1,1) NOT NULL,
	[tipoCargo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idCargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[conductor]    Script Date: 10/05/2021 10:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[conductor](
	[numeroLicenciaConducir] [varchar](10) NOT NULL,
	[telefonoCelular] [varchar](10) NULL,
	[nombreCompleto] [varchar](75) NOT NULL,
	[fechaNacimiento] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[numeroLicenciaConducir] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delegacion]    Script Date: 10/05/2021 10:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delegacion](
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
/****** Object:  Table [dbo].[dictamen]    Script Date: 10/05/2021 10:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dictamen](
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
/****** Object:  Table [dbo].[fotografia]    Script Date: 10/05/2021 10:08:12 p. m. ******/
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
/****** Object:  Table [dbo].[municipio]    Script Date: 10/05/2021 10:08:12 p. m. ******/
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
/****** Object:  Table [dbo].[reporteSiniestro]    Script Date: 10/05/2021 10:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reporteSiniestro](
	[idReporte] [int] IDENTITY(1,1) NOT NULL,
	[calle] [varchar](30) NOT NULL,
	[numero] [varchar](5) NOT NULL,
	[colonia] [varchar](30) NOT NULL,
	[fechaHora] [datetime] NOT NULL,
	[idDelegacion] [int] NOT NULL,
	[username] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idReporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipoDelegacion]    Script Date: 10/05/2021 10:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoDelegacion](
	[idTipoDelegacion] [int] IDENTITY(1,1) NOT NULL,
	[tipoDelegacion] [varchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoDelegacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 10/05/2021 10:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
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
/****** Object:  Table [dbo].[vehiculo]    Script Date: 10/05/2021 10:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehiculo](
	[numeroPlaca] [varchar](8) NOT NULL,
	[marca] [varchar](20) NOT NULL,
	[modelo] [varchar](20) NOT NULL,
	[color] [varchar](15) NOT NULL,
	[numeroPolizaSeguro] [varchar](20) NULL,
	[nombreAseguradora] [varchar](30) NULL,
	[ano] [varchar](4) NOT NULL,
	[numeroLicenciaConducir] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[numeroPlaca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (1, N'ACAJETE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (2, N'ACATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (3, N'ACAYUCAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (4, N'ACTOPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (5, N'ACULA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (6, N'ACULTZINGO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (7, N'CAMARON DE TEJEDA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (8, N'ALPATLAHUAC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (9, N'ALTO LUCERO DE GUTIERREZ BARRIOS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (10, N'ALTOTONGA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (11, N'ALVARADO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (12, N'AMATITLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (13, N'NARANJOS AMATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (14, N'AMATLAN DE LOS REYES')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (15, N'ANGEL R. CABADA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (16, N'ANTIGUA, LA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (17, N'APAZAPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (18, N'AQUILA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (19, N'ASTACINGA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (20, N'ATLAHUILCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (21, N'ATOYAC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (22, N'ATZACAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (23, N'ATZALAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (24, N'TLALTETELA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (25, N'AYAHUALULCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (26, N'BANDERILLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (27, N'BENITO JUAREZ')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (28, N'BOCA DEL RIO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (29, N'CALCAHUALCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (30, N'CAMERINO Z. MENDOZA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (31, N'CARRILLO PUERTO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (32, N'CATEMACO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (33, N'CAZONES DE HERRERA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (34, N'CERRO AZUL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (35, N'CITLALTEPETL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (36, N'COACOATZINTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (37, N'COAHUITLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (38, N'COATEPEC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (39, N'COATZACOALCOS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (40, N'COATZINTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (41, N'COETZALA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (42, N'COLIPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (43, N'COMAPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (44, N'CORDOBA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (45, N'COSAMALOAPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (46, N'COSAUTLAN DE CARVAJAL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (47, N'COSCOMATEPEC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (48, N'COSOLEACAQUE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (49, N'COTAXTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (50, N'COXQUIHUI')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (51, N'COYUTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (52, N'CUICHAPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (53, N'CUITLAHUAC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (54, N'CHACALTIANGUIS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (55, N'CHALMA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (56, N'CHICONAMEL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (57, N'CHICONQUIACO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (58, N'CHICONTEPEC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (59, N'CHINAMECA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (60, N'CHINAMPA DE GOROSTIZA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (61, N'CHOAPAS, LAS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (62, N'CHOCAMAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (63, N'CHONTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (64, N'CHUMATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (65, N'EMILIANO ZAPATA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (66, N'ESPINAL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (67, N'FILOMENO MATA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (68, N'FORTIN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (69, N'GUTIERREZ ZAMORA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (70, N'HIDALGOTITLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (71, N'HUATUSCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (72, N'HUAYACOCOTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (73, N'HUEYAPAN DE OCAMPO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (74, N'HUILOAPAN DE CUAUHTEMOC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (75, N'IGNACIO DE LA LLAVE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (76, N'ILAMATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (77, N'ISLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (78, N'IXCATEPEC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (79, N'IXHUACAN DE LOS REYES')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (80, N'IXHUATLAN DEL CAFE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (81, N'IXHUATLANCILLO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (82, N'IXHUATLAN DEL SURESTE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (83, N'IXHUATLAN DE MADERO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (84, N'IXMATLAHUACAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (85, N'IXTACZOQUITLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (86, N'JALACINGO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (87, N'XALAPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (88, N'JALCOMULCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (89, N'JALTIPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (90, N'JAMAPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (91, N'JESUS CARRANZA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (92, N'XICO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (93, N'JILOTEPEC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (94, N'JUAN RODRIGUEZ CLARA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (95, N'JUCHIQUE DE FERRER')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (96, N'LANDERO Y COSS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (97, N'LERDO DE TEJADA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (98, N'MAGDALENA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (99, N'MALTRATA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (100, N'MANLIO FABIO ALTAMIRANO')
GO
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (101, N'MARIANO ESCOBEDO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (102, N'MARTINEZ DE LA TORRE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (103, N'MECATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (104, N'MECAYAPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (105, N'MEDELLIN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (106, N'MIAHUATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (107, N'MINAS, LAS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (108, N'MINATITLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (109, N'MISANTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (110, N'MIXTLA DE ALTAMIRANO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (111, N'MOLOACAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (112, N'NAOLINCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (113, N'NARANJAL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (114, N'NAUTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (115, N'NOGALES')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (116, N'OLUTA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (117, N'OMEALCA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (118, N'ORIZABA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (119, N'OTATITLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (120, N'OTEAPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (121, N'OZULUAMA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (122, N'PAJAPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (123, N'PANUCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (124, N'PAPANTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (125, N'PASO DEL MACHO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (126, N'PASO DE OVEJAS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (127, N'PERLA, LA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (128, N'PEROTE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (129, N'PLATON SANCHEZ')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (130, N'PLAYA VICENTE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (131, N'POZA RICA DE HIDALGO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (132, N'VIGAS DE RAMIREZ, LAS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (133, N'PUEBLO VIEJO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (134, N'PUENTE NACIONAL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (135, N'RAFAEL DELGADO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (136, N'RAFAEL LUCIO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (137, N'REYES, LOS')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (138, N'RIO BLANCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (139, N'SALTABARRANCA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (140, N'SAN ANDRES TENEJAPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (141, N'SAN ANDRES TUXTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (142, N'SAN JUAN EVANGELISTA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (143, N'SANTIAGO TUXTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (144, N'SAYULA DE ALEMAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (145, N'SOCONUSCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (146, N'SOCHIAPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (147, N'SOLEDAD ATZOMPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (148, N'SOLEDAD DE DOBLADO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (149, N'SOTEAPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (150, N'TAMALIN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (151, N'TAMIAHUA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (152, N'TAMPICO ALTO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (153, N'TANCOCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (154, N'TANTIMA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (155, N'TANTOYUCA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (156, N'TATATILA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (157, N'CASTILLO DE TEAYO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (158, N'TECOLUTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (159, N'TEHUIPANGO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (160, N'TEMAPACHE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (161, N'TEMPOAL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (162, N'TENAMPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (163, N'TENOCHTITLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (164, N'TEOCELO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (165, N'TEPATLAXCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (166, N'TEPETLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (167, N'TEPETZINTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (168, N'TEQUILA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (169, N'JOSE AZUETA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (170, N'TEXCATEPEC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (171, N'TEXHUACAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (172, N'TEXISTEPEC')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (173, N'TEZONAPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (174, N'TIERRA BLANCA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (175, N'TIHUATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (176, N'TLACOJALPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (177, N'TLACOLULAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (178, N'TLACOTALPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (179, N'TLACOTEPEC DE MEJIA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (180, N'TLACHICHILCO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (181, N'TLALIXCOYAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (182, N'TLALNELHUAYOCAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (183, N'TLAPACOYAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (184, N'TLAQUILPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (185, N'TLILAPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (186, N'TOMATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (187, N'TONAYAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (188, N'TOTUTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (189, N'TUXPAM')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (190, N'TUXTILLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (191, N'URSULO GALVAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (192, N'VEGA DE ALATORRE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (193, N'VERACRUZ')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (194, N'VILLA ALDAMA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (195, N'XOXOCOTLA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (196, N'YANGA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (197, N'YECUATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (198, N'ZACUALPAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (199, N'ZARAGOZA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (200, N'ZENTLA')
GO
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (201, N'ZONGOLICA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (202, N'ZONTECOMATLAN')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (203, N'ZOZOCOLCO DE HIDALGO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (204, N'AGUA DULCE')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (205, N'HIGO, EL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (206, N'NANCHITAL DE LAZARO CARDENAS DEL RIO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (207, N'TRES VALLES')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (208, N'CARLOS A. CARRILLO')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (209, N'TATAHUICAPAN DE JUAREZ')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (210, N'UXPANAPA')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (211, N'SAN RAFAEL')
INSERT [dbo].[municipio] ([idMunicipio], [nombre]) VALUES (212, N'SANTIAGO SOCHIAPA')
GO
