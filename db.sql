USE [master]
GO

CREATE DATABASE [QUAN_LY_KTX]
GO
USE [QUAN_LY_KTX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[mahd] [int] IDENTITY(1,1) NOT NULL,
	[ngaytao] [datetime] NOT NULL,
	[maphong] [int] NOT NULL,
	[tiendiennuoc] [float] NULL,
	[tongtien] [float] NULL,
	[tienphong] [float] NULL,
	[tenhs] [nvarchar](50) NULL,
	[tennv] [nvarchar](50) NULL,
	[maphieudiennuoc] [int] NOT NULL,
 CONSTRAINT [PK__HOADON__981D1EB23CD73055] PRIMARY KEY CLUSTERED 
(
	[mahd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOCSINH]    Script Date: 5/15/2022 10:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOCSINH](
	[mahs] [int] IDENTITY(1,1) NOT NULL,
	[maphong] [int] NOT NULL,
	[hoten] [nvarchar](50) NOT NULL,
	[ngaysinh] [datetime] NULL,
	[quequan] [nvarchar](100) NULL,
	[gioitinh] [bit] NOT NULL,
	[ttphuhuynh] [nvarchar](150) NULL,
	[lop] [nvarchar](30) NULL,
 CONSTRAINT [PK__HOCSINH__7A2100A91276970E] PRIMARY KEY CLUSTERED 
(
	[mahs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOCSINH_NEW](
	[mahs] [int] IDENTITY(1,1) NOT NULL,
	[hoten] [nvarchar](50) NULL,
	[ngaysinh] [datetime] NULL,
	[quequan] [nvarchar](100) NULL,
	[gioitinh] [bit] NULL,
	[ttphuhuynh] [nvarchar](150) NULL,
	[lop] [nvarchar](30) NULL,
 CONSTRAINT [PK_HOCSINH_NEW] PRIMARY KEY CLUSTERED 
(
	[mahs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOCSINH_OLD](
	[mahs] [int] IDENTITY(1,1) NOT NULL,
	[hoten] [nvarchar](50) NULL,
	[ngaysinh] [datetime] NULL,
	[quequan] [nvarchar](100) NULL,
	[gioitinh] [bit] NULL,
	[ttphuhuynh] [nvarchar](150) NULL,
	[lop] [nvarchar](30) NULL,
 CONSTRAINT [PK_HOCSINH_OLD] PRIMARY KEY CLUSTERED 
(
	[mahs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEU_DIENNUOC](
	[maphieu] [int] IDENTITY(1,1) NOT NULL,
	[ngaytaophieu] [datetime] NULL,
	[maphong] [int] NOT NULL,
	[sodien] [int] NULL,
	[sonuoc] [int] NULL,
	[giadien] [int] NULL,
	[gianuoc] [int] NULL,
	[tongtien] [float] NULL,
 CONSTRAINT [PK__PHIEU_DI__A72B518548848E65] PRIMARY KEY CLUSTERED 
(
	[maphieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHONG]    Script Date: 5/15/2022 10:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHONG](
	[maphong] [int] IDENTITY(1,1) NOT NULL,
	[tenphong] [nvarchar](50) NOT NULL,
	[tsogiuong] [int] NOT NULL,
	[tang] [int] NULL,
	[tinhtrang] [bit] NULL,
 CONSTRAINT [PK__PHONG__BBA25480B37DBAB9] PRIMARY KEY CLUSTERED 
(
	[maphong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 5/15/2022 10:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[matk] [int] IDENTITY(1,1) NOT NULL,
	[hoten] [nvarchar](50) NULL,
	[email] [nvarchar](100) NOT NULL,
	[pass] [nvarchar](100) NOT NULL,
	[cvu] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[matk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VATTU](
	[mavt] [int] IDENTITY(1,1) NOT NULL,
	[tenvattu] [nvarchar](100) NULL,
	[soluong] [int] NULL,
	[giatien] [float] NULL,
 CONSTRAINT [PK__VATTU__7A208E5D08B15BC7] PRIMARY KEY CLUSTERED 
(
	[mavt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VATTU_PHONG]    Script Date: 5/15/2022 10:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VATTU_PHONG](
	[ID] [int] NOT NULL,
	[maphong] [int] NOT NULL,
	[mavt] [int] NOT NULL,
	[soluong] [int] NULL,
 CONSTRAINT [PK_VATTU_PHONG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HOCSINH] ON 

GO
SET IDENTITY_INSERT [dbo].[HOCSINH] ON 

INSERT [dbo].[HOCSINH] ([mahs], [maphong], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) 
VALUES 
(2, 1, N'Nguyễn Văn A', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 0, N'NO', N'5A'),
(3, 1, N'Nguyễn Văn B', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'NO', N'3B'),
(4, 1, N'Nguyễn Văn C', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 0, N'NO', N'4B'),
(5, 1, N'Nguyễn Văn D', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'Bình Dương', 1, N'NO', N'1E'),
(6, 2, N'Trần Vỹ', CAST(N'2003-01-09T00:00:00.000' AS DateTime), N'Cần Thơ', 1, N'No', N'8A'),
(7, 2, N'Nguyễn Nhi', CAST(N'2003-12-01T00:00:00.000' AS DateTime), N'Đồng Nai', 1, N'No', N'9E'),
(8, 3, N'Hồng Nhung', CAST(N'2003-02-03T00:00:00.000' AS DateTime), N'An Giang', 0, N'NO', N'7A'),
(9, 3, N'Thúy Anh', CAST(N'2003-02-02T00:00:00.000' AS DateTime), N'Cà Mau', 0, N'', N'9A'),
(10, 3, N'Phú Mỹ', CAST(N'2003-03-09T00:00:00.000' AS DateTime), N'Tiền Giang', 1, N'', N'8B'),
(13, 4, N'Nguyễn Hà', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'Vĩnh Long', 0, N'', N'9A'),
(16, 4, N'Thế Anh', CAST(N'2003-02-09T00:00:00.000' AS DateTime), N'Bạc Liêu', 1, N'', N'7E');

SET IDENTITY_INSERT [dbo].[HOCSINH] OFF;

SET IDENTITY_INSERT [dbo].[HOCSINH] OFF
GO
SET IDENTITY_INSERT [dbo].[HOCSINH_NEW] ON 

INSERT [dbo].[HOCSINH_NEW] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (1, N'hs mới 1', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'9a')
INSERT [dbo].[HOCSINH_NEW] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (2, N'hs mới 232', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'9a')
INSERT [dbo].[HOCSINH_NEW] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (3, N'hs mới 3', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'9a')
INSERT [dbo].[HOCSINH_NEW] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (4, N'hs mới 4', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'9a')
INSERT [dbo].[HOCSINH_NEW] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (5, N'hs mới 5', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'9a')
INSERT [dbo].[HOCSINH_NEW] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (6, N'The Anh', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N's', N's')
SET IDENTITY_INSERT [dbo].[HOCSINH_NEW] OFF
GO
SET IDENTITY_INSERT [dbo].[HOCSINH_OLD] ON 

INSERT [dbo].[HOCSINH_OLD] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (1, N'hs cũ 1', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'no')
INSERT [dbo].[HOCSINH_OLD] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (2, N'hs cũ 2', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'no')
INSERT [dbo].[HOCSINH_OLD] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (3, N'hs cũ 3', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'no')
INSERT [dbo].[HOCSINH_OLD] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (4, N'hs cũ 45', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'no')
INSERT [dbo].[HOCSINH_OLD] ([mahs], [hoten], [ngaysinh], [quequan], [gioitinh], [ttphuhuynh], [lop]) VALUES (5, N'hs cũ 33', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'TP.HCM', 1, N'no', N'no')
SET IDENTITY_INSERT [dbo].[HOCSINH_OLD] OFF
GO
SET IDENTITY_INSERT [dbo].[PHIEU_DIENNUOC] ON 

INSERT [dbo].[PHIEU_DIENNUOC] ([maphieu], [ngaytaophieu], [maphong], [sodien], [sonuoc], [giadien], [gianuoc], [tongtien]) VALUES (1, CAST(N'2001-01-01T00:00:00.000' AS DateTime), 1, 54, 456, 456, 654, 46)
INSERT [dbo].[PHIEU_DIENNUOC] ([maphieu], [ngaytaophieu], [maphong], [sodien], [sonuoc], [giadien], [gianuoc], [tongtien]) VALUES (3, CAST(N'2001-02-02T00:00:00.000' AS DateTime), 1, 45, 54, 654, 456, 64)
INSERT [dbo].[PHIEU_DIENNUOC] ([maphieu], [ngaytaophieu], [maphong], [sodien], [sonuoc], [giadien], [gianuoc], [tongtien]) VALUES (4, CAST(N'2022-05-15T20:41:34.000' AS DateTime), 1, 25, 435, 345, 5, 5)
SET IDENTITY_INSERT [dbo].[PHIEU_DIENNUOC] OFF
GO
SET IDENTITY_INSERT [dbo].[PHONG] ON 

INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (1, N'Phòng 1A', 8, 1, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (2, N'Phòng 2A', 8, 1, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (3, N'Phòng 3A', 8, 1, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (4, N'Phòng 4A', 6, 1, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (6, N'Phòng 5A', 6, 1, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (7, N'Phòng 1B', 5, 2, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (8, N'Phòng 2B', 5, 2, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (9, N'Phòng 3B', 7, 2, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (10, N'Phòng 4B', 6, 2, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (11, N'Phòng 5B', 8, 2, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (12, N'Phòng 1C', 7, 3, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (13, N'Phòng 2C', 8, 3, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (14, N'Phòng 3C', 7, 3, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (15, N'Phòng 4C', 5, 3, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (16, N'Phòng 5D', 5, 3, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (17, N'Phòng 1D', 7, 4, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (18, N'Phòng 2D', 8, 4, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (19, N'Phòng 3D', 7, 4, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (20, N'Phòng 4D', 5, 4, 1)
INSERT [dbo].[PHONG] ([maphong], [tenphong], [tsogiuong], [tang], [tinhtrang]) VALUES (21, N'Phòng 5D', 5, 4, 1)
SET IDENTITY_INSERT [dbo].[PHONG] OFF
GO
SET IDENTITY_INSERT [dbo].[TAIKHOAN] ON 

INSERT [dbo].[TAIKHOAN] ([matk], [hoten], [email], [pass], [cvu]) VALUES (1, N'khanh', N'khanh@gmail.com', N'123456', N'ADMIN4')
INSERT [dbo].[TAIKHOAN] ([matk], [hoten], [email], [pass], [cvu]) VALUES (2, N'phat', N'phat@gmail.com', N'123456', N'Nhân viên')
INSERT [dbo].[TAIKHOAN] ([matk], [hoten], [email], [pass], [cvu]) VALUES (3, N'tuan', N'tuan@gmail.com', N'123456', N'Nhân viên')
INSERT [dbo].[TAIKHOAN] ([matk], [hoten], [email], [pass], [cvu]) VALUES (3, N'khoa', N'khoa@gmail.com', N'123456', N'Nhân viên')
SET IDENTITY_INSERT [dbo].[TAIKHOAN] OFF
GO
SET IDENTITY_INSERT [dbo].[VATTU] ON 

INSERT [dbo].[VATTU] ([mavt], [tenvattu], [soluong], [giatien]) VALUES (1, N'Bàn ', 12, 120000)
INSERT [dbo].[VATTU] ([mavt], [tenvattu], [soluong], [giatien]) VALUES (2, N'Giường', 123, 450000)
INSERT [dbo].[VATTU] ([mavt], [tenvattu], [soluong], [giatien]) VALUES (3, N'Tủ', 32, 345000)
INSERT [dbo].[VATTU] ([mavt], [tenvattu], [soluong], [giatien]) VALUES (4, N'Ghế', 76, 65000)
INSERT [dbo].[VATTU] ([mavt], [tenvattu], [soluong], [giatien]) VALUES (5, N'Điều hòa', 43, 2300000)
INSERT [dbo].[VATTU] ([mavt], [tenvattu], [soluong], [giatien]) VALUES (6, N'Máy giặt ', 56, 5600000)
INSERT [dbo].[VATTU] ([mavt], [tenvattu], [soluong], [giatien]) VALUES (8, N'máy tính', 4, 456)
SET IDENTITY_INSERT [dbo].[VATTU] OFF
GO
ALTER TABLE [dbo].[HOADON] ADD  CONSTRAINT [DF_HOADON_ngayhoadon]  DEFAULT (getdate()) FOR [ngaytao]
GO
ALTER TABLE [dbo].[PHIEU_DIENNUOC] ADD  CONSTRAINT [DF_PHIEU_DIENNUOC_ngaytaophieu]  DEFAULT (getdate()) FOR [ngaytaophieu]
GO
ALTER TABLE [dbo].[VATTU_PHONG] ADD  CONSTRAINT [DF_VATTU_PHONG_soluong]  DEFAULT ((1)) FOR [soluong]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [fk_dn_1] FOREIGN KEY([maphieudiennuoc])
REFERENCES [dbo].[PHIEU_DIENNUOC] ([maphieu])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [fk_dn_1]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_PHONG] FOREIGN KEY([maphong])
REFERENCES [dbo].[PHONG] ([maphong])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_PHONG]
GO
ALTER TABLE [dbo].[HOCSINH]  WITH CHECK ADD  CONSTRAINT [fk_ii_1] FOREIGN KEY([maphong])
REFERENCES [dbo].[PHONG] ([maphong])
GO
ALTER TABLE [dbo].[HOCSINH] CHECK CONSTRAINT [fk_ii_1]
GO
ALTER TABLE [dbo].[PHIEU_DIENNUOC]  WITH CHECK ADD  CONSTRAINT [fk_p_2] FOREIGN KEY([maphong])
REFERENCES [dbo].[PHONG] ([maphong])
GO
ALTER TABLE [dbo].[PHIEU_DIENNUOC] CHECK CONSTRAINT [fk_p_2]
GO
ALTER TABLE [dbo].[VATTU_PHONG]  WITH CHECK ADD  CONSTRAINT [FK_VATTU_PHONG_PHONG] FOREIGN KEY([maphong])
REFERENCES [dbo].[PHONG] ([maphong])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VATTU_PHONG] CHECK CONSTRAINT [FK_VATTU_PHONG_PHONG]
GO
ALTER TABLE [dbo].[VATTU_PHONG]  WITH CHECK ADD  CONSTRAINT [FK_VATTU_PHONG_VATTU] FOREIGN KEY([mavt])
REFERENCES [dbo].[VATTU] ([mavt])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VATTU_PHONG] CHECK CONSTRAINT [FK_VATTU_PHONG_VATTU]
GO
USE [master]
GO
ALTER DATABASE [QUAN_LY_KTX] SET  READ_WRITE 
GO




