USE [QLKS]
GO

-- C?p nh?t l?i d? li?u chính xác cho b?ng CHUCNANG
UPDATE [dbo].[CHUCNANG] SET tenChucNang = N'Qu?n lý phòng' WHERE maChucNang = '1';
UPDATE [dbo].[CHUCNANG] SET tenChucNang = N'Qu?n lý d?ch v?' WHERE maChucNang = '2';
UPDATE [dbo].[CHUCNANG] SET tenChucNang = N'Qu?n lý khách hàng' WHERE maChucNang = '3';
UPDATE [dbo].[CHUCNANG] SET tenChucNang = N'Qu?n lý nhân viên' WHERE maChucNang = '4';
UPDATE [dbo].[CHUCNANG] SET tenChucNang = N'Qu?n lý phân quy?n' WHERE maChucNang = '5';
UPDATE [dbo].[CHUCNANG] SET tenChucNang = N'Qu?n lý ??t phòng' WHERE maChucNang = '6';
UPDATE [dbo].[CHUCNANG] SET tenChucNang = N'Qu?n lý hóa ??n' WHERE maChucNang = '7';
UPDATE [dbo].[CHUCNANG] SET tenChucNang = N'Xem th?ng kê' WHERE maChucNang = '8';

-- Hi?n th? k?t qu?
SELECT maChucNang AS [Mã ch?c n?ng], tenChucNang AS [Tên ch?c n?ng]
FROM [dbo].[CHUCNANG]
ORDER BY maChucNang;

PRINT '=========================================='
PRINT 'C?P NH?T THÀNH CÔNG!'
PRINT '=========================================='
PRINT '?ã s?a t?t c? tên ch?c n?ng v? ti?ng Vi?t chính xác'
PRINT '=========================================='

GO
