USE [QLKS]
GO

-- T?o nhân viên m?i v?i role Admin
DECLARE @maNV VARCHAR(20)
SET @maNV = 'NV' + FORMAT(GETDATE(), 'ddMMyyHHmmss')

INSERT INTO [dbo].[NHANVIEN] 
    ([maNV], [tenNV], [gioiTinh], [soNgayPhep], [chucVu], [ngaySinh], [ngayVaoLam], [email], [luong1Ngay], [xuLy])
VALUES 
    (@maNV, N'Admin', 0, 5, 0, CAST('1990-01-01' AS Date), CAST(GETDATE() AS Date), 'admin@hotel.com', 500000, 0)

-- T?o tài kho?n admin
-- M?t kh?u: 123456 (?ã ???c mã hóa MD5)
INSERT INTO [dbo].[TAIKHOAN] 
    ([taiKhoan], [maNV], [matKhau], [tinhTrang], [maPQ], [xuLy])
VALUES 
    ('admin123', @maNV, 'e10adc3949ba59abbe56e057f20f883e', 0, '0', 0)

-- Hi?n th? thông tin tài kho?n v?a t?o
PRINT '=================================================='
PRINT 'TÀI KHO?N ?Ã ???C T?O THÀNH CÔNG!'
PRINT '=================================================='
PRINT 'Tên ??ng nh?p: admin123'
PRINT 'M?t kh?u: 123456'
PRINT 'Mã nhân viên: ' + @maNV
PRINT 'Quy?n h?n: Admin'
PRINT 'Ngày t?o: ' + CONVERT(VARCHAR, GETDATE(), 120)
PRINT '=================================================='

GO
