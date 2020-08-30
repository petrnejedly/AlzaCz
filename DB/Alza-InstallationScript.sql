GO
-- ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
-- ||
-- ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
GO
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [dbo].[sysobjects].[id] = object_id(N'[dbo].[Alza_Products_SelectAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Alza_Products_SelectAll]
GO
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [dbo].[sysobjects].[id] = object_id(N'[dbo].[Alza_Products_SelectPaged]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Alza_Products_SelectPaged]
GO
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [dbo].[sysobjects].[id] = object_id(N'[dbo].[Alza_Products_SelectOneById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Alza_Products_SelectOneById]
GO
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [dbo].[sysobjects].[id] = object_id(N'[dbo].[Alza_ProductsHistory_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Alza_ProductsHistory_Insert]
GO
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [dbo].[sysobjects].[id] = object_id(N'[dbo].[Alza_Products_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Alza_Products_Insert]
GO
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [dbo].[sysobjects].[id] = object_id(N'[dbo].[Alza_Products_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Alza_Products_Update]
GO
-- |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
-- |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
GO
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [dbo].[sysobjects].[id] = object_id(N'[dbo].[Alza_Products]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[Alza_Products]
GO
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [dbo].[sysobjects].[id] = object_id(N'[dbo].[Alza_ProductsHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[Alza_ProductsHistory]
GO
-- ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
-- ||
-- ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
GO
CREATE TABLE [dbo].[Alza_Products]
(
	[SysID] [int] IDENTITY (1, 1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[ImgUri] [nvarchar](255) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[CreatedByIP] [nvarchar](255) NOT NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[ModifiedByIP] [nvarchar](255) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Alza_Products] WITH NOCHECK
ADD CONSTRAINT [PK_Alza_Products] PRIMARY KEY CLUSTERED
(
	[SysID]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Alza_Products] ADD
CONSTRAINT [DF_Alza_Products_Price] DEFAULT (0) FOR [Price],
CONSTRAINT [DF_Alza_Products_Created] DEFAULT (GETDATE()) FOR [Created],
CONSTRAINT [DF_Alza_Products_CreatedBy] DEFAULT (SUSER_SNAME()) FOR [CreatedBy],
CONSTRAINT [DF_Alza_Products_CreatedByIP] DEFAULT (N'0.0.0.0') FOR [CreatedByIP]
GO
EXEC [sys].[sp_addextendedproperty] @name = N'MS_Description', @value = 'Product name', @level0type = N'Schema', @level0name = 'dbo', @level1type = N'Table', @level1name = 'Alza_Products', @level2type = N'Column', @level2name = 'Name';
EXEC [sys].[sp_addextendedproperty] @name = N'MS_Description', @value = 'Product image relative URI', @level0type = N'Schema', @level0name = 'dbo', @level1type = N'Table', @level1name = 'Alza_Products', @level2type = N'Column', @level2name = 'ImgUri';
EXEC [sys].[sp_addextendedproperty] @name = N'MS_Description', @value = 'Product price', @level0type = N'Schema', @level0name = 'dbo', @level1type = N'Table', @level1name = 'Alza_Products', @level2type = N'Column', @level2name = 'Price';
EXEC [sys].[sp_addextendedproperty] @name = N'MS_Description', @value = 'Product description', @level0type = N'Schema', @level0name = 'dbo', @level1type = N'Table', @level1name = 'Alza_Products', @level2type = N'Column', @level2name = 'Description';
GO
CREATE TABLE [dbo].[Alza_ProductsHistory]
(
	[SysID] [int] IDENTITY (1, 1) NOT NULL,
	[FKProductID] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[ImgUri] [nvarchar](255) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[CreatedByIP] [nvarchar](255) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Alza_ProductsHistory] WITH NOCHECK
ADD CONSTRAINT [PK_Alza_ProductsHistory] PRIMARY KEY CLUSTERED
(
	[SysID]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Alza_ProductsHistory] ADD
CONSTRAINT [DF_Alza_ProductsHistory_FKProductID] DEFAULT (0) FOR [FKProductID],
CONSTRAINT [DF_Alza_ProductsHistory_Created] DEFAULT (GETDATE()) FOR [Created],
CONSTRAINT [DF_Alza_ProductsHistory_CreatedBy] DEFAULT (SUSER_SNAME()) FOR [CreatedBy],
CONSTRAINT [DF_Alza_ProductsHistory_CreatedByIP] DEFAULT (N'0.0.0.0') FOR [CreatedByIP]
GO
-- |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
-- |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
GO
CREATE PROCEDURE [dbo].[Alza_Products_SelectAll]
--WITH ENCRYPTION
AS
SELECT
	[dbo].[Alza_Products].[SysID],
	[dbo].[Alza_Products].[Name],
	[dbo].[Alza_Products].[ImgUri],
	[dbo].[Alza_Products].[Price],
	[dbo].[Alza_Products].[Description],
	[dbo].[Alza_Products].[Created],
	[dbo].[Alza_Products].[CreatedBy],
	[dbo].[Alza_Products].[CreatedByIP],
	[dbo].[Alza_Products].[Modified],
	[dbo].[Alza_Products].[ModifiedBy],
	[dbo].[Alza_Products].[ModifiedByIP]
FROM
	[dbo].[Alza_Products]
ORDER BY
	[dbo].[Alza_Products].[SysID] ASC	;
GO
CREATE PROCEDURE [dbo].[Alza_Products_SelectPaged]
(
	@PageSize INT = 10,
	@CurrentPageIndex INT = 0
)
--WITH ENCRYPTION
AS
DECLARE @ItemsCount INT = (SELECT COUNT(*) FROM [dbo].[Alza_Products]);
DECLARE @PagesCount INT = CEILING(@ItemsCount * 1.0 / @PageSize);

SELECT
	@ItemsCount AS [VirtualtemsCount],
	[CurrentPageIndex] = CASE WHEN @CurrentPageIndex < @PagesCount THEN @CurrentPageIndex ELSE NULL END,
	@PageSize AS [PageSize],
	@PagesCount AS [PagesCount]

DECLARE @RowStart INT = @PageSize * @CurrentPageIndex + 1;
DECLARE @RowEnd INT = @RowStart + @PageSize - 1;

BEGIN
	WITH [AlzaProducts] AS
		(
			SELECT
				[dbo].[Alza_Products].[SysID],
				[dbo].[Alza_Products].[Name],
				[dbo].[Alza_Products].[ImgUri],
				[dbo].[Alza_Products].[Price],
				[dbo].[Alza_Products].[Description],
				[dbo].[Alza_Products].[Created],
				[dbo].[Alza_Products].[CreatedBy],
				[dbo].[Alza_Products].[CreatedByIP],
				[dbo].[Alza_Products].[Modified],
				[dbo].[Alza_Products].[ModifiedBy],
				[dbo].[Alza_Products].[ModifiedByIP],
				ROW_NUMBER() OVER (ORDER BY [dbo].[Alza_Products].[SysID] ASC) AS [RowNumber]
			FROM
				[dbo].[Alza_Products]
		)
	SELECT
	*
	FROM
		[AlzaProducts]
	WHERE
		[AlzaProducts].[RowNumber] >= @RowStart AND [AlzaProducts].[RowNumber] <= @RowEnd
END
GO
CREATE PROCEDURE [dbo].[Alza_Products_SelectOneById]
(
	@ProductID INT
)
--WITH ENCRYPTION
AS
SELECT TOP 1
	[dbo].[Alza_Products].[SysID],
	[dbo].[Alza_Products].[Name],
	[dbo].[Alza_Products].[ImgUri],
	[dbo].[Alza_Products].[Price],
	[dbo].[Alza_Products].[Description],
	[dbo].[Alza_Products].[Created],
	[dbo].[Alza_Products].[CreatedBy],
	[dbo].[Alza_Products].[CreatedByIP],
	[dbo].[Alza_Products].[Modified],
	[dbo].[Alza_Products].[ModifiedBy],
	[dbo].[Alza_Products].[ModifiedByIP]
FROM
	[dbo].[Alza_Products]
WHERE
	[dbo].[Alza_Products].[SysID] = @ProductID	;
GO
CREATE PROCEDURE [dbo].[Alza_ProductsHistory_Insert]
(
	@ProductID INT
)
--WITH ENCRYPTION
AS
INSERT INTO
	[dbo].[Alza_ProductsHistory]
	(
		[dbo].[Alza_ProductsHistory].[FKProductID],
		[dbo].[Alza_ProductsHistory].[Name],
		[dbo].[Alza_ProductsHistory].[ImgUri],
		[dbo].[Alza_ProductsHistory].[Price],
		[dbo].[Alza_ProductsHistory].[Description],
		[dbo].[Alza_ProductsHistory].[Created],
		[dbo].[Alza_ProductsHistory].[CreatedBy],
		[dbo].[Alza_ProductsHistory].[CreatedByIP]
	)
	SELECT TOP 1
		[dbo].[Alza_Products].[SysID],
		[dbo].[Alza_Products].[Name],
		[dbo].[Alza_Products].[ImgUri],
		[dbo].[Alza_Products].[Price],
		[dbo].[Alza_Products].[Description],
		ISNULL([dbo].[Alza_Products].[Modified], [dbo].[Alza_Products].[Created]) AS [Modified],
		ISNULL([dbo].[Alza_Products].[ModifiedBy], [dbo].[Alza_Products].[CreatedBy]) AS [ModifiedBy],
		ISNULL([dbo].[Alza_Products].[ModifiedByIP], [dbo].[Alza_Products].[CreatedByIP]) AS [ModifiedByIP]
	FROM
		[dbo].[Alza_Products]
	WHERE
		[dbo].[Alza_Products].[SysID] = @ProductID	;
GO
CREATE PROCEDURE [dbo].[Alza_Products_Insert]
(
	@Name NVARCHAR(255),
	@ImgUri NVARCHAR(255),
	@Price DECIMAL(18, 2) = 0.00,
	@Description NVARCHAR(MAX),
	@IdentityName NVARCHAR(255),
	@IdentityIP NVARCHAR(255),
	@ProductID INT OUT
)
--WITH ENCRYPTION
AS
SET @ProductID = 0;
DECLARE @SYSDATE DATETIME = GETDATE()	;

INSERT INTO
	[dbo].[Alza_Products]
	(
		[dbo].[Alza_Products].[Name],
		[dbo].[Alza_Products].[ImgUri],
		[dbo].[Alza_Products].[Price],
		[dbo].[Alza_Products].[Description],
		[dbo].[Alza_Products].[Created],
		[dbo].[Alza_Products].[CreatedBy],
		[dbo].[Alza_Products].[CreatedByIP],
		[dbo].[Alza_Products].[Modified],
		[dbo].[Alza_Products].[ModifiedBy],
		[dbo].[Alza_Products].[ModifiedByIP]
	)
	VALUES
	(
		@Name,
		@ImgUri,
		@Price,
		@Description,
		@SYSDATE,
		@IdentityName,
		@IdentityIP,
		NULL,
		NULL,
		NULL
	)
SET @ProductID = SCOPE_IDENTITY()	;

EXEC [dbo].[Alza_ProductsHistory_Insert] @ProductID	;
GO
CREATE PROCEDURE [dbo].[Alza_Products_Update]
(
	@ProductID INT,
	@Description NVARCHAR(MAX),
	@IdentityName NVARCHAR(255),
	@IdentityIP NVARCHAR(255)
)
--WITH ENCRYPTION
AS
DECLARE @SYSDATE DATETIME = GETDATE()	;

UPDATE
	[dbo].[Alza_Products]
SET
	[dbo].[Alza_Products].[Description] = @Description,
	[dbo].[Alza_Products].[Modified] = @SYSDATE,
	[dbo].[Alza_Products].[ModifiedBy] = @IdentityName,
	[dbo].[Alza_Products].[ModifiedByIP] = @IdentityIP
WHERE
	[dbo].[Alza_Products].[SysID] = @ProductID	;

EXEC [dbo].[Alza_ProductsHistory_Insert] @ProductID	;
GO
-- ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
-- ||
-- ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
GO
TRUNCATE TABLE [dbo].[Alza_ProductsHistory]
TRUNCATE TABLE [dbo].[Alza_Products]
GO
DECLARE @ProductID INT = 0
DECLARE @UserID INT = 0
DECLARE @IdentityName NVARCHAR(255) = N'Web'
DECLARE @IdentityIP NVARCHAR(255) = N'0.0.0.0'

--EXEC [dbo].[Alza_Products_Insert] @Name, @ImgUri, @Price, @Description, @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Samsung Galaxy S20 šedá', N'/ImgW.ashx?fd=f3&cd=SAMO0187b3', 22999.00, N'Mobilní telefon 6,2" AMOLED 3200 × 1440, procesor Samsung Exynos 990 8jádrový, RAM 8 GB, interní paměť 128 GB, Micro SDXC až 1000 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,8) + 64 Mpx (f/2) + 12 Mpx (f/2,2), přední fotoaparát 10 Mpx, optická stabilizace, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků v displeji, voděodolný dle IP68, hybridní slot, neblokovaný, rychlé nabíjení 25W, bezdrátové nabíjení 15W, reverzní nabíjení 9W, baterie 4000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Samsung Galaxy S20+ šedá', N'/ImgW.ashx?fd=f3&cd=SAMO0188b3', 25190.00, N'Mobilní telefon 6,7" AMOLED 3200 × 1440, procesor Samsung Exynos 990 8jádrový, RAM 8 GB, interní paměť 128 GB, Micro SDXC až 1000 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,8) + 64 Mpx (f/2) + 12 Mpx (f/2,2), přední fotoaparát 10 Mpx, optická stabilizace, ToF, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků v displeji, voděodolný dle IP68, hybridní slot + eSIM, neblokovaný, rychlé nabíjení 25W, bezdrátové nabíjení 15W, reverzní nabíjení 9W, baterie 4500 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Samsung Galaxy S20 Ultra 5G šedá', N'/ImgW.ashx?fd=f3&cd=SAMO0189b2', 37490.00, N'Mobilní telefon 6,9" AMOLED 3200 × 1440, procesor Samsung Exynos 990 8jádrový, RAM 12 GB, interní paměť 128 GB, Micro SDXC až 1000 GB, zadní fotoaparát s optickým zoomem 108 Mpx (f/1,8) + 48 Mpx (f/3,5) + 12 Mpx (f/2,2), přední fotoaparát 40 Mpx, optická stabilizace, ToF, GPS, Glonass, NFC, LTE, 5G, USB-C, čtečka otisků v displeji, voděodolný dle IP68, hybridní slot + eSIM, neblokovaný, rychlé nabíjení 45W, bezdrátové nabíjení 15W, reverzní nabíjení 9W, baterie 5000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Xiaomi Mi 10 5G 256GB šedá', N'/ImgW.ashx?fd=f3&cd=XI207c1', 22999.00, N'Mobilní telefon 6,67" AMOLED 2340 × 1080, 8jádrový procesor, RAM 8 GB, interní paměť 256 GB, zadní fotoaparát 108 Mpx (f/1,7) + 13 Mpx (f/2,4) + 2 Mpx (f/2,4) + 2 Mpx (f/2,4), přední fotoaparát 20 Mpx, optická stabilizace, GPS, Glonass, IrDA, NFC, LTE, 5G, USB-C, čtečka otisků v displeji, single SIM, neblokovaný, rychlé nabíjení 30W, bezdrátové nabíjení 30W, baterie 4780 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Xiaomi Mi Note 10 LTE 128GB černá', N'/ImgW.ashx?fd=f3&cd=XI205b2', 12990.00, N'Mobilní telefon 6,47" AMOLED 2340 × 1080, procesor Qualcomm Snapdragon 730 8jádrový, RAM 6 GB, interní paměť 128 GB, zadní fotoaparát s optickým zoomem 108 Mpx (f/1,7) + 12 Mpx (f/2) + 20 Mpx (f/2) + 2 Mpx (f/2,4), přední fotoaparát 32 Mpx, optická stabilizace, GPS, Glonass, IrDA, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 30W, baterie 5260 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Xiaomi Poco F2 Pro LTE 128GB šedá', N'/ImgW.ashx?fd=f3&cd=XI213b2', 11990.00, N'Mobilní telefon 6,67" AMOLED 2400 × 1080, procesor Qualcomm Snapdragon 865 8jádrový, RAM 6 GB, interní paměť 128 GB, zadní fotoaparát 64 Mpx (f/1,9) + 13 Mpx (f/2,4) + 5 Mpx (f/2,2) + 2 Mpx (f/2,4), přední fotoaparát 20 Mpx, elektronická stabilizace, GPS, Glonass, IrDA, NFC, LTE, 5G, Jack (3,5mm) a USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 30W, baterie 4700 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Huawei P40 Pro šedá', N'/ImgW.ashx?fd=f3&cd=HU3147b3', 24999.00, N'Mobilní telefon 6,58" OLED 2640 × 1200, procesor HiSilicon Kirin 990 8jádrový, RAM 8 GB, interní paměť 256 GB, NM Card až 256 GB, zadní fotoaparát s optickým zoomem 50 Mpx (f/1,9) + 12 Mpx (f/3,4) + 40 Mpx (f/1,8), přední fotoaparát 32 Mpx, optická stabilizace, GPS, Glonass, NFC, LTE, 5G, USB-C, čtečka otisků v displeji, voděodolný dle IP68, hybridní slot, neblokovaný, baterie 4100 mAh, Android 10, Huawei Mobile Services', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Huawei P40 šedá', N'/ImgW.ashx?fd=f3&cd=HU3146b3', 17999.00, N'Mobilní telefon 6,1" OLED 2340 × 1080, procesor HiSilicon Kirin 990 8jádrový, RAM 8 GB, interní paměť 128 GB, NM Card až 256 GB, zadní fotoaparát s optickým zoomem 50 Mpx (f/1,9) + 8 Mpx (f/2,4) + 16 Mpx (f/2,2), přední fotoaparát 32 Mpx, optická stabilizace, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků v displeji, hybridní slot, neblokovaný, rychlé nabíjení 22,5W, baterie 3800 mAh, Android 10, Huawei Mobile Services', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'HUAWEI P30 černá', N'/ImgW.ashx?fd=f3&cd=HU3134b1', 12999.00, N'Mobilní telefon 6,1" OLED 2340 × 1080, procesor HiSilicon Kirin 980 8jádrový, RAM 6 GB, interní paměť 128 GB, NM Card až 256 GB, zadní fotoaparát s optickým zoomem 40 Mpx (f/1,8) + 8 Mpx (f/2,4) + 16 Mpx (f/2,2), přední fotoaparát 32 Mpx, optická stabilizace, GPS, Glonass, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 22,5W, baterie 3650 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'iPhone 11 Pro Max 512GB zlatá', N'/ImgW.ashx?fd=f3&cd=RI030d4', 39999.00, N'Mobilní telefon 6,5" OLED 2688 × 1242, procesor Apple A13 Bionic 6jádrový, RAM 4 GB, interní paměť 512 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,8) + 12 Mpx (f/2) + 12 Mpx (f/2,4), přední fotoaparát 12 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, Lightning port, voděodolný dle IP68, single SIM + eSIM, neblokovaný, rychlé nabíjení 18W, bezdrátové nabíjení, baterie 3969 mAh, iOS 13', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'iPhone 11 256GB černá', N'/ImgW.ashx?fd=f3&cd=RI028d2', 24999.00, N'Mobilní telefon 6,1" IPS 1792 × 828, procesor Apple A13 Bionic 6jádrový, RAM 4 GB, interní paměť 256 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,8) + 12 Mpx (f/2,4), přední fotoaparát 12 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, Lightning port, voděodolný dle IP68, single SIM + eSIM, neblokovaný, rychlé nabíjení 18W, bezdrátové nabíjení, baterie 3110 mAh, iOS 13', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'iPhone 11 128GB černá', N'/ImgW.ashx?fd=f3&cd=RI028c2', 19999.00, N'Mobilní telefon 6,1" IPS 1792 × 828, procesor Apple A13 Bionic 6jádrový, RAM 4 GB, interní paměť 128 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,8) + 12 Mpx (f/2,4), přední fotoaparát 12 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, Lightning port, voděodolný dle IP68, single SIM + eSIM, neblokovaný, rychlé nabíjení 18W, bezdrátové nabíjení, baterie 3110 mAh, iOS 13', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'iPhone Xr 128GB černá', N'/ImgW.ashx?fd=f3&cd=RI027c2', 19490.00, N'Mobilní telefon 6,1" IPS 1792 × 828, procesor Apple A12 Bionic 6jádrový, RAM 3 GB, interní paměť 128 GB, zadní fotoaparát 12 Mpx (f/1,8), přední fotoaparát 7 Mpx, optická stabilizace, GPS, Glonass, NFC, LTE, Lightning port, voděodolný dle IP67, single SIM + eSIM, neblokovaný, rychlé nabíjení 15W, bezdrátové nabíjení, baterie 2942 mAh, iOS 12', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'iPhone Xs 64GB stříbrná', N'/ImgW.ashx?fd=f3&cd=RI025b1', 18999.00, N'Mobilní telefon 5,8" P-OLED 2436 × 1125, procesor Apple A12 Bionic 6jádrový, RAM 4 GB, interní paměť 64 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,8) + 12 Mpx (f/2,4), přední fotoaparát 7 Mpx, optická stabilizace, GPS, Glonass, NFC, LTE, Lightning port, voděodolný dle IP68, single SIM + eSIM, neblokovaný, rychlé nabíjení 15W, bezdrátové nabíjení, baterie 2658 mAh, iOS 12', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'iPhone 11 64GB černá', N'/ImgW.ashx?fd=f3&cd=RI028b2', 17999.00, N'Mobilní telefon 6,1" IPS 1792 × 828, procesor Apple A13 Bionic 6jádrový, RAM 4 GB, interní paměť 64 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,8) + 12 Mpx (f/2,4), přední fotoaparát 12 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, Lightning port, voděodolný dle IP68, single SIM + eSIM, neblokovaný, rychlé nabíjení 18W, bezdrátové nabíjení, baterie 3110 mAh, iOS 13', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'OnePlus 8 Pro 256GB zelená', N'/ImgW.ashx?fd=f3&cd=ONE0073c3', 25999.00, N'Mobilní telefon 6,78" AMOLED 3168 × 1440, procesor Qualcomm Snapdragon 865 8jádrový, RAM 12 GB, interní paměť 256 GB, zadní fotoaparát s optickým zoomem 48 Mpx (f/1,78) + 8 Mpx (f/2,4) + 48 Mpx (f/2,2) + 5 Mpx (f/2,4), přední fotoaparát 16 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, 5G, USB-C, čtečka otisků v displeji, voděodolný dle IP68, dual SIM, neblokovaný, rychlé nabíjení 30W, bezdrátové nabíjení 30W, baterie 4510 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'OnePlus 8 256GB Interstellar Glow', N'/ImgW.ashx?fd=f3&cd=ONE0072c1', 17999.00, N'Mobilní telefon 6,55" AMOLED 2400 × 1080, procesor Qualcomm Snapdragon 865 8jádrový, RAM 12 GB, interní paměť 256 GB, zadní fotoaparát 48 Mpx (f/1,75) + 16 Mpx (f/2,2) + 2 Mpx (f/2,4), přední fotoaparát 16 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, 5G, USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 30W, baterie 4300 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'OnePlus 7T Pro 256GB modrá', N'/ImgW.ashx?fd=f3&cd=ONE0071b1', 14999.00, N'Mobilní telefon 6,67" AMOLED 3120 × 1440, procesor Qualcomm Snapdragon 855+ 8jádrový, RAM 8 GB, interní paměť 256 GB, zadní fotoaparát s optickým zoomem 48 Mpx (f/1,6) + 8 Mpx (f/2,4) + 16 Mpx (f/2,2), přední fotoaparát 16 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 30W, baterie 4085 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'OnePlus Nord 256GB šedá', N'/ImgW.ashx?fd=f3&cd=ONE0074c2', 13999.00, N'Mobilní telefon 6,44" AMOLED 2400 × 1080, procesor Qualcomm Snapdragon 765G 8jádrový, RAM 12 GB, interní paměť 256 GB, zadní fotoaparát 48 Mpx (f/1,75) + 8 Mpx (f/2,25) + 2 Mpx (f/2,4) + 5 Mpx (f/2,4), přední fotoaparát 32 Mpx + 8 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, 5G, USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 30W, baterie 4115 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'OnePlus 7T gradientní modrá', N'/ImgW.ashx?fd=f3&cd=ONE0006b1', 13499.00, N'Mobilní telefon 6,55" AMOLED 2400 × 1080, procesor Qualcomm Snapdragon 855+ 8jádrový, RAM 8 GB, interní paměť 128 GB, zadní fotoaparát s optickým zoomem 48 Mpx (f/1,6) + 12 Mpx (f/2,2) + 16 Mpx (f/2,2), přední fotoaparát 16 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 30W, baterie 3800 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Motorola Edge+ 256GB šedá', N'/ImgW.ashx?fd=f3&cd=SMTR5046b1', 27990.00, N'Mobilní telefon 6,7" OLED 2340 × 1080, procesor Qualcomm Snapdragon 865 8jádrový, RAM 12 GB, interní paměť 256 GB, zadní fotoaparát s optickým zoomem 108 Mpx (f/1,8) + 8 Mpx (f/2,4) + 16 Mpx (f/2,2), přední fotoaparát 25 Mpx, optická stabilizace, ToF, GPS, Glonass, NFC, LTE, 5G, Jack (3,5mm) a USB-C, čtečka otisků v displeji, single SIM, neblokovaný, rychlé nabíjení 18W, bezdrátové nabíjení 15W, baterie 5000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Motorola Razr eSIM zlatá', N'/ImgW.ashx?fd=f3&cd=SMTR5047b2', 26990.00, N'Mobilní telefon 6,2" P-OLED 2142 × 876, procesor Qualcomm Snapdragon 710 8jádrový, RAM 6 GB, interní paměť 128 GB, zadní fotoaparát 16 Mpx (f/1,7), přední fotoaparát 5 Mpx, elektronická stabilizace, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků, eSIM, neblokovaný, rychlé nabíjení 15W, baterie 2510 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Motorola Edge 128GB Dual SIM černá', N'/ImgW.ashx?fd=f3&cd=SMTR5045b1', 14999.00, N'Mobilní telefon 6,7" OLED 2340 × 1080, procesor Qualcomm Snapdragon 765G 8jádrový, RAM 6 GB, interní paměť 128 GB, Micro SD až 1000 GB, zadní fotoaparát s optickým zoomem 64 Mpx (f/1,8) + 8 Mpx (f/2,4) + 16 Mpx (f/2,2), přední fotoaparát 25 Mpx, ToF, GPS, Glonass, NFC, LTE, 5G, Jack (3,5mm) a USB-C, čtečka otisků v displeji, hybridní slot, neblokovaný, rychlé nabíjení 15W, baterie 4500 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Motorola Moto G8 Plus červená', N'/ImgW.ashx?fd=f3&cd=SMTR5038b1', 5399.00, N'Mobilní telefon 6,3" LTPS 2280 × 1080, procesor Qualcomm Snapdragon 665 8jádrový, RAM 4 GB, interní paměť 64 GB, Micro SD až 512 GB, zadní fotoaparát 48 Mpx (f/1,7) + 16 Mpx (f/2,2) + 5 Mpx (f/2,2), přední fotoaparát 25 Mpx, elektronická stabilizace, GPS, Glonass, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM, neblokovaný, rychlé nabíjení 15W, baterie 4000 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Motorola Moto G9 Play 64GB zelená + Moto Buds', N'/ImgW.ashx?fd=f3&cd=SMTR5052b1', 4999.00, N'Mobilní telefon 6,5" IPS 1600 × 720, 8jádrový procesor, RAM 4 GB, interní paměť 64 GB, Micro SD až 512 GB, zadní fotoaparát 48 Mpx (f/1,7) + 2 Mpx + 2 Mpx, přední fotoaparát 8 Mpx, GPS, Glonass, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM + paměťová karta, neblokovaný, baterie 5000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Google Pixel 4 64GB černá', N'/ImgW.ashx?fd=f3&cd=GPX1063b2', 15999.00, N'Mobilní telefon 5,7" P-OLED 2280 × 1080, procesor Qualcomm Snapdragon 855 8jádrový, RAM 6 GB, interní paměť 64 GB, zadní fotoaparát s optickým zoomem 12,2 Mpx (f/1,7) + 16 Mpx (f/2,4), přední fotoaparát 8 Mpx, optická stabilizace, GPS, Glonass, NFC, LTE, USB-C, voděodolný dle IP68, single SIM + eSIM, rychlé nabíjení 18W, bezdrátové nabíjení, baterie 2800 mAh, Android 10.0', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Google Pixel 3XL 128GB černá', N'/ImgW.ashx?fd=f3&cd=GPX1060a5a', 14999.00, N'Mobilní telefon 6,3" P-OLED 2960 × 1440, procesor Qualcomm Snapdragon 845 8jádrový, RAM 4 GB, interní paměť 128 GB, zadní fotoaparát 12 Mpx (f/1,8), přední fotoaparát 8 Mpx + 8 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků, voděodolný dle IP68, single SIM + eSIM, rychlé nabíjení, bezdrátové nabíjení, baterie 3430 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Google Pixel 4a černá', N'/ImgW.ashx?fd=f3&cd=GPX1065b1', 0.00, N'Mobilní telefon 5,81" OLED 2340 × 1080, 8jádrový procesor, RAM 6 GB, interní paměť 128 GB, zadní fotoaparát 12 Mpx (f/1,7), přední fotoaparát 8 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, single SIM, rychlé nabíjení, baterie 3140 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Land Rover Explore R (2020) černá', N'/ImgW.ashx?fd=f3&cd=LND002', 11999.00, N'Mobilní telefon 5,65" IPS, 8jádrový procesor, RAM 4 GB, interní paměť 64 GB, zadní fotoaparát 12 Mpx (f/1,8), přední fotoaparát 8 Mpx, elektronická stabilizace, GPS, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, voděodolný dle IP68, dual SIM + paměťová karta, neblokovaný, baterie 3100 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Land Rover Explore', N'/ImgW.ashx?fd=f3&cd=LND001', 6599.00, N'Mobilní telefon 5" TFT 1920 × 1080, 10jádrový procesor, RAM 4 GB, interní paměť 64 GB, Micro SD až 256 GB, zadní fotoaparát 16 Mpx, přední fotoaparát 8 Mpx, GPS, Glonass, NFC, LTE, Jack (3,5mm) a USB-C, voděodolný dle IP68, hybridní slot, nárazuodolný, neblokovaný, baterie 4000 mAh, Android 8.0 Oreo', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Lenovo K10 Note černá', N'/ImgW.ashx?fd=f3&cd=LE1093b1', 4199.00, N'Mobilní telefon 6,3" IPS 2340 × 1080, procesor Qualcomm Snapdragon 710 8jádrový, RAM 6 GB, interní paměť 128 GB, Micro SD až 256 GB, zadní fotoaparát s optickým zoomem 16 Mpx (f/1,8) + 8 Mpx (f/2,4) + 2 Mpx (f/2,2), přední fotoaparát 16 Mpx, GPS, LTE, USB-C, čtečka otisků, dual SIM, neblokovaný, rychlé nabíjení 18W, baterie 4050 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Lenovo K5 Pro 64GB Dual Sim černá', N'/ImgW.ashx?fd=f3&cd=LE1097b1', 3199.00, N'Mobilní telefon 5,99" IPS 2160 × 1080, procesor Qualcomm Snapdragon 636 8jádrový, RAM 4 GB, interní paměť 64 GB, Micro SD až 256 GB, zadní fotoaparát 16 Mpx (f/2) + 5 Mpx (f/2,4), přední fotoaparát 16 Mpx + 5 Mpx, GPS, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM, neblokovaný, rychlé nabíjení 18W, baterie 4050 mAh, Android 8.1', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Lenovo S5 Pro 64GB Dual Sim černá', NULL, 2999.00, N'Mobilní telefon 6,2" IPS 2246 × 1080, procesor Qualcomm Snapdragon 636 8jádrový, RAM 6 GB, interní paměť 64 GB, Micro SD až 256 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,8), přední fotoaparát 20 Mpx, GPS, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM, neblokovaný, rychlé nabíjení 18W, baterie 3500 mAh, Android 8.1', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Lenovo K9 4GB černá', N'/ImgW.ashx?fd=f3&cd=LE1090c1', 2799.00, N'Mobilní telefon 5,7" IPS 1440 × 720, 8jádrový procesor, RAM 4 GB, interní paměť 32 GB, Micro SD až 256 GB, zadní fotoaparát 13 Mpx (f/2,2) + 5 Mpx (f/2,4), přední fotoaparát 13 Mpx + 5 Mpx, GPS, Glonass, LTE, Jack (3,5mm) a USB-C, čtečka otisků, hybridní slot, neblokovaný, rychlé nabíjení 10W, baterie 3000 mAh, Android 8.1 Oreo', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'LG Velvet šedá', N'/ImgW.ashx?fd=f3&cd=LGE2307b1', 15999.00, N'Mobilní telefon 6,8" OLED 2460 × 1080, procesor Qualcomm Snapdragon 765G 8jádrový, RAM 6 GB, interní paměť 128 GB, Micro SDXC až 2048 GB, zadní fotoaparát 48 Mpx (f/1,8) + 8 Mpx (f/2,2) + 5 Mpx (f/2,4), přední fotoaparát 16 Mpx, elektronická stabilizace, GPS, Glonass, NFC, LTE, 5G, Jack (3,5mm) a USB-C, čtečka otisků v displeji, voděodolný dle IP68, single SIM, neblokovaný, rychlé nabíjení 25W, bezdrátové nabíjení, baterie 4300 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'LG K61 šedá', N'/ImgW.ashx?fd=f3&cd=LGE2306b1', 5999.00, N'Mobilní telefon 6,53" IPS 2340 × 1080, procesor MediaTek MT6765 8jádrový, RAM 4 GB, interní paměť 128 GB, Micro SDXC až 2000 GB, zadní fotoaparát 48 Mpx (f/1,79) + 8 Mpx (f/2,2) + 2 Mpx (f/2,4) + 5 Mpx (f/2,4), přední fotoaparát 16 Mpx, GPS, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM + paměťová karta, neblokovaný, baterie 4000 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'LG K51S šedá', N'/ImgW.ashx?fd=f3&cd=LGE2305b2', 4399.00, N'Mobilní telefon 6,55" IPS 1600 × 720, procesor MediaTek MT6765 8jádrový, RAM 3 GB, interní paměť 64 GB, Micro SDXC až 2000 GB, zadní fotoaparát 32 Mpx (f/1,8) + 5 Mpx (f/2,2) + 2 Mpx (f/2,4) + 2 Mpx (f/2,4), přední fotoaparát 13 Mpx, GPS, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM + paměťová karta, neblokovaný, baterie 4000 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'LG K41S černá', N'/ImgW.ashx?fd=f3&cd=LGE2304b1', 4290.00, N'Mobilní telefon 6,55" IPS 1600 × 720, procesor MediaTek MT6762 (Helio P22) 8jádrový, RAM 3 GB, interní paměť 32 GB, Micro SDXC až 2000 GB, zadní fotoaparát 13 Mpx (f/2) + 5 Mpx (f/2,2) + 2 Mpx (f/2,4) + 2 Mpx (f/2,4), přední fotoaparát 8 Mpx, GPS, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM + paměťová karta, neblokovaný, baterie 4000 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Sony Xperia 1 II černá', N'/ImgW.ashx?fd=f3&cd=SOM092b1', 32990.00, N'Mobilní telefon 6,5" OLED 3840 × 1644, procesor Qualcomm Snapdragon 865 8jádrový, RAM 8 GB, interní paměť 256 GB, Micro SDXC až 1000 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,7) + 12 Mpx (f/2,4) + 12 Mpx (f/2,2), přední fotoaparát 8 Mpx, optická stabilizace, ToF, GPS, Glonass, NFC, LTE, 5G, Jack (3,5mm) a USB-C, čtečka otisků, voděodolný dle IP68, single SIM, neblokovaný, rychlé nabíjení 21W, bezdrátové nabíjení, baterie 4000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Sony Xperia 1 černá', N'/ImgW.ashx?fd=f3&cd=SOM082b1', 19990.00, N'Mobilní telefon 6,5" OLED 3840 × 1644, procesor Qualcomm Snapdragon 855 8jádrový, RAM 6 GB, interní paměť 128 GB, Micro SDXC až 512 GB, zadní fotoaparát s optickým zoomem 12 Mpx (f/1,6) + 12 Mpx (f/2,4) + 12 Mpx (f/2,4), přední fotoaparát 8 Mpx, optická a elektronická stabilizace, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků, voděodolný dle IP68, hybridní slot, neblokovaný, rychlé nabíjení 18W, baterie 3330 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'TCL 10Plus 64GB stříbrná', N'/ImgW.ashx?fd=f3&cd=TCL111b2', 7990.00, N'Mobilní telefon 6,47" AMOLED 2340 × 1080, procesor Qualcomm Snapdragon 665 8jádrový, RAM 6 GB, interní paměť 64 GB, Micro SDXC až 256 GB, zadní fotoaparát 48 Mpx (f/1,8) + 8 Mpx (f/2,2) + 2 Mpx (f/2,4) + 2 Mpx (f/2,4), přední fotoaparát 16 Mpx, GPS, Glonass, NFC, LTE, USB-C, čtečka otisků v displeji, dual SIM + paměťová karta, neblokovaný, rychlé nabíjení 18W, baterie 4500 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'TCL Plex bílá', N'/ImgW.ashx?fd=f3&cd=ALCT110b1', 5490.00, N'Mobilní telefon 6,53" IPS 2340 × 1080, 8jádrový procesor, RAM 6 GB, interní paměť 128 GB, Micro SD až 256 GB, zadní fotoaparát 48 Mpx (f/1,8) + 16 Mpx (f/2,4), přední fotoaparát 24 Mpx, GPS, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, hybridní slot, neblokovaný, rychlé nabíjení 18W, baterie 3820 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Oppo Reno3 Pro gradientní černá', N'/ImgW.ashx?fd=f3&cd=OPP2004b1', 12999.00, N'Mobilní telefon 6,4" AMOLED 2400 × 1080, procesor Mediatek Helio P95 8jádrový, RAM 12 GB, interní paměť 256 GB, Micro SD až 256 GB, zadní fotoaparát s optickým zoomem 48 Mpx (f/1,7) + 13 Mpx (f/2,4) + 8 Mpx (f/2,2) + 2 Mpx (f/2,4), přední fotoaparát 44 Mpx, elektronická stabilizace, GPS, Glonass, LTE, USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 30W, baterie 4025 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Oppo A91 černá', N'/ImgW.ashx?fd=f3&cd=OPP2002b1', 6999.00, N'Mobilní telefon 6,4" AMOLED 2400 × 1080, procesor MediaTek MT6771 (Helio P60) 8jádrový, RAM 8 GB, interní paměť 128 GB, zadní fotoaparát 48 Mpx (f/1,8) + 8 Mpx (f/2,2) + 2 Mpx (f/2,4) + 2 Mpx (f/2,4), přední fotoaparát 16 Mpx, elektronická stabilizace, GPS, Glonass, LTE, Jack (3,5mm) a USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení, baterie 4025 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Nokia 6.2 Dual SIM černá', N'/ImgW.ashx?fd=f3&cd=NOK2806b1', 5899.00, N'Mobilní telefon 6,3" IPS 2280 × 1080, procesor Qualcomm Snapdragon 636 8jádrový, RAM 4 GB, interní paměť 64 GB, Micro SD až 512 GB, zadní fotoaparát 16 Mpx (f/1,8) + 8 Mpx (f/2,2) + 5 Mpx (f/2,4), přední fotoaparát 8 Mpx, GPS, Glonass, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM, neblokovaný, baterie 3500 mAh, Android 9.0 Pie', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Nokia 5.3 modrá', N'/ImgW.ashx?fd=f3&cd=NOK2713b1', 5399.00, N'Mobilní telefon 6,55" IPS 1600 × 720, procesor Qualcomm Snapdragon 665 8jádrový, RAM 4 GB, interní paměť 64 GB, Micro SDXC až 512 GB, zadní fotoaparát 13 Mpx (f/1,8) + 5 Mpx + 2 Mpx + 2 Mpx, přední fotoaparát 8 Mpx, GPS, Glonass, NFC, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM + paměťová karta, neblokovaný, baterie 4000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Nokia 5.1 Plus Black', N'/ImgW.ashx?fd=f3&cd=NOK2674axblack', 3999.00, N'Mobilní telefon 5,86" IPS 1520 × 720, procesor MediaTek MT6771 (Helio P60) 8jádrový, RAM 3 GB, interní paměť 32 GB, Micro SD až 400 GB, zadní fotoaparát 13 Mpx (f/2) + 5 Mpx (f/2,4), přední fotoaparát 8 Mpx, GPS, Glonass, LTE, Jack (3,5mm) a USB-C, čtečka otisků, dual SIM, neblokovaný, baterie 3060 mAh, Android 8.1 Oreo', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Asus ROG Phone 3 16GB/512GB černá', N'/ImgW.ashx?fd=f3&cd=AST041c1', 31990.00, N'Mobilní telefon 6,59" AMOLED 2340 × 1080, 8jádrový procesor, RAM 16 GB, interní paměť 512 GB, zadní fotoaparát 64 Mpx (f/1,8) + 13 Mpx (f/2,4) + 5 Mpx (f/2), přední fotoaparát 24 Mpx, elektronická stabilizace, GPS, Glonass, NFC, LTE, 5G, USB-C, čtečka otisků v displeji, dual SIM, neblokovaný, rychlé nabíjení 30W, reverzní nabíjení 10W, baterie 6000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Asus Zenfone 7 Pro černá', N'/ImgW.ashx?fd=f3&cd=AST043b2', 0.00, N'Mobilní telefon 6,67" AMOLED 2400 × 1080, procesor Snapdragon 865+ 8jádrový, RAM 8 GB, interní paměť 256 GB, Micro SD až 2000 GB, otočný zadní fotoaparát s optickým zoomem 64 Mpx (f/1,8) + 8 Mpx (f/2,4) + 12 Mpx (f/2,2), elektronická stabilizace, GPS, Glonass, NFC, LTE, 5G, USB-C, čtečka otisků, dual SIM + paměťová karta, neblokovaný, rychlé nabíjení 33W, baterie 5000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
EXEC [dbo].[Alza_Products_Insert] N'Asus Zenfone 7 černá', N'/ImgW.ashx?fd=f3&cd=AST042b2', 0.00, N'Mobilní telefon 6,67" AMOLED 2400 × 1080, procesor Qualcomm Snapdragon 865 8jádrový, RAM 8 GB, interní paměť 128 GB, Micro SD až 2000 GB, otočný zadní fotoaparát s optickým zoomem 64 Mpx (f/1,8) + 8 Mpx (f/2,4) + 12 Mpx (f/2,2), elektronická stabilizace, GPS, Glonass, NFC, LTE, 5G, USB-C, čtečka otisků, dual SIM + paměťová karta, neblokovaný, rychlé nabíjení 31W, baterie 5000 mAh, Android 10', @IdentityName, @IdentityIP, @ProductID OUT
GO
-- ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
-- ||
-- ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
GO
EXEC [dbo].[Alza_Products_SelectAll]
GO