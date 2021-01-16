CREATE DATABASE ElectronicShop
GO

USE ElectronicShop
GO

CREATE TABLE Advertisement
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	Image VARCHAR(500),
)
GO

CREATE TABLE Customer
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	UserName VARCHAR(100) UNIQUE,
	PassWord VARCHAR(500),
	DisplayName NVARCHAR(100),
	PhoneNumber VARCHAR(15),
	Email VARCHAR(100),
	Address NVARCHAR(500),
	Avatar VARCHAR(500), 
	IsRegister BIT,
)
GO

CREATE TABLE Category
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	DisplayName NVARCHAR(100),
	Image VARCHAR(500)
)
GO

CREATE TABLE KeyWord
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	Content NVARCHAR(500)
)
GO 

CREATE TABLE Product
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	CategoryID INT FOREIGN KEY REFERENCES Category(ID),
	KeyWordID INT FOREIGN KEY REFERENCES KeyWord(ID),
	Rating FLOAT,
	Discount INT,
	IsFreeShip BIT,
	ViewNumber INT,
	Image1 VARCHAR(500),
	Image2 VARCHAR(500),
	Image3 VARCHAR(500),
	Image4 VARCHAR(500),
	IsNew BIT,
	IsInstallment BIT,
	DisplayName NVARCHAR(100),
	Description NVARCHAR(500),
)
GO

CREATE TABLE Promotion
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	ProductID INT FOREIGN KEY REFERENCES Product(ID),
	Content NVARCHAR(500)
)
GO

CREATE TABLE ProductDetail
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	ProductID INT FOREIGN KEY REFERENCES Product(ID),
	ElementLabel NVARCHAR(100),
	ElementDescription NVARCHAR(500),
)
GO 

CREATE TABLE ProductOption
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	ProductID INT FOREIGN KEY REFERENCES Product(ID),
	DelPrice DECIMAl,
	Price DECIMAL,
	OptionContent NVARCHAR(100),
)
GO

-- Chưa INSERT
CREATE TABLE Comment
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	CustomerID INT FOREIGN KEY REFERENCES Customer(ID),
	ProductID INT FOREIGN KEY REFERENCES Product(ID),
	LikeNumber INT,
	DisLikeNumber INT,
	StarNumber INT,
	Reason NVARCHAR(100),
	Description NVARCHAR(500),
)
GO
 
CREATE TABLE BillStatus
( 
	ID INT PRIMARY KEY IDENTITY(1,1),
	Status NVARCHAR(100)
)
GO

CREATE TABLE Bill
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	CreationTime DATETIME,
	TotalBill DECIMAL,
	BillStatusID INT FOREIGN KEY REFERENCES BillStatus(ID),
	CustomerID INT FOREIGN KEY REFERENCES Customer(ID),
	IsDelete BIT DEFAULT 0
)
GO

CREATE TABLE BillInfo
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	Count INT,
	Total DECIMAL,
	ProductID INT FOREIGN KEY REFERENCES Product(ID),
	OptionProduct INT FOREIGN KEY REFERENCES ProductOption(ID),
	BillID INT FOREIGN KEY REFERENCES Bill(ID) 
)
GO 

CREATE TABLE Notification
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	CustomerID INT FOREIGN KEY REFERENCES Customer(ID),
	Title NVARCHAR(100),
	CreationTime DATETIME,
	IsDelete BIT DEFAULT 0,
	Description NVARCHAR(500),
)
GO

CREATE TABLE DiscountCode
( 
	Code VARCHAR(500) PRIMARY KEY,
	Discount INT, 
)
GO
   
CREATE TRIGGER UpdateCommentRating
ON Comment
FOR  INSERT
AS
BEGIN 
	DEClARE @Rating INT = 0,
		@ProductID INT

	SELECT @ProductID = ProductID
	FROM INSERTED

	SELECT @Rating = AVG(StarNumber)
	FROM Comment
	WHERE ProductID = @ProductID

	UPDATE Product
	SET Rating = @Rating
	WHERE ID = @ProductID
END
GO 
   
CREATE TRIGGER UpdateTotalBillInfo
ON BillInfo
FOR INSERT
AS
BEGIN 
	DEClARE @ID INT = 0,
			@ProductID INT = 0,
			@Count INT = 0,
			@OptionProduct INT = 0,
			@Price DECIMAL = 0

	SELECT	@ProductID = ProductID, 
			@Count = Count, 
			@OptionProduct = OptionProduct,
			@ID = ID
	FROM INSERTED

	SELECT @Price = Price
	FROM ProductOption
	WHERE @ProductID = ProductID AND @OptionProduct = ID
	 
	UPDATE BillInfo
	SET Total = @Price * @Count
	WHERE ID = @ID
END
GO 

-------------------------------------------------------------------------------
-- INSERT Advertisement
INSERT INTO Advertisement VALUES('img1.jpg')
INSERT INTO Advertisement VALUES('img2.jpg')
INSERT INTO Advertisement VALUES('img3.jpg')
INSERT INTO Advertisement VALUES('img4.jpg')
INSERT INTO Advertisement VALUES('img5.jpg')
INSERT INTO Advertisement VALUES('img6.jpg')
INSERT INTO Advertisement VALUES('img7.jpg')
INSERT INTO Advertisement VALUES('img8.jpg')
INSERT INTO Advertisement VALUES('img9.jpg')
INSERT INTO Advertisement VALUES('img10.jpg')
INSERT INTO Advertisement VALUES('img11.jpg')

-------------------------------------------------------------------------------

-- INSERT 3 Customer
INSERT INTO Customer VALUES('admin',	'21232f297a57a5a743894a0e4a801fc3',		N'Admin',					'0359521032',	'18524692@gm.uit.edu.vn',	N'Khu phố 6, P.Linh Trung, Q.Thủ Đức, TP.Hồ Chí Minh',		'admin.jpg'	,	0)
INSERT INTO Customer VALUES('son',		'498d3c6bfa033f6dc1be4fcc3c370aa7',		N'Huỳnh Ngọc Sơn',			'0398182219',	'18521694@gm.uit.edu.vn',	N'Khu phố 6, P.Linh Trung, Q.Thủ Đức, TP.Hồ Chí Minh',		'son.jpg'	,	0)
INSERT INTO Customer VALUES('phuong',	'60c9312821b0d1eb614810a21f159147',		N'Nguyễn Trần Thị Phương',	'0356521091',	'18526464@gm.uit.edu.vn',	N'Khu phố 6, P.Linh Trung, Q.Thủ Đức, TP.Hồ Chí Minh',		'phuong.jpg',	0)
INSERT INTO Customer VALUES('liem',		'803e188b6c58a7f2178a2e56b989598f',		N'Đặng Ngọc Liêm',			'0356521091',	'18520981@gm.uit.edu.vn',	N'Khu phố 6, P.Linh Trung, Q.Thủ Đức, TP.Hồ Chí Minh',		'admin.jpg'	,	0)

-------------------------------------------------------------------------------

-- INSERT 5 Category
INSERT INTO Category VALUES(N'Điện thoại',				'cellphone.png')
INSERT INTO Category VALUES(N'Máy tính bảng',			'tablet.png')
INSERT INTO Category VALUES(N'Laptop',					'laptop.png')
INSERT INTO Category VALUES(N'Đồng hồ thông minh',		'smartwatch.png')
INSERT INTO Category VALUES(N'Phụ kiện',				'headphone.png')
GO

-------------------------------------------------------------------------------

-- INSERT 5 KEYWORD
INSERT INTO KeyWord VALUES(N'dien thoai di dong - điện thoại di động - dien thoai thong minh - điện thoại thông minh - cellphone - smartphone')
INSERT INTO KeyWord VALUES(N'may tinh bang - máy tình bảng - tablet')
INSERT INTO KeyWord VALUES(N'may tinh - máy tính - may tinh de ban - máy tính để bàn - may tinh xach tay - máy tính xách tay - pc - laptop')
INSERT INTO KeyWord VALUES(N'dong ho thong minh - đồng hồ thông minh - dong ho dien tu - đồng hồ điện tử - smartwatch')
INSERT INTO KeyWord VALUES(N'phu kien - phụ kiện - sac du phong - sạc dự phòng - cap sac - cáp sạc - tai nghe - loa - op lung - ốp lưng')
GO

-------------------------------------------------------------------------------INSERT Produt 
-- Cellphone(1)				   ĐT keyword  Rating Discount  isFree  View
-- Product 1
INSERT INTO Product VALUES(1,	1,	0,	0,	0,	10,	'img1.jpg',		'img2.jpg',		'img3.jpg',		'img4.jpg',		1,	1,	N'Điện thoại iPhone 12 mini 64GB',						N'Điểm nhấn đầu tiên phải kể đến ở dòng máy này chính là viền máy không còn được thiết kế bo cong các cạnh, mà thay vào đó là phần cạnh máy được vát phẳng vô cùng mạnh mẽ và cá tính.')
-- Prodcut 2																								
INSERT INTO Product VALUES(1,	1,	0,	5,	1,	20,	'img5.jpg',		'img6.jpg',		'img7.jpg',		'img8.jpg',		1,	1,	N'Điện thoại iPhone 11 Pro Max 512GB',					N'Để tìm kiếm một chiếc smartphone có hiệu năng mạnh mẽ và có thể sử dụng mượt mà trong 2-3 năm tới thì không có chiếc máy nào xứng đang hơn chiếc iPhone 11 Pro Max 512GB mới ra mắt trong năm 2019 của Apple.')
-- Product 3																								
INSERT INTO Product VALUES(1,	1,	0,	10,	1,	30,	'img9.jpg',		'img10.jpg',	'img11.jpg',	'img12.jpg',	1,	1,	N'Điện thoại Samsung Galaxy Note 20 Ultra 5G Trắng',	N'Samsung Galaxy Note 20 Ultra 5G Trắng mẫu flagship hàng đầu của Samsung hoàn hảo về mọi mặt, từ thiết kế sang trọng đẳng cấp cho đến hiệu năng cực khủng ẩn chứa bên trong. Đặc biệt hơn cả là chiếc bút S Pen đầy “quyền năng” của dòng Note giờ đây cũng đã được nâng lên một tầm cao mới.')
-- Product 4																										
INSERT INTO Product VALUES(1,	1,	0,	5,	0,	40,	'img13.jpg',	'img14.jpg',	'img15.jpg',	'img16.jpg',	0,	1,	N'Điện thoại Samsung Galaxy S20 FE',					N'Camera trên S20 FE là 3 cảm biến chất lượng nằm gọn trong mô đun chữ nhật độc đáo ở mặt lưng bao gồm: 1 cảm biến chính 12 MP cho chất lượng ảnh sắc nét, 1 camera góc siêu rộng 12 MP cung cấp góc chụp tối đa và cuối cùng 1 camera tele 8 MP hỗ trợ zoom quang 3X.')
-- Product 5																									
INSERT INTO Product VALUES(1,	1,	0,	15,	0,	50,	'img17.jpg',	'img18.jpg',	'img19.jpg',	'img20.jpg',	0,	0,	N'Điện thoại Samsung Galaxy Z Fold2 5G',				N'Samsung Galaxy Z Fold 2 vẫn giữ nguyên cơ chế màn hình gập ở thế hệ tiền nhiệm. Như một quyển sách, chiếc điện thoại mở ra để hiển thị màn hình lớn bên trong. Bên ngoài là một màn hình phụ tràn viền với thiết kế đục lỗ.')
-- Product 6																									
INSERT INTO Product VALUES(1,	1,	0,	0,	1,	60,	'img21.jpg',	'img22.jpg',	'img23.jpg',	'img24.jpg',	0,	0,	N'Điện thoại OPPO Reno4',								N'OPPO Reno4 - Chiếc điện thoại có thiết kế thời thượng, hiệu năng mạnh mẽ cùng bộ 4 camera siêu ấn tượng, tối ưu hóa cho chụp ảnh và quay phim siêu sắc nét, hứa hẹn sẽ là sản phẩm đáng để nâng cấp của hãng OPPO trong năm nay.')
GO

-- Tablet(2)
-- Product 7
INSERT INTO Product VALUES(2,	2,	0,	5,	1,	20,	'img1.jpg',		'img2.jpg',		'img3.jpg',		'img4.jpg',		1,	1,	N'iPad Pro 12.9 inch Wifi Cellular 128GB (2020)',		N'Sau bao ngày chờ đợi, chiếc máy tính bảng iPad Pro 12.9 inch Wifi Cellular 128GB (2020) đã được trình làng. Với thiết kế không mấy khác biệt so với người anh em iPad Pro 2018 nhưng được Apple nâng cấp hệ thống camera, cùng con chip A12Z giúp iPad Pro 12.9 (2020) mang đến hiệu năng ấn tượng cho người dùng những trải nghiệm tuyệt vời.')
-- Product 8
INSERT INTO Product VALUES(2,	2,	0,	5,	1,	30,	'img5.jpg',		'img6.jpg',		'img7.jpg',		'img8.jpg',		0,	1,	N'iPad 10.2 inch Wifi 32GB (2019)',						N'Thiết kế sang trọng, màn hình đẹp và một cấu hình đủ dùng cho hầu hết nhu cầu là những ưu điểm mà chiếc máy tính bảng iPad 10.2 inch Wifi 32GB (2019) này sở hữu.')
-- Product 9
INSERT INTO Product VALUES(2,	2,	0,	10,	0,	40,	'img9.jpg',		'img10.jpg',	'img11.jpg',	'img12.jpg',	1,	1,	N'Máy tính bảng Samsung Galaxy Tab S6',					N'Ấn tượng đầu tiên về chiếc máy tính bảng Samsung này chính là nó rất mỏng và sang trọng, nếu bạn đang có suy nghĩ là những chiếc máy tính bảng thường có kích thước lớn, trọng lượng nặng và khó mang theo thì đây sẽ là một trải nghiệm hoàn toàn khác.')
-- Product 10
INSERT INTO Product VALUES(2,	2,	0,	0,	1,	50,	'img13.jpg',	'img14.jpg',	'img15.jpg',	'img16.jpg',	0,	0,	N'Máy tính bảng iPad 10.2 inch Wifi 128GB (2019',		N'Muốn mua một chiếc máy tính bảng với hiệu năng ổn định, có thể dùng trong khoảng 2-3 năm tới với mức giá phải chăng thì không đi đâu xa, chiếc iPad 10.2 inch Wifi 128GB (2019) chính là sự lựa chọn dành cho bạn.')
GO
																													 
-- Laptop(3)
-- Product 11																										 
INSERT INTO Product VALUES(3,	3,	0,	0,	1,	10,	'img1.jpg',		'img2.jpg',		'img3.jpg',		'img4.jpg',		1,	1,	N'Asus Gaming Rog Strix G512 i7',						N'Laptop Asus Gaming Rog Strix G512 i7 (IAL001T) là chiếc laptop gaming có cấu hình mạnh mẽ và thiết kế hầm hố đậm chất gaming. Máy được trang bị chip Intel Core i7 thế hệ mới nhất cùng với màn hình cao cấp chuẩn gaming giúp bạn thỏa sức chiến những tựa game cực đỉnh.')
-- Product 12																										  
INSERT INTO Product VALUES(3,	3,	0,	5,	1,	20,	'img5.jpg',		'img6.jpg',		'img7.jpg',		'img8.jpg',		1,	1,	N'Asus Gaming Rog Strix G512 i5',						N'Laptop Asus Gaming ROG Strix G512 i5 (IAL013T) mang đến ngôn ngữ thiết kế hiện đại, cấu hình mạnh mẽ với vi xử lí gen 10 mới, card đồ họa rời GeForce GTX 1650Ti, chiến những tựa game nặng kí nhất.')
-- Product 13																										 
INSERT INTO Product VALUES(3,	3,	0,	5,	0,	30,	'img9.jpg',		'img10.jpg',	'img11.jpg',	'img12.jpg',	0,	0,	N'Asus VivoBook X509JA i7',								N'Laptop ASUS VivoBook X509JA i7 (EJ232TS) với ngoại hình hiện đại, sang trọng, cấu hình mạnh mẽ giúp bạn làm việc, giải trí thoải mái. Laptop còn trang bị bảo mật vân tay hiện đại, màn hình tràn viền để bạn thả ga sáng tạo.')
-- Product 14																										 
INSERT INTO Product VALUES(3,	3,	0,	0,	1,	40,	'img13.jpg',	'img14.jpg',	'img15.jpg',	'img16.jpg',	1,	1,	N'Asus VivoBook A512FA i5',								N'Laptop Asus Vivobook A512FA được thiết kế mỏng nhẹ, thanh lịch phù hợp cho sinh viên - nhân viên văn phòng khi đi làm và đi học. Máy được trang bị dòng chip thế hệ thứ 10 mới nhất hiện nay để chạy tốt ứng dụng văn phòng và thiết kế cơ bản.')
-- Product 15
INSERT INTO Product VALUES(3,	3,	0,	0,	1,	50,	'img17.jpg',	'img18.jpg',	'img19.jpg',	'img20.jpg',	1,	1,	N'HP Zbook Firefly 14 G7 i7',							N'Laptop HP Zbook Firefly 14 G7 i7 (8VK71AV) phiên bản i7 được mệnh danh là chiếc máy trạm nhỏ gọn nhất thế giới. Đây là chiếc máy có hiệu năng cao với chip Intel thế hệ 10 kết hợp cùng card đồ họa rời NVIDIA Quadro cho người dùng chuyên nghiệp, phù hợp với người dùng là doanh nhân, người làm đồ họa,...')
-- Product 16																										 
INSERT INTO Product VALUES(3,	3,	0,	10,	1,	20,	'img21.jpg',	'img22.jpg',	'img23.jpg',	'img24.jpg',	0,	1,	N'HP Envy 13 aq1057TX i7',								N'Laptop HP Envy 13 aq1057TX i7 (8XS68PA) là chiếc laptop thuộc dòng HP Envy 13 vừa được ra mắt năm 2019 có cấu hình mạnh mẽ với chip Intel Core i7 thế hệ mới cùng card đồ họa rời. Máy có thiết kế siêu mỏng nhẹ phù hợp với người thường xuyên di chuyển.')
-- Product 17																									 
INSERT INTO Product VALUES(3,	3,	0,	5,	0,	30,	'img25.jpg',	'img26.jpg',	'img27.jpg',	'img28.jpg',	1,	0,	N'HP Envy 13 aq1047TU i7',								N'Laptop HP Envy 13 aq1047TU i7 (8XS69PA) toàn diện từ cấu hình chip Intel Core i7 cực mạnh mẽ cho đến thiết kế độc đáo, thanh lịch với hoạ tiết vân gỗ bắt mắt. Đây sẽ là chiếc là chiếc máy tính xách tay thuộc phân khúc cao cấp đáp ứng được mọi yêu cầu thao tác của người dùng.')
-- Product 18																										 
INSERT INTO Product VALUES(3,	3,	0,	0,	1,	40,	'img29.jpg',	'img30.jpg',	'img31.jpg',	'img32.jpg',	0,	1,	N'HP Envy 13 ba0045TU i5',								N'HP Envy 13 ba0045TU i5 (171M2PA) được thiết kế sang trọng, chắc chắn. Đồng thời máy được trang bị dòng chip thế hệ thứ 10 mới nhất hiện nay sẽ đáp ứng tốt hầu hết các ứng dụng văn phòng hay cái tựa game phổ thông bây giờ.')
-- Product 19																										 
INSERT INTO Product VALUES(3,	3,	0,	15,	0,	20,	'img33.jpg',	'img34.jpg',	'img35.jpg',	'img36.jpg',	1,	1,	N'Lenovo Yoga C940 14IIL i7',							N'Laptop Lenovo Yoga C940 14IIL i7 (81Q9007KVN) là phiên bản laptop doanh nhân cao cấp sang trọng. Máy có cấu hình khỏe với chip Core i7 thế hệ 10, ổ cứng SSD 1024 GB cực ấn tượng cùng một thiết kế siêu mỏng nhẹ tiện dụng.')
-- Product 20																										 
INSERT INTO Product VALUES(3,	3,	0,	10,	1,	40,	'img37.jpg',	'img38.jpg',	'img39.jpg',	'img40.jpg',	0,	0,	N'Lenovo Yoga Duet 7 13IML05 i5',						N'Cực kỳ linh hoạt và mạnh mẽ với cấu hình thế hệ 10 mới nhất, màn hình cảm ứng cùng thiết bị mở khoá khuôn mặt thông minh, laptop Lenovo Yoga Duet 7 13IML05 i5 (82AS007BVN) quả là một tuyệt tác của giới công nghệ 2020.')
GO
																													 	
-- SmartWatc4(4)
-- Product 21																										 
INSERT INTO Product VALUES(4,	4,	0,	5,	1,	50,	'img1.jpg',		'img2.jpg',		'img3.jpg',		'img4.jpg',		0,	1,	N'Apple Watch S6 LTE 44mm',								N'Chiếc Apple Watch S6 LTE sở hữu dây đeo sáng bóng, chống gỉ sét tốt, màn hình tràn viền mang lại sự trải nghiệm sắc nét, rõ ràng. Phần thân máy có kết cấu chắc chắn, kính cường lực chống trầy xước tốt, không ngại những va chạm thông thường. Mặt đồng hồ với kích thước 1.78 inch giúp hiển thị thông tin rõ ràng hơn, đem lại cho bạn sự hài lòng khi đeo mẫu đồng hồ phiên bản 2020 này trên tay.')
-- Product 22			   																							   
INSERT INTO Product VALUES(4,	4,	0,	0,	0,	30,	'img5.jpg',		'img6.jpg',		'img7.jpg',		'img8.jpg',		1,	1,	N'Garmin Fenix 6X Saphire dây silicone',				N'Đồng hồ thông minh Garmin Fenix 6X Saphire trang bị màn hình tròn kích thước 1.3 inch và độ phân giải 280 x 280 pixels giúp hình ảnh hiển thị sắc nét. Dây đeo làm từ vật liệu silicone mang lại cảm giác thoải mái, không bị đau cổ tay khi đeo lâu. mẫu đồng hồ này sử dụng 5 phím cơ để điều khiển và không có thao tác cảm ứng.')
-- Product 23			   																							 
INSERT INTO Product VALUES(4,	4,	0,	10,	0,	40,	'img9.jpg',		'img10.jpg',	'img11.jpg',	'img12.jpg',	1,	1,	N'Suunto 9 Baro titanium dây silicone',					N'Đồng hồ thông minh Suunto 9 Baro sở hữu màn hình tròn Active Matrix LCD 1.97 inch, độ phân giải 320 x 300 pixels cho hình ảnh hiển thị sắc nét. Cùng với mặt kính Sapphire cứng hạn chế trầy xước cũng như giúp chiếc smartwatch của bạn được bền bỉ hơn.')
-- Product 24			   																							  
INSERT INTO Product VALUES(4,	4,	0,	0,	1,	20,	'img13.jpg',	'img14.jpg',	'img15.jpg',	'img16.jpg',	0,	0,	N'Apple Watch S5 44mm',									N'Apple Watch S5 44 mm là phiên bản nâng cấp nhẹ so với phiên bản Apple Watch S4 tiền nhiệm. Điểm ấn tượng của sản phẩm mới lần này là màn hình OLED luôn hiển thị, tính năng la bàn và khả năng cảnh báo khẩn cấp trên nhiều quốc gia hơn.')
GO
																													 	
-- Accessories(5)
-- Product 25																									 
INSERT INTO Product VALUES(5,	5,	0,	5,	1,	20,	'img1.jpg',		'img2.jpg',		'img3.jpg',		'img4.jpg',		1,	1,	N'Pin sạc dự phòng Polymer 20.000mAh',					N'Kích thước gọn gàng, trọng lượng nhẹ 532 gam giúp bạn dễ dàng mang theo trong túi xách, túi quần của bạn khi di chuyển. Thiết kế bằng nhựa đen nhám cố các cạnh được bo tròn cho cảm giác êm ái, thoải mái khi cầm trên tay và không gây bám mồ hôi.')
-- Product 26			   																							 					 
INSERT INTO Product VALUES(5,	5,	0,	0,	0,	30,	'img5.jpg',		'img6.jpg',		'img7.jpg',		'img8.jpg',		0,	0,	N'Polymer 20.000mAh Type C PowerCore Essential',		N'Pin sạc dự phòng PowerCore Essential 20000mAh PD với kích thước mỏng gọn, trọng lượng nhẹ, dễ dàng mang theo')
-- Product 27			   																							  					 
INSERT INTO Product VALUES(5,	5,	0,	10,	0,	40,	'img9.jpg',		'img10.jpg',	'img11.jpg',	'img12.jpg',	1,	1,	N'Polymer 10.000mAh Type C PD QC3.0 Energizer',			N'Pin sạc dự phòng không dây Polymer 10.000mAh Type C PD QC3.0 Energizer QE10007PQ xám có thiết kế sang trọng, mạnh mẽ và nhỏ gọn, dễ dàng mang theo đến nhiều nơi.')
-- Product 28			   																							  					
INSERT INTO Product VALUES(5,	5,	0,	15,	1,	50,	'img13.jpg',	'img14.jpg',	'img15.jpg',	'img16.jpg',	1,	1,	N'Adapter chuyển đổi USB C',							N'Cho phép adapter kết nối đa năng với nhiều thiết bị cùng lúc, dễ dàng sử dụng để kết nối, truyền tải, trao đổi dữ liệu từ Macbook gồm các dòng Macbook Pro 12/13/15 inch 2016 - 2017, Macbook Air 13 inch 2018, iPad Pro 2018, Surface Pro X, Surface Go, Surface Pro 7, laptop Surface 3 13/15 inch.')
-- Product 29																										 					  
INSERT INTO Product VALUES(5,	5,	0,	0,	1,	20,	'img17.jpg',	'img18.jpg',	'img19.jpg',	'img20.jpg',	0,	1,	N'Adapter sạc 4 cổng USB Type C',						N'Adapter sạc 4 cổng USB Type C PD 100W Anker PowerPort Atom A2041 Trắng với thiết kế nhỏ nhắn, tinh tế, gọn gàng dễ mang theo.')
-- Product 30																									 					  
INSERT INTO Product VALUES(5,	5,	0,	10,	1,	10,	'img21.jpg',	'img22.jpg',	'img23.jpg',	'img24.jpg',	1,	1,	N'Tai nghe chụp tai Bluetooth Sony',					N'Tai nghe chụp tai Bluetooth Sony WH-1000XM4/BME Đen có thiết kế đơn giản, kích thước nhỏ gọn, màu sắc hài hòa đáp ứng nhu cầu giải trí bằng âm nhạc ở mọi địa điểm. Tai nghe có cấu trúc gấp thông minh bạn có thể dễ dàng gấp bỏ vào balo mang theo muôn nơi.')
-- Product 31																									 	 					 
INSERT INTO Product VALUES(5,	5,	0,	5,	0,	20,	'img25.jpg',	'img26.jpg',	'img27.jpg',	'img28.jpg',	0,	0,	N'Tai nghe chụp tai Beats Studio3',						N'Tai nghe chụp tai Beats Studio3 Wireless MX422/ MX432 mặt ngoài bằng nhựa cao cấp bền tốt, mới lâu, đệm tai dày và êm ái, chuẩn tốt cho nhu cầu sử dụng lâu như dùng trong phòng thu…')
-- Product 32																										 					  
INSERT INTO Product VALUES(5,	5,	0,	20,	1,	30,	'img29.jpg',	'img30.jpg',	'img31.jpg',	'img32.jpg',	1,	1,	N'Tai nghe AirPods Pro',								N'AirPods Pro với thiết kế gọn gàng, đẹp và tinh tế,  tai nghe được thiết kế theo dạng In-ear thay vì Earbuds như AirPods 2, cho phép chống ồn tốt hơn, khó rớt khi đeo và êm tai hơn.')
-- Product 33																										 					 
INSERT INTO Product VALUES(5,	5,	0,	15,	0,	30,	'img33.jpg',	'img34.jpg',	'img35.jpg',	'img36.jpg',	1,	0,	N'Tai nghe AirPods 2',									N'Tai nghe sở hữu thiết kế thời trang và nhỏ gọn, trọng lượng cũng rất nhẹ không khác mấy so với tai nghe Bluetooth AirPods thế hệ đầu tiên. Tai nghe còn được phủ lên một lớp chất liệu mới ở đầu mỗi tai nghe để giúp tai nghe bám trên tai người đeo hơn, tránh bị rơi rớt hơn.')
-- Product 34																										  
INSERT INTO Product VALUES(5,	5,	0,	10,	1,	30,	'img37.jpg',	'img38.jpg',	'img39.jpg',	'img40.jpg',	0,	1,	N'Tai nghe EP Bluetooth Sony',							N'Chất liệu silicone có thể gấp gọn tai nghe Sony cho vào túi xách tiện mang theo khi ra ngoài, cất giữ. 2 đầu tai nghe có nam châm, khi không nghe nhạc, bạn có thể chập 2 củ tai lại với nhau, giữ tai nghe ở vùng cổ, không sợ rơi rớt, chống rối dây hiệu quả.')
GO
  																												 	
-------------------------------------------------------------------------------INSERT Promotion
 																													 
-- For Product 1 
INSERT INTO Promotion VALUES(1,	N'Phụ kiện Apple mua kèm giảm đến 30% (không áp dụng đồng thời khuyến mãi khác).')
INSERT INTO Promotion VALUES(1,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(1,	N'Phiếu Mua Máy Lọc Nước Kangaroo, Karofi, Sunhouse, KoriHome Trị Giá 300,000đ (EV).')
INSERT INTO Promotion VALUES(1,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 2		(ProductID, Content)
INSERT INTO Promotion VALUES(2,	N'Phụ kiện Apple mua kèm giảm đến 20% (không áp dụng đồng thời khuyến mãi khác).')
INSERT INTO Promotion VALUES(2,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(2,	N'Phiếu Mua Máy Lọc Nước Kangaroo, Karofi, Sunhouse, KoriHome Trị Giá 300,000đ (EV).')
INSERT INTO Promotion VALUES(2,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 3
INSERT INTO Promotion VALUES(3,	N'Mua online thêm quà: Giảm giá 1,000,000đ (Không áp dụng kèm Thu cũ đổi mới).')
INSERT INTO Promotion VALUES(3,	N'Giảm giá 5,000,000đ khi tham gia thu cũ đổi mới Note 20 Series.')
INSERT INTO Promotion VALUES(3,	N'Ưu đãi phòng chờ thương gia.')
INSERT INTO Promotion VALUES(3,	N'Trả góp 0% thẻ tín dụng.')
INSERT INTO Promotion VALUES(3,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(3,	N'Mua Đồng hồ Samsung Active 2 giảm 35% (riêng Active 2 nhôm 40mm giảm 900.000đ), Watch 3 giảm 25% (không kèm KM khác).')
INSERT INTO Promotion VALUES(3,	N'Phiếu Mua Máy Lọc Nước Kangaroo, Karofi, Sunhouse, KoriHome Trị Giá 300,000đ (EV).')
INSERT INTO Promotion VALUES(3,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 4
INSERT INTO Promotion VALUES(4,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(4,	N'Mua Đồng hồ Samsung Active 2 giảm 35% (riêng Active 2 nhôm 40mm giảm 900.000đ), Watch 3 giảm 25% (không kèm KM khác).')
INSERT INTO Promotion VALUES(4,	N'Phiếu Mua Máy Lọc Nước Kangaroo, Karofi, Sunhouse, KoriHome Trị Giá 300,000đ (EV).')
GO

-- For Product 5
INSERT INTO Promotion VALUES(5,	N'Gói bảo hành mở rộng Samsung Care+.')
INSERT INTO Promotion VALUES(5,	N'Ưu đãi phòng chờ thương gia.')
INSERT INTO Promotion VALUES(5,	N'Trả góp 0% thẻ tín dụng.')
INSERT INTO Promotion VALUES(5,	N'Hỗ trợ kỹ thuật tận nhà tại 5 Tỉnh/Thành')
INSERT INTO Promotion VALUES(5,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(5,	N'Mua Đồng hồ Samsung Active 2 giảm 35% (riêng Active 2 nhôm 40mm giảm 900.000đ), Watch 3 giảm 25% (không kèm KM khác).')
INSERT INTO Promotion VALUES(5,	N'Phiếu Mua Máy Lọc Nước Kangaroo, Karofi, Sunhouse, KoriHome Trị Giá 300,000đ (EV).')
GO

-- For Product 6
INSERT INTO Promotion VALUES(6,	N'Giảm giá 700,000đ khi tham gia thu cũ đổi mới OPPO Reno 4.')
INSERT INTO Promotion VALUES(6,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(6,	N'Phiếu Mua Máy Lọc Nước Kangaroo, Karofi, Sunhouse, KoriHome Trị Giá 300,000đ (EV).')
INSERT INTO Promotion VALUES(6,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).') 
GO

-- For Product 7
INSERT INTO Promotion VALUES(7,	N'Phụ kiện Apple mua kèm giảm đến 20% (không áp dụng đồng thời khuyến mãi khác).')
INSERT INTO Promotion VALUES(7,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
GO

-- For Product 8
INSERT INTO Promotion VALUES(8,	N'Phụ kiện Apple mua kèm giảm đến 20% (không áp dụng đồng thời khuyến mãi khác).')
INSERT INTO Promotion VALUES(8,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
GO

-- For Product 9
INSERT INTO Promotion VALUES(9,	N'Phụ kiện Apple mua kèm giảm đến 20% (không áp dụng đồng thời khuyến mãi khác).')
INSERT INTO Promotion VALUES(9,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
GO

-- For Product 10
INSERT INTO Promotion VALUES(10,	N'Phụ kiện Apple mua kèm giảm đến 20% (không áp dụng đồng thời khuyến mãi khác)')
INSERT INTO Promotion VALUES(10,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
GO

-- For Product 11
INSERT INTO Promotion VALUES(11,	N'Tai nghe chụp tai Gaming MozardX DS902 7.1.')
INSERT INTO Promotion VALUES(11,	N'Chuột Gaming Zadez G-152M.')
INSERT INTO Promotion VALUES(11,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(11,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(11,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 12
INSERT INTO Promotion VALUES(12,	N'Tai nghe chụp tai Gaming MozardX DS902 7.1.')
INSERT INTO Promotion VALUES(12,	N'Chuột Gaming Zadez G-152M.')
INSERT INTO Promotion VALUES(12,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(12,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(12,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 13
INSERT INTO Promotion VALUES(13,	N'Túi chống sốc Laptop 15.6" eValu.')
INSERT INTO Promotion VALUES(13,	N'Chuột không dây.')
INSERT INTO Promotion VALUES(13,	N'Tai nghe chụp tai Kanen IP-350.')
INSERT INTO Promotion VALUES(13,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(13,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(13,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 14
INSERT INTO Promotion VALUES(14,	N'Chuột không dây.')
INSERT INTO Promotion VALUES(14,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(14,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(14,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).') 
GO

-- For Product 15
INSERT INTO Promotion VALUES(15,	N'Chuột không dây.')
INSERT INTO Promotion VALUES(15,	N'Tai nghe chụp tai Kanen IP-350.')
INSERT INTO Promotion VALUES(15,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(15,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(15,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 16
INSERT INTO Promotion VALUES(16,	N'Chuột không dây.')
INSERT INTO Promotion VALUES(16,	N'Tai nghe chụp tai Kanen IP-350.')
INSERT INTO Promotion VALUES(16,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(16,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(16,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 17
INSERT INTO Promotion VALUES(17,	N'Chuột không dây.')
INSERT INTO Promotion VALUES(17,	N'Tai nghe chụp tai Kanen IP-350.')
INSERT INTO Promotion VALUES(17,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(17,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(17,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 18
INSERT INTO Promotion VALUES(18,	N'Chuột không dây.')
INSERT INTO Promotion VALUES(18,	N'Tai nghe chụp tai Kanen IP-350.')
INSERT INTO Promotion VALUES(18,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(18,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(18,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 19
INSERT INTO Promotion VALUES(19,	N'Chuột không dây.')
INSERT INTO Promotion VALUES(19,	N'Balo Laptop Lenovo.')
INSERT INTO Promotion VALUES(19,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(19,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(19,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 20
INSERT INTO Promotion VALUES(20,	N'Túi chống sốc Laptop 14" eValu.')
INSERT INTO Promotion VALUES(20,	N'Chuột không dây.')
INSERT INTO Promotion VALUES(20,	N'Mua kèm Microsoft 365 Personal 1 năm chỉ còn 690,000đ.')
INSERT INTO Promotion VALUES(20,	N'Pin sạc dự phòng giảm 30% khi mua kèm.')
INSERT INTO Promotion VALUES(20,	N'Mua Đồng hồ thời trang giảm 40% (không kèm KM khác).')
GO

-- For Product 21
INSERT INTO Promotion VALUES(21,	N'Bộ sản phẩm gồm: Hộp, Đồng hồ, Dây đeo dự phòng, Sách hướng dẫn, Dây đế sạc Apple.')
INSERT INTO Promotion VALUES(21,	N'Bảo hành chính hãng 12 tháng.')
INSERT INTO Promotion VALUES(21,	N'1 đổi 1 trong 1 tháng với sản phẩm lỗi.') 
GO

-- For Product 22
INSERT INTO Promotion VALUES(22,	N'Bảo hành chính hãng 12 tháng.')
INSERT INTO Promotion VALUES(22,	N'1 đổi 1 trong 1 tháng với sản phẩm lỗi.') 
GO

-- For Product 23
INSERT INTO Promotion VALUES(23,	N'Bộ sản phẩm gồm: Hộp, Sạc, Sách hướng dẫn.')
INSERT INTO Promotion VALUES(23,	N'Bảo hành chính hãng 24 tháng.') 
INSERT INTO Promotion VALUES(23,	N'1 đổi 1 trong 1 tháng với sản phẩm lỗi.') 
GO

-- For Product 24
INSERT INTO Promotion VALUES(24,	N'Bộ sản phẩm gồm: Hộp, Đồng hồ, Sách hướng dẫn, Adapter Apple, Dây đế sạc Apple.')
INSERT INTO Promotion VALUES(24,	N'Bảo hành chính hãng 24 tháng.') 
INSERT INTO Promotion VALUES(24,	N'1 đổi 1 trong 1 tháng với sản phẩm lỗi.') 
GO

-- For Product 25
INSERT INTO Promotion VALUES(25,	N'Lỗi là đổi mới trong 12 tháng tại 2288 siêu thị toàn quốc.')
INSERT INTO Promotion VALUES(25,	N'Trong hộp có: Pin sạc, Dây cáp Type C.') 
INSERT INTO Promotion VALUES(25,	N'TP.Hồ Chí Minh (trừ Nhà Bè, Củ Chi, Cần Giờ): Giao hàng nhanh chóng.') 
INSERT INTO Promotion VALUES(25,	N'Tỉnh Thành khác: Giao hàng từ 2 - 10 ngày.') 
INSERT INTO Promotion VALUES(25,	N'Bạn có thể trải nghiệm sản phẩm tại 12 siêu thị.') 
GO

-- For Product 26
INSERT INTO Promotion VALUES(26,	N'Bảo hành chính hãng 18 tháng.')
INSERT INTO Promotion VALUES(26,	N'Lỗi là đổi mới trong 12 tháng tại 2288 siêu thị toàn quốc.') 
INSERT INTO Promotion VALUES(26,	N'Trong hộp có: Pin sạc, Dây cáp Type C, Túi đựng pin sạc, Sách hướng dẫn.')  
GO

-- For Product 27
INSERT INTO Promotion VALUES(27,	N'Lỗi là đổi mới trong 12 tháng tại 2288 siêu thị toàn quốc.')
INSERT INTO Promotion VALUES(27,	N'Trong hộp có: Pin sạc, Dây cáp Type C, Sách hướng dẫn.')   
GO

-- For Product 28
INSERT INTO Promotion VALUES(28,	N'Lỗi là đổi mới trong 12 tháng tại 2292 siêu thị toàn quốc.')
INSERT INTO Promotion VALUES(28,	N'Tặng 100.000₫ mua online tại web thành viên BachhoaXANH.com, áp dụng khi mua Online tại Tp.HCM và 1 số khu vực.')   
INSERT INTO Promotion VALUES(28,	N'Trong hộp có: Adapter chuyển')
GO

-- For Product 29
INSERT INTO Promotion VALUES(29,	N'Lỗi là đổi mới trong 12 tháng tại 2292 siêu thị toàn quốc.')
INSERT INTO Promotion VALUES(29,	N'Tặng 100.000₫ mua online tại web thành viên BachhoaXANH.com, áp dụng khi mua Online tại Tp.HCM và 1 số khu vực.')   
INSERT INTO Promotion VALUES(29,	N'TP.Hồ Chí Minh (trừ Nhà Bè, Củ Chi, Cần Giờ): Giao hàng nhanh chóng.')
GO

-- For Product 30
INSERT INTO Promotion VALUES(30,	N'Trong hộp có: Tai nghe, Cáp 3.5mm, Dây cáp Type-C, Sách hướng dẫn, Bộ chuyển đổi đầu cắm để dùng trên máy bay, Hộp đựng')
INSERT INTO Promotion VALUES(30,	N'Lỗi là đổi mới trong 12 tháng tại 2292 siêu thị toàn quốc.')   
INSERT INTO Promotion VALUES(30,	N'Tặng 100.000₫ mua online tại web thành viên BachhoaXANH.com, áp dụng khi mua Online tại Tp.HCM và 1 số khu vực.')
GO

-- For Product 31
INSERT INTO Promotion VALUES(31,	N'Trong hộp có: Tai nghe, Cáp 3.5mm, Dây cáp Micro USB, Sách hướng dẫn, Hộp đựng')
INSERT INTO Promotion VALUES(31,	N'Bảo hành chính hãng 12 tháng')   
INSERT INTO Promotion VALUES(31,	N'Tặng 100.000₫ mua online tại web thành viên BachhoaXANH.com, áp dụng khi mua Online tại Tp.HCM và 1 số khu vực.')
GO

-- For Product 32
INSERT INTO Promotion VALUES(32,	N'Bảo hành chính hãng 12 tháng')
INSERT INTO Promotion VALUES(32,	N'Trong hộp có: Tai nghe, Sách hướng dẫn, Hộp sạc, Cáp Type C - Lightning')    
GO

-- For Product 33
INSERT INTO Promotion VALUES(33,	N'Bảo hành chính hãng 12 tháng')
INSERT INTO Promotion VALUES(33,	N'Trong hộp có: Tai nghe, Sách hướng dẫn, Hộp sạc, Dây cáp Lightning')    
GO

-- For Product 34
INSERT INTO Promotion VALUES(34,	N'Bảo hành chính hãng 12 tháng')
INSERT INTO Promotion VALUES(34,	N'Trong hộp có: Tai nghe, 3 cặp đệm tai nghe')    
GO


-------------------------------------------------------------------------------INSERT ProductDetail	 
-- For Product 1
INSERT INTO ProductDetail VALUES(1,		N'Màn hình',			N'OLED, 5.4", Super Retina XDR')
INSERT INTO ProductDetail VALUES(1,		N'Hệ điều hành',		N'iOS 14')
INSERT INTO ProductDetail VALUES(1,		N'Camera sau',			N'2 camera 12 MP')
INSERT INTO ProductDetail VALUES(1,		N'Camera trước',		N'12 MP')
INSERT INTO ProductDetail VALUES(1,		N'CPU',					N'Apple A14 Bionic 6 nhân')
INSERT INTO ProductDetail VALUES(1,		N'RAM',					N'4 GB')
INSERT INTO ProductDetail VALUES(1,		N'Bộ nhớ trong',		N'64 GB')
INSERT INTO ProductDetail VALUES(1,		N'Thẻ SIM',				N'1 Nano SIM & 1 eSIM, Hỗ trợ 5G')
INSERT INTO ProductDetail VALUES(1,		N'Dung lượng pin',		N'2227 mAh, có sạc nhanh')
GO

-- For Product 2
INSERT INTO ProductDetail VALUES(2,		N'Màn hình',			N'OLED, 6.5", Super Retina XDR')
INSERT INTO ProductDetail VALUES(2,		N'Hệ điều hành',		N'	iOS 13')
INSERT INTO ProductDetail VALUES(2,		N'Camera sau',			N'3 camera 12 MP')
INSERT INTO ProductDetail VALUES(2,		N'Camera trước',		N'12 MP')
INSERT INTO ProductDetail VALUES(2,		N'CPU',					N'Apple A13 Bionic 6 nhân')
INSERT INTO ProductDetail VALUES(2,		N'RAM',					N'4 GB')
INSERT INTO ProductDetail VALUES(2,		N'Bộ nhớ trong',		N'512 GB')
INSERT INTO ProductDetail VALUES(2,		N'Thẻ SIM',				N'1 Nano SIM & 1 eSIM, Hỗ trợ 4G')
INSERT INTO ProductDetail VALUES(2,		N'Dung lượng pin',		N'3969 mAh, có sạc nhanh')
GO

-- For Product 3
INSERT INTO ProductDetail VALUES(3,		N'Màn hình',				N'Dynamic AMOLED 2X, 6.9", Quad HD+ (2K+)')
INSERT INTO ProductDetail VALUES(3,		N'Hệ điều hành',			N'Android 10')
INSERT INTO ProductDetail VALUES(3,		N'Camera sau',				N'Chính 108 MP & Phụ 12 MP, 12 MP, cảm biến Laser AF')
INSERT INTO ProductDetail VALUES(3,		N'Camera trước',			N'10 MP')
INSERT INTO ProductDetail VALUES(3,		N'CPU',						N'Exynos 990 8 nhân')
INSERT INTO ProductDetail VALUES(3,		N'RAM',						N'12 GB')
INSERT INTO ProductDetail VALUES(3,		N'Bộ nhớ trong',			N'256 GB')
INSERT INTO ProductDetail VALUES(3,		N'Thẻ nhớ',					N'MicroSD, hỗ trợ tối đa 1 TB')
INSERT INTO ProductDetail VALUES(3,		N'Thẻ SIM',					N'2 Nano SIM hoặc 1 Nano SIM + 1 eSIM, Hỗ trợ 5G')
INSERT INTO ProductDetail VALUES(3,		N'Dung lượng pin',			N'4500 mAh, có sạc nhanh')
GO

-- For Product 4
INSERT INTO ProductDetail VALUES(4,		N'Màn hình',				N'Super AMOLED, 6.5", Full HD+')
INSERT INTO ProductDetail VALUES(4,		N'Hệ điều hành',			N'Android 10')
INSERT INTO ProductDetail VALUES(4,		N'Camera sau',				N'Chính 12 MP & Phụ 12 MP, 8 MP')
INSERT INTO ProductDetail VALUES(4,		N'Camera trước',			N'32 MP')
INSERT INTO ProductDetail VALUES(4,		N'CPU',						N'Exynos 990 8 nhân')
INSERT INTO ProductDetail VALUES(4,		N'RAM',						N'8 GB')
INSERT INTO ProductDetail VALUES(4,		N'Bộ nhớ trong',			N'128 GB')
INSERT INTO ProductDetail VALUES(4,		N'Thẻ nhớ',					N'MicroSD, hỗ trợ tối đa 1 TB')
INSERT INTO ProductDetail VALUES(4,		N'Thẻ SIM',					N'2 Nano SIM (SIM 2 chung khe thẻ nhớ), Hỗ trợ 4G')
INSERT INTO ProductDetail VALUES(4,		N'Dung lượng pin',			N'4500 mAh, có sạc nhanh')
GO

-- For Product 5
INSERT INTO ProductDetail VALUES(5,		N'Màn hình',				N'Chính: Dynamic AMOLED, Phụ: Super AMOLED, Chính 7.59" & Phụ 6.23", Full HD+')
INSERT INTO ProductDetail VALUES(5,		N'Hệ điều hành',			N'Android 10')
INSERT INTO ProductDetail VALUES(5,		N'Camera sau',				N'3 camera 12 MP')
INSERT INTO ProductDetail VALUES(5,		N'Camera trước',			N'10 MP')
INSERT INTO ProductDetail VALUES(5,		N'CPU',						N'Snapdragon 865+ 8 nhân')
INSERT INTO ProductDetail VALUES(5,		N'RAM',						N'12 GB')
INSERT INTO ProductDetail VALUES(5,		N'Bộ nhớ trong',			N'256 GB')
INSERT INTO ProductDetail VALUES(5,		N'Thẻ SIM',					N'1 Nano SIM & 1 eSIM, Hỗ trợ 5G')
INSERT INTO ProductDetail VALUES(5,		N'Dung lượng pin',			N'4500 mAh, có sạc nhanh')
GO

-- For Product 6
INSERT INTO ProductDetail VALUES(6,		N'Màn hình',				N'AMOLED, 6.4", Full HD+')
INSERT INTO ProductDetail VALUES(6,		N'Hệ điều hành',			N'Android 10')
INSERT INTO ProductDetail VALUES(6,		N'Camera sau',				N'Chính 48 MP & Phụ 8 MP, 2 MP, 2 MP')
INSERT INTO ProductDetail VALUES(6,		N'Camera trước',			N'Chính 32 MP & Phụ cảm biến thông minh A.I')
INSERT INTO ProductDetail VALUES(6,		N'CPU',						N'Snapdragon 720G 8 nhân')
INSERT INTO ProductDetail VALUES(6,		N'RAM',						N'8 GB')
INSERT INTO ProductDetail VALUES(6,		N'Bộ nhớ trong',			N'128 GB')
INSERT INTO ProductDetail VALUES(6,		N'Thẻ nhớ',					N'MicroSD, hỗ trợ tối đa 256 GB')
INSERT INTO ProductDetail VALUES(6,		N'Thẻ SIM',					N'2 Nano SIM, Hỗ trợ 4G')
INSERT INTO ProductDetail VALUES(6,		N'Dung lượng pin',			N'4015 mAh, có sạc nhanh')
GO

-- For Product 7
INSERT INTO ProductDetail VALUES(7,		N'Màn hình',				N'Liquid Retina, 12.9"')
INSERT INTO ProductDetail VALUES(7,		N'Hệ điều hành',			N'iPadOS 13')
INSERT INTO ProductDetail VALUES(7,		N'CPU',						N'Apple A12Z Bionic')
INSERT INTO ProductDetail VALUES(7,		N'RAM',						N'6 GB')
INSERT INTO ProductDetail VALUES(7,		N'Bộ nhớ trong',			N'128 GB')
INSERT INTO ProductDetail VALUES(7,		N'Camera sau',				N'Chính 12 MP & Phụ 10 MP, TOF 3D LiDAR')
INSERT INTO ProductDetail VALUES(7,		N'Camera trước',			N'7 MP')
INSERT INTO ProductDetail VALUES(7,		N'Kết nối mạng',			N'Hỗ trợ 4G')
INSERT INTO ProductDetail VALUES(7,		N'Hỗ trợ SIM',				N'1 Nano SIM hoặc 1 eSIM')
INSERT INTO ProductDetail VALUES(7,		N'Đàm thoại',				N'FaceTime')
GO

-- For Product 8
INSERT INTO ProductDetail VALUES(8,		N'Màn hình',				N'IPS LCD, 10.2"')
INSERT INTO ProductDetail VALUES(8,		N'Hệ điều hành',			N'iPadOS 13')
INSERT INTO ProductDetail VALUES(8,		N'CPU',						N'Apple A10 Fusion 4 nhân')
INSERT INTO ProductDetail VALUES(8,		N'RAM',						N'3 GB')
INSERT INTO ProductDetail VALUES(8,		N'Bộ nhớ trong',			N'32 GB')
INSERT INTO ProductDetail VALUES(8,		N'Camera sau',				N'8 MP')
INSERT INTO ProductDetail VALUES(8,		N'Camera trước',			N'1.2 MP')
INSERT INTO ProductDetail VALUES(8,		N'Đàm thoại',				N'FaceTime')
INSERT INTO ProductDetail VALUES(8,		N'Trọng lượng',				N'483 g')
INSERT INTO ProductDetail VALUES(8,		N'Dung lượng pin',			N'32.4 Wh (Khoảng 8600 mAh)')
GO

-- For Product 9
INSERT INTO ProductDetail VALUES(9,		N'Màn hình',				N'Super AMOLED, 10.5"')
INSERT INTO ProductDetail VALUES(9,		N'Hệ điều hành',			N'Android 9')
INSERT INTO ProductDetail VALUES(9,		N'CPU',						N'Snapdragon 855 8 nhân')
INSERT INTO ProductDetail VALUES(9,		N'RAM',						N'6 GB')
INSERT INTO ProductDetail VALUES(9,		N'Bộ nhớ trong',			N'	128 GB')
INSERT INTO ProductDetail VALUES(9,		N'Camera sau',				N'Chính 13 MP & Phụ 5 MP')
INSERT INTO ProductDetail VALUES(9,		N'Camera trước',			N'8 MP')
INSERT INTO ProductDetail VALUES(9,		N'Kết nối mạng',			N'Hỗ trợ 4G')
INSERT INTO ProductDetail VALUES(9,		N'Hỗ trợ SIM',				N'1 Nano SIM')
INSERT INTO ProductDetail VALUES(9,		N'Đàm thoại',				N'Có')
GO

-- For Product 10
INSERT INTO ProductDetail VALUES(10,	N'Màn hình',				N'IPS LCD, 10.2"')
INSERT INTO ProductDetail VALUES(10,	N'Hệ điều hành',			N'iPadOS 13')
INSERT INTO ProductDetail VALUES(10,	N'CPU',						N'Apple A10 Fusion 4 nhân')
INSERT INTO ProductDetail VALUES(10,	N'RAM',						N'3 GB')
INSERT INTO ProductDetail VALUES(10,	N'Bộ nhớ trong',			N'128 GB')
INSERT INTO ProductDetail VALUES(10,	N'Camera sau',				N'8 MP')
INSERT INTO ProductDetail VALUES(10,	N'Camera trước',			N'1.2 MP')
INSERT INTO ProductDetail VALUES(10,	N'Đàm thoại',				N'FaceTime')
INSERT INTO ProductDetail VALUES(10,	N'Trọng lượng',				N'483 g')
INSERT INTO ProductDetail VALUES(10,	N'Dung lượng pin',			N'32.4 Wh (Khoảng 8600 mAh)')
GO

-- For Product 11
INSERT INTO ProductDetail VALUES(11,	N'CPU',						N'Intel Core i7 Comet Lake, 10750H, 2.60 GHz')
INSERT INTO ProductDetail VALUES(11,	N'RAM',						N'8 GB, DDR4 (2 khe), 2933 MHz')
INSERT INTO ProductDetail VALUES(11,	N'Ổ cứng',					N'Hỗ trợ thêm 2 khe cắm SSD M.2 PCIe, SSD 512 GB M.2 PCIe')
INSERT INTO ProductDetail VALUES(11,	N'Màn hình',				N'15.6 inch, Full HD (1920 x 1080), 144Hz')
INSERT INTO ProductDetail VALUES(11,	N'Card màn hình',			N'Card đồ họa rời, NVIDIA GeForce GTX 1650Ti 4GB')
INSERT INTO ProductDetail VALUES(11,	N'Cổng kết nối',			N'3 x USB 3.2, HDMI, LAN (RJ45), USB Type-C')
INSERT INTO ProductDetail VALUES(11,	N'Hệ điều hành',			N'Windows 10 Home SL')
INSERT INTO ProductDetail VALUES(11,	N'Thiết kế',				N'Vỏ nhựa, PIN liền')
INSERT INTO ProductDetail VALUES(11,	N'Kích thước',				N'Dày 25.8 mm, 2.3 Kg')
INSERT INTO ProductDetail VALUES(11,	N'Thời điểm ra mắt',		N'2020') 
GO

-- For Product 12
INSERT INTO ProductDetail VALUES(12,	N'CPU',						N'Intel Core i5 Comet Lake, 10300H, 2.50 GHz')
INSERT INTO ProductDetail VALUES(12,	N'RAM',						N'8 GB, DDR4 (2 khe), 2933 MHz')
INSERT INTO ProductDetail VALUES(12,	N'Ổ cứng',					N'Hỗ trợ thêm 2 khe cắm SSD M.2 PCIe, SSD 512 GB M.2 PCIe')
INSERT INTO ProductDetail VALUES(12,	N'Màn hình',				N'15.6 inch, Full HD (1920 x 1080), 144Hz')
INSERT INTO ProductDetail VALUES(12,	N'Card màn hình',			N'Card đồ họa rời, NVIDIA GeForce GTX 1650Ti 4GB')
INSERT INTO ProductDetail VALUES(12,	N'Cổng kết nối',			N'3 x USB 3.2, HDMI, LAN (RJ45), USB Type-C')
INSERT INTO ProductDetail VALUES(12,	N'Hệ điều hành',			N'Windows 10 Home SL')
INSERT INTO ProductDetail VALUES(12,	N'Thiết kế',				N'Vỏ nhựa, PIN liền')
INSERT INTO ProductDetail VALUES(12,	N'Kích thước',				N'Dày 25.8 mm, 2.3 kg')
INSERT INTO ProductDetail VALUES(12,	N'Thời điểm ra mắt',		N'2020')
GO

-- For Product 13
INSERT INTO ProductDetail VALUES(13,	N'CPU',						N'Intel Core i7 Ice Lake, 1065G7, 1.30 GHz')
INSERT INTO ProductDetail VALUES(13,	N'RAM',						N'8 GB, DDR4 (On board 4GB +1 khe 4GB), 2666 MHz')
INSERT INTO ProductDetail VALUES(13,	N'Ổ cứng',					N'SSD 512 GB M.2 PCIe, Hỗ trợ khe cắm HDD SATA')
INSERT INTO ProductDetail VALUES(13,	N'Màn hình',				N'15.6 inch, Full HD (1920 x 1080)')
INSERT INTO ProductDetail VALUES(13,	N'Card màn hình',			N'Card đồ họa tích hợp, Intel UHD Graphics')
INSERT INTO ProductDetail VALUES(13,	N'Cổng kết nối',			N'2 x USB 2.0, HDMI, USB 3.0, USB Type-C')
INSERT INTO ProductDetail VALUES(13,	N'Hệ điều hành',			N'Windows 10 Home SL + Office Home&Student 2019 vĩnh viễn')
INSERT INTO ProductDetail VALUES(13,	N'Thiết kế',				N'Vỏ nhựa, PIN liền')
INSERT INTO ProductDetail VALUES(13,	N'Kích thước',				N'Dày 22.9 mm, 1.8 kg')
INSERT INTO ProductDetail VALUES(13,	N'Thời điểm ra mắt',		N'2019')
GO

-- For Product 14
INSERT INTO ProductDetail VALUES(14,	N'CPU',						N'Intel Core i5 Comet Lake, 10210U, 1.60 GHz')
INSERT INTO ProductDetail VALUES(14,	N'RAM',						N'8 GB, DDR4 (On board 4GB +1 khe 4GB), 2666 MHz')
INSERT INTO ProductDetail VALUES(14,	N'Ổ cứng',					N'SSD 512 GB M.2 PCIe, Hỗ trợ khe cắm HDD SATA')
INSERT INTO ProductDetail VALUES(14,	N'Màn hình',				N'15.6 inch, Full HD (1920 x 1080)')
INSERT INTO ProductDetail VALUES(14,	N'Card màn hình',			N'Card đồ họa tích hợp, Intel UHD Graphics')
INSERT INTO ProductDetail VALUES(14,	N'Cổng kết nối',			N'2 x USB 2.0, USB 3.1, HDMI, USB Type-C')
INSERT INTO ProductDetail VALUES(14,	N'Hệ điều hành',			N'Windows 10 Home SL')
INSERT INTO ProductDetail VALUES(14,	N'Thiết kế',				N'Vỏ nhựa, PIN liền')
INSERT INTO ProductDetail VALUES(14,	N'Kích thước',				N'Dày 19.9 mm, 1.65 kg')
INSERT INTO ProductDetail VALUES(14,	N'Thời điểm ra mắt',		N'2019')
GO

-- For Product 15
INSERT INTO ProductDetail VALUES(15,	N'CPU',						N'Intel Core i7 Comet Lake, 10510U, 1.80 GHz')
INSERT INTO ProductDetail VALUES(15,	N'RAM',						N'16 GB, DDR4 (On board), 2666 MHz')
INSERT INTO ProductDetail VALUES(15,	N'Ổ cứng',					N'SSD 512 GB M.2 PCIe')
INSERT INTO ProductDetail VALUES(15,	N'Màn hình',				N'14 inch, Full HD (1920 x 1080)')
INSERT INTO ProductDetail VALUES(15,	N'Card màn hình',			N'Card đồ họa rời, NVIDIA Quadro P520, 4GB')
INSERT INTO ProductDetail VALUES(15,	N'Cổng kết nối',			N'2 x USB 3.1, HDMI, 2 x USB Type-C')
INSERT INTO ProductDetail VALUES(15,	N'Hệ điều hành',			N'Windows 10 Home SL')
INSERT INTO ProductDetail VALUES(15,	N'Thiết kế',				N'Vỏ kim loại, PIN liền')
INSERT INTO ProductDetail VALUES(15,	N'Kích thước',				N'Dày 17.9 mm, 1.41 kg')
INSERT INTO ProductDetail VALUES(15,	N'Thời điểm ra mắt',		N'2020')
GO

-- For Product 16
INSERT INTO ProductDetail VALUES(16,	N'CPU',						N'Intel Core i7 Comet Lake, 10510U, 1.80 GHz')
INSERT INTO ProductDetail VALUES(16,	N'RAM',						N'8 GB, DDR4 (On board), 2400 MHz')
INSERT INTO ProductDetail VALUES(16,	N'Ổ cứng',					N'SSD 512 GB M.2 PCIe')
INSERT INTO ProductDetail VALUES(16,	N'Màn hình',				N'13.3 inch, Full HD (1920 x 1080)')
INSERT INTO ProductDetail VALUES(16,	N'Card màn hình',			N'Card đồ họa rời, NVIDIA GeForce MX250 2GB')
INSERT INTO ProductDetail VALUES(16,	N'Cổng kết nối',			N'2 x USB 3.1, HDMI, 2 x USB Type-C')
INSERT INTO ProductDetail VALUES(16,	N'Hệ điều hành',			N'Windows 10 Home SL')
INSERT INTO ProductDetail VALUES(16,	N'Thiết kế',				N'Vỏ kim loại, PIN liền')
INSERT INTO ProductDetail VALUES(16,	N'Kích thước',				N'Dày 14.7 mm, 1.17 kg')
INSERT INTO ProductDetail VALUES(16,	N'Thời điểm ra mắt',		N'2019')
GO

-- For Product 17
INSERT INTO ProductDetail VALUES(17,	N'CPU',						N'Intel Core i7 Ice Lake, 1065G7, 1.30 GHz')
INSERT INTO ProductDetail VALUES(17,	N'RAM',						N'8 GB, DDR4 (On board), 2400 MHz')
INSERT INTO ProductDetail VALUES(17,	N'Ổ cứng',					N'SSD 512 GB M.2 PCIe')
INSERT INTO ProductDetail VALUES(17,	N'Màn hình',				N'13.3 inch, Full HD (1920 x 1080)')
INSERT INTO ProductDetail VALUES(17,	N'Card màn hình',			N'Card đồ họa tích hợp, Intel UHD Graphics')
INSERT INTO ProductDetail VALUES(17,	N'Cổng kết nối',			N'2 x USB 3.1, USB Type-C')
INSERT INTO ProductDetail VALUES(17,	N'Hệ điều hành',			N'Windows 10 Home SL')
INSERT INTO ProductDetail VALUES(17,	N'Thiết kế',				N'Chiếu nghỉ tay bằng vân gỗ, PIN liền')
INSERT INTO ProductDetail VALUES(17,	N'Kích thước',				N'Dày 14.7 mm, 1.3 kg')
INSERT INTO ProductDetail VALUES(17,	N'Thời điểm ra mắt',		N'	2020')
GO

-- For Product 18
INSERT INTO ProductDetail VALUES(18,	N'CPU',						N'Intel Core i5 Ice Lake, 1035G4, 1.10 GHz')
INSERT INTO ProductDetail VALUES(18,	N'RAM',						N'8 GB, DDR4 (On board), 3200 MHz')
INSERT INTO ProductDetail VALUES(18,	N'Ổ cứng',					N'SSD 256GB NVMe PCIe')
INSERT INTO ProductDetail VALUES(18,	N'Màn hình',				N'13.3 inch, Full HD (1920 x 1080)')
INSERT INTO ProductDetail VALUES(18,	N'Card màn hình',			N'Card đồ họa tích hợp, Intel Iris Plus Graphics')
INSERT INTO ProductDetail VALUES(18,	N'Cổng kết nối',			N'Thunderbolt 3, 2 x USB 3.1')
INSERT INTO ProductDetail VALUES(18,	N'Hệ điều hành',			N'Windows 10 Home SL + Office Home&Student 2019 vĩnh viễn')
INSERT INTO ProductDetail VALUES(18,	N'Thiết kế',				N'Vỏ kim loại, PIN liền')
INSERT INTO ProductDetail VALUES(18,	N'Kích thước',				N'Dày 16.9 mm, 1.258 kg')
INSERT INTO ProductDetail VALUES(18,	N'Thời điểm ra mắt',		N'2020')
GO

-- For Product 19
INSERT INTO ProductDetail VALUES(19,	N'CPU',						N'Intel Core i5 Ice Lake, 1035G1, 1.00 GHz')
INSERT INTO ProductDetail VALUES(19,	N'RAM',						N'8 GB, DDR4 (On board), 3200 MHz')
INSERT INTO ProductDetail VALUES(19,	N'Ổ cứng',					N'SSD 512 GB M.2 PCIe, Không hỗ trợ khe cắm HDD')
INSERT INTO ProductDetail VALUES(19,	N'Màn hình',				N'14 inch, Full HD (1920 x 1080)')
INSERT INTO ProductDetail VALUES(19,	N'Card màn hình',			N'	Card đồ họa tích hợp, Intel UHD Graphics')
INSERT INTO ProductDetail VALUES(19,	N'Cổng kết nối',			N'2 x USB 3.0, HDMI, USB Type-C')
INSERT INTO ProductDetail VALUES(19,	N'Hệ điều hành',			N'Windows 10 Home SL')
INSERT INTO ProductDetail VALUES(19,	N'Thiết kế',				N'Vỏ nhựa - nắp lưng bằng kim loại, PIN liền')
INSERT INTO ProductDetail VALUES(19,	N'Kích thước',				N'Dày 20.4 mm, 1.5 kg')
INSERT INTO ProductDetail VALUES(19,	N'Thời điểm ra mắt',		N'2020')
GO

-- For Product 20
INSERT INTO ProductDetail VALUES(20,	N'CPU',						N'Intel Core i5 Comet Lake, 10210U, 1.60 GHz')
INSERT INTO ProductDetail VALUES(20,	N'RAM',						N'8 GB, DDR4 (On board), 2666 MHz')
INSERT INTO ProductDetail VALUES(20,	N'Ổ cứng',					N'SSD 512 GB M.2 PCIe')
INSERT INTO ProductDetail VALUES(20,	N'Màn hình',				N'13 inch, WQHD (2160x1350)')
INSERT INTO ProductDetail VALUES(20,	N'Card màn hình',			N'Card đồ họa tích hợp, Intel UHD Graphics')
INSERT INTO ProductDetail VALUES(20,	N'Cổng kết nối',			N'2 x USB Type-C (Power Delivery and DisplayPort), USB Type-C')
INSERT INTO ProductDetail VALUES(20,	N'Hệ điều hành',			N'Windows 10 Home SL')
INSERT INTO ProductDetail VALUES(20,	N'Thiết kế',				N'Vỏ kim loại, PIN liền')
INSERT INTO ProductDetail VALUES(20,	N'Kích thước',				N'Dày 9.19 mm, 0.7988 kg + Bàn phím 0.3695 kg')
INSERT INTO ProductDetail VALUES(20,	N'Thời điểm ra mắt',		N'2020')
GO

-- For Product 21
INSERT INTO ProductDetail VALUES(21,	N'Công nghệ màn hình',			N'OLED')
INSERT INTO ProductDetail VALUES(21,	N'Kích thước màn hình',			N'1.78 inch')
INSERT INTO ProductDetail VALUES(21,	N'Thời gian sử dụng pin',		N'Khoảng 1.5 ngày')
INSERT INTO ProductDetail VALUES(21,	N'Hệ điều hành',				N'watchOS 7.0')
INSERT INTO ProductDetail VALUES(21,	N'Kết nối với hệ điều hành',	N'iOS 14 trở lên')
INSERT INTO ProductDetail VALUES(21,	N'Chất liệu mặt',				N'Kính cường lực Saphire')
INSERT INTO ProductDetail VALUES(21,	N'Đường kính mặt',				N'44 mm')
INSERT INTO ProductDetail VALUES(21,	N'Kết nối',						N'Wifi, Bluetooth v5.0, GPS, Hỗ trợ eSim')
INSERT INTO ProductDetail VALUES(21,	N'Ngôn ngữ',					N'Tiếng Việt, Tiếng Anh')
INSERT INTO ProductDetail VALUES(21,	N'Theo dõi sức khỏe',			N'Đo nhịp tim, Theo dõi giấc ngủ, Tính lượng calories tiêu thụ, Đếm số bước chân, Tính quãng đường chạy, Chế độ luyện tập')
GO 

-- For Product 22
INSERT INTO ProductDetail VALUES(22,	N'Công nghệ màn hình',			N'MIP')
INSERT INTO ProductDetail VALUES(22,	N'Kích thước màn hình',			N'1.3 inch')
INSERT INTO ProductDetail VALUES(22,	N'Thời gian sử dụng pin',		N'Khoảng 21 ngày, Khoảng 60 giờ khi sử dụng GPS')
INSERT INTO ProductDetail VALUES(22,	N'Kết nối với hệ điều hành',	N'Android, iOS')
INSERT INTO ProductDetail VALUES(22,	N'Chất liệu mặt',				N'Kính Saphire')
INSERT INTO ProductDetail VALUES(22,	N'Đường kính mặt',				N'51 mm')
INSERT INTO ProductDetail VALUES(22,	N'Kết nối',						N'Wifi, Bluetooth, GPS')
INSERT INTO ProductDetail VALUES(22,	N'Ngôn ngữ',					N'Tiếng Việt, Tiếng Anh')
INSERT INTO ProductDetail VALUES(22,	N'Theo dõi sức khỏe',			N'Theo dõi mức độ stress, Đo nồng độ oxy trong máu (SpO2), Đo lượng tiêu thụ oxy tối đa (VO2 max), Đo nhịp tim, Theo dõi giấc ngủ, Tính lượng calories tiêu thụ, Tính quãng đường chạy, Đếm số bước chân, Chế độ luyện tập')
GO 

-- For Product 23
INSERT INTO ProductDetail VALUES(23,	N'Công nghệ màn hình',			N'Active Matrix LCD with LED Backlight')
INSERT INTO ProductDetail VALUES(23,	N'Kích thước màn hình',			N'1.97 inch')
INSERT INTO ProductDetail VALUES(23,	N'Thời gian sử dụng pin',		N'Khoảng 7 ngày, Khoảng 120 giờ khi sử dụng GPS')
INSERT INTO ProductDetail VALUES(23,	N'Hệ điều hành',				N'Suunto OS')
INSERT INTO ProductDetail VALUES(23,	N'Kết nối với hệ điều hành',	N'Android 5.0 trở lên, iOS 12 trở lên')
INSERT INTO ProductDetail VALUES(23,	N'Chất liệu mặt',				N'Kính Saphire')
INSERT INTO ProductDetail VALUES(23,	N'Đường kính mặt',				N'50 mm')
INSERT INTO ProductDetail VALUES(23,	N'Kết nối',						N'Bluetooth, GPS')
INSERT INTO ProductDetail VALUES(23,	N'Ngôn ngữ',					N'Tiếng Anh')
INSERT INTO ProductDetail VALUES(23,	N'Theo dõi sức khỏe',			N'Đánh giá chỉ số trẻ hóa của cơ thể (Fitness Age), Theo dõi mức độ stress, Đo nhịp tim, Theo dõi giấc ngủ, Tính lượng calories tiêu thụ, Tính quãng đường chạy, Đếm số bước chân, Chế độ luyện tập')
GO 

-- For Product 24
INSERT INTO ProductDetail VALUES(24,	N'Công nghệ màn hình',			N'OLED')
INSERT INTO ProductDetail VALUES(24,	N'Kích thước màn hình',			N'1.78 inch')
INSERT INTO ProductDetail VALUES(24,	N'Thời gian sử dụng pin',		N'Khoảng 1.5 ngày')
INSERT INTO ProductDetail VALUES(24,	N'Hệ điều hành',				N'watchOS 6.0')
INSERT INTO ProductDetail VALUES(24,	N'Kết nối với hệ điều hành',	N'iOS 13 trở lên')
INSERT INTO ProductDetail VALUES(24,	N'Chất liệu mặt',				N'Ion-X strengthened glass')
INSERT INTO ProductDetail VALUES(24,	N'Đường kính mặt',				N'44 mm')
INSERT INTO ProductDetail VALUES(24,	N'Kết nối',						N'Wifi, Bluetooth v5.0, GPS')
INSERT INTO ProductDetail VALUES(24,	N'Ngôn ngữ',					N'Tiếng Việt, Tiếng Anh')
INSERT INTO ProductDetail VALUES(24,	N'Theo dõi sức khỏe',			N'Đo nhịp tim, Tính lượng calories tiêu thụ, Đếm số bước chân, Tính quãng đường chạy, Chế độ luyện tập')
GO 

-- For Product 25
INSERT INTO ProductDetail VALUES(25,	N'Hiệu suất sạc',				N'60%')
INSERT INTO ProductDetail VALUES(25,	N'Dung lượng pin',				N'20.000 mAh')
INSERT INTO ProductDetail VALUES(25,	N'Thời gian sạc đầy pin',		N'19 - 20 giờ (dùng Adapter 1A)10 - 11 giờ (dùng Adapter 2A)')
INSERT INTO ProductDetail VALUES(25,	N'Nguồn vào',					N'Type C: 5V - 3A, 9V - 3A, 12V - 3A, 15V - 3A, 20V - 2.25A')
INSERT INTO ProductDetail VALUES(25,	N'Nguồn ra',					N'USB QC: 5V - 2.4A, 9V - 2A, 12V - 1.5AType C: 5V - 3A, 9V - 3A, 12V - 3A, 15V - 3A, 20V - 2A')
INSERT INTO ProductDetail VALUES(25,	N'Lõi pin',						N'Polymer')
INSERT INTO ProductDetail VALUES(25,	N'Công nghệ/Tiện ích',			N'Power Delivery')
INSERT INTO ProductDetail VALUES(25,	N'Kích thước',					N'Dài 15cm - Rộng 7.2cm - Dày 2.5cm')
INSERT INTO ProductDetail VALUES(25,	N'Trọng lượng',					N'532g')
INSERT INTO ProductDetail VALUES(25,	N'Thương hiệu của',				N'Trung Quốc')
INSERT INTO ProductDetail VALUES(25,	N'Sản xuất tại',				N'Trung Quốc')
GO 

-- For Product 26
INSERT INTO ProductDetail VALUES(26,	N'Hiệu suất sạc',				N'65%')
INSERT INTO ProductDetail VALUES(26,	N'Dung lượng pin',				N'20.000 mAh')
INSERT INTO ProductDetail VALUES(26,	N'Thời gian sạc đầy pin',		N'19 - 20 giờ (dùng Adapter 1A)12 - 14 giờ (dùng Adapter 2A)')
INSERT INTO ProductDetail VALUES(26,	N'Nguồn vào',					N'Type C (PD): 5V - 3A, 9V - 2A, 15V - 1.2A')
INSERT INTO ProductDetail VALUES(26,	N'Nguồn ra',					N'	Type C (PD): 5V - 3A, 9V - 2A, 15V - 1.2AUSB (PiQ 2.0): 5-6V - 3A, 6-9V - 2A, 9-12V - 1.5A')
INSERT INTO ProductDetail VALUES(26,	N'Lõi pin',						N'Polymer')
INSERT INTO ProductDetail VALUES(26,	N'Công nghệ/Tiện ích',			N'PowerIQPower Delivery')
INSERT INTO ProductDetail VALUES(26,	N'Kích thước',					N'Dài 15.7 cm - Rộng 7.4 cm - Dày 2 cm')
INSERT INTO ProductDetail VALUES(26,	N'Trọng lượng',					N'345g')
INSERT INTO ProductDetail VALUES(26,	N'Thương hiệu của',				N'Trung Quốc')
INSERT INTO ProductDetail VALUES(26,	N'Sản xuất tại',				N'Trung Quốc')
GO 

-- For Product 27
INSERT INTO ProductDetail VALUES(27,	N'Hiệu suất sạc',				N'65%')
INSERT INTO ProductDetail VALUES(27,	N'Dung lượng pin',				N'10.000 mAh')
INSERT INTO ProductDetail VALUES(27,	N'Thời gian sạc đầy pin',		N'10 - 11 giờ (dùng Adapter 1A)5 - 6 giờ (dùng Adapter 2A)')
INSERT INTO ProductDetail VALUES(27,	N'Nguồn vào',					N'Micro USB/Type-C: 5V - 2A')
INSERT INTO ProductDetail VALUES(27,	N'Nguồn ra',					N'Sạc không dây: 10W (Android), 5W/7W (iOS)Type-C PD: 5V - 3A, 9V - 2A, 12V - 1.5AUSB: 5V - 3A, 9V - 2A, 12V - 1.5A')
INSERT INTO ProductDetail VALUES(27,	N'Lõi pin',						N'Polymer')
INSERT INTO ProductDetail VALUES(27,	N'Công nghệ/Tiện ích',			N'Auto Voltage SensingĐèn LED báo hiệuQuick Charge 3.0Multi Protocol Fast ChargingSạc không dây chuẩn QiPower Delivery')
INSERT INTO ProductDetail VALUES(27,	N'Trọng lượng',					N'~210g')
INSERT INTO ProductDetail VALUES(27,	N'Thương hiệu của',				N'Mỹ')
INSERT INTO ProductDetail VALUES(27,	N'Sản xuất tại',				N'Trung Quốc')
GO 

-- For Product 28
INSERT INTO ProductDetail VALUES(28,	N'Chức năng',					N'Chuyển đổi cổng kết nối')
INSERT INTO ProductDetail VALUES(28,	N'Đầu vào',						N'USB Type-C')
INSERT INTO ProductDetail VALUES(28,	N'Đầu ra',						N'Jack 3.5mmUSB Type-CHDMI1 Cổng LANKhe đọc thẻ nhớ SDKhe đọc thẻ nhớ Micro SD3 cổng USB')
INSERT INTO ProductDetail VALUES(28,	N'Thương hiệu của',				N'Mỹ')
INSERT INTO ProductDetail VALUES(28,	N'Sản xuất tại',				N'Trung Quốc')
GO 

-- For Product 29
INSERT INTO ProductDetail VALUES(29,	N'Chức năng',					N'Sạc')
INSERT INTO ProductDetail VALUES(29,	N'Đầu ra',						N'USB: 5V - 2.4AType C PD: 5V - 3A , 9V - 3A, 15V - 3A, 20V - 5A')
INSERT INTO ProductDetail VALUES(29,	N'Công nghệ/Tiện ích',			N'PowerIQPower Delivery')
INSERT INTO ProductDetail VALUES(29,	N'Thương hiệu của',				N'Trung Quốc')
INSERT INTO ProductDetail VALUES(29,	N'Sản xuất tại',				N'Trung Quốc')
GO 

-- For Product 30
INSERT INTO ProductDetail VALUES(30,	N'Tương thích',					N'Android, iOS (iPhone, iPad), Windows')
INSERT INTO ProductDetail VALUES(30,	N'Jack cắm',					N'3.5 mm')
INSERT INTO ProductDetail VALUES(30,	N'Cổng sạc',					N'Type-C')
INSERT INTO ProductDetail VALUES(30,	N'Công nghệ âm thanh',			N'Chống ồn HD QN1')
INSERT INTO ProductDetail VALUES(30,	N'Thời gian sử dụng',			N'30 giờ')
INSERT INTO ProductDetail VALUES(30,	N'Thời gian sạc đầy',			N'3 giờ')
INSERT INTO ProductDetail VALUES(30,	N'Kết nối cùng lúc',			N'2 thiết bị')
INSERT INTO ProductDetail VALUES(30,	N'Hỗ trợ kết nối',				N'10m (Bluetooth 5.0)')
INSERT INTO ProductDetail VALUES(30,	N'Phím điều khiển',				N'Nghe/nhận cuộc gọi Chuyển bài hát Tăng/giảm âm lượng')
INSERT INTO ProductDetail VALUES(30,	N'Trọng lượng',					N'254 g')
INSERT INTO ProductDetail VALUES(30,	N'Thương hiệu của',				N'Nhật Bản')
INSERT INTO ProductDetail VALUES(30,	N'Sản xuất tạ',					N'Malaysia')
GO 

-- For Product 31
INSERT INTO ProductDetail VALUES(31,	N'Tương thích',					N'AndroidWindowsiOS (iPhone)')
INSERT INTO ProductDetail VALUES(31,	N'Jack cắm',					N'3.5 mm')
INSERT INTO ProductDetail VALUES(31,	N'Cổng sạc',					N'Micro USB')
INSERT INTO ProductDetail VALUES(31,	N'Công nghệ âm thanh',			N'Active Noise Cancelling')
INSERT INTO ProductDetail VALUES(31,	N'Thời gian sử dụng',			N'22 giờ')
INSERT INTO ProductDetail VALUES(31,	N'Thời gian sạc đầy',			N'3 giờ')
INSERT INTO ProductDetail VALUES(31,	N'Kết nối cùng lúc',			N'1 thiết bị')
INSERT INTO ProductDetail VALUES(31,	N'Hỗ trợ kết nối',				N'Bluetooth 4.0') 
INSERT INTO ProductDetail VALUES(31,	N'Thương hiệu của',				N'Mỹ')
INSERT INTO ProductDetail VALUES(31,	N'Sản xuất tạ',					N'Trung Quốc')
GO 

-- For Product 32
INSERT INTO ProductDetail VALUES(32,	N'Tương thích',					N'MacOS (Macbook, iMac)iOS (iPhone)')
INSERT INTO ProductDetail VALUES(32,	N'Cổng sạc',					N'LightningSạc không dây')
INSERT INTO ProductDetail VALUES(32,	N'Công nghệ âm thanh',			N'Active Noise CancellingAdaptive EQ')
INSERT INTO ProductDetail VALUES(32,	N'Thời gian sử dụng',			N'4.5 giờ nghe nhạc, 3.5 giờ đàm thoại (chế độ chống ồn chủ động)20 giờ nghe nhạc, 18 giờ đàm thoại (sử dụng với hộp sạc)')
INSERT INTO ProductDetail VALUES(32,	N'Kết nối cùng lúc',			N'1 thiết bị')
INSERT INTO ProductDetail VALUES(32,	N'Hỗ trợ kết nối',				N'Bluetooth')
INSERT INTO ProductDetail VALUES(32,	N'Phím điều khiển',				N'Mic thoạiNghe/nhận cuộc gọiPhát/dừng chơi nhạcChuyển bài hát')
INSERT INTO ProductDetail VALUES(32,	N'Trọng lượng',					N'5.4g/1 tai nghe, 45.6g/hộp sạc')
INSERT INTO ProductDetail VALUES(32,	N'Thương hiệu của',				N'Mỹ')
INSERT INTO ProductDetail VALUES(32,	N'Sản xuất tại',				N'Việt Nam / Trung Quốc (tùy lô hàng)')
GO 

-- For Product 33
INSERT INTO ProductDetail VALUES(33,	N'Tương thích',					N'AndroidMacOS (Macbook, iMac)iOS (iPhone)')
INSERT INTO ProductDetail VALUES(33,	N'Cổng sạc',					N'LightningSạc không dây')
INSERT INTO ProductDetail VALUES(33,	N'Thời gian sạc đầy',			N'15 phút')
INSERT INTO ProductDetail VALUES(33,	N'Kết nối cùng lúc',			N'1 thiết bị')
INSERT INTO ProductDetail VALUES(33,	N'Hỗ trợ kết nối',				N'Bluetooth')
INSERT INTO ProductDetail VALUES(33,	N'Phím điều khiển',				N'Mic thoạiNghe/nhận cuộc gọiPhát/dừng chơi nhạcChuyển bài hát')
INSERT INTO ProductDetail VALUES(33,	N'Trọng lượng',					N'4 g') 
INSERT INTO ProductDetail VALUES(33,	N'Thương hiệu của',				N'Mỹ')
INSERT INTO ProductDetail VALUES(33,	N'Sản xuất tại',				N'Việt Nam / Trung Quốc (tùy lô hàng)') 
GO 

-- For Product 34
INSERT INTO ProductDetail VALUES(34,	N'Tương thích',					N'AndroidWindowsiOS (iPhone)')
INSERT INTO ProductDetail VALUES(34,	N'Cổng sạc',					N'Type-C')
INSERT INTO ProductDetail VALUES(34,	N'Thời gian sử dụng',			N'6.5 giờ')
INSERT INTO ProductDetail VALUES(34,	N'Thời gian sạc đầy',			N'2.5 giờ')
INSERT INTO ProductDetail VALUES(34,	N'Kết nối cùng lúc',			N'1 thiết bị')
INSERT INTO ProductDetail VALUES(34,	N'Hỗ trợ kết nối',				N'10m (Bluetooth 4.2)')
INSERT INTO ProductDetail VALUES(34,	N'Phím điều khiển',				N'Mic thoạiNghe/nhận cuộc gọiPhát/dừng chơi nhạcTăng/giảm âm lượngBật/ Tắt Bluetooth')
INSERT INTO ProductDetail VALUES(34,	N'Trọng lượng',					N'34 g') 
INSERT INTO ProductDetail VALUES(34,	N'Thương hiệu của',				N'Nhật Bản')
INSERT INTO ProductDetail VALUES(34,	N'Sản xuất tại',				N'Malaysia') 
GO 

-------------------------------------------------------------------------------INSERT ProductOption
-- For Product 1
INSERT INTO ProductOption VALUES(1, 21990000,	19490000,		N'64GB')
INSERT INTO ProductOption VALUES(1, 23990000,	20490000,		N'128GB')
INSERT INTO ProductOption VALUES(1, 25990000,	23990000,		N'256GB')
GO

-- For Prodcut 2 
INSERT INTO ProductOption VALUES(2, 37990000,	35990000,		N'512GB') 
INSERT INTO ProductOption VALUES(2, 30990000,	27990000,		N'256GB')
GO

-- For Prodcut 3 
INSERT INTO ProductOption VALUES(3, 23990000,		21990000,		N'Note 20') 
INSERT INTO ProductOption VALUES(3,	29990000,		27990000,		N'Note 20 Ultra')
INSERT INTO ProductOption VALUES(3,	32990000,		30990000,		N' Note 20 Ultra 5G') 
INSERT INTO ProductOption VALUES(3,	32990000,		30990000,		N'Note 20 Ultra 5G Trắng')
GO

-- For Prodcut 4 
INSERT INTO ProductOption VALUES(4, 17990000,		15990000,		N'S20 FE') 
INSERT INTO ProductOption VALUES(4,	18990000,		16990000,		N'S20 FE Trắng')
GO

-- For Producr 5
INSERT INTO ProductOption VALUES(5, 53000000,		50000000,		N'Galaxy Z Fold2 5G') 
INSERT INTO ProductOption VALUES(5,	54000000,		51000000,		N'Galaxy Z Fold2 5G Trắng')
GO

-- For Product 6
INSERT INTO ProductOption VALUES(6, 8490000,		8190000,		N'Reno4') 
INSERT INTO ProductOption VALUES(6,	12990000,		11990000,		N'Reno4 Pro') 
GO

-- For Product 7
INSERT INTO ProductOption VALUES(7, 27990000,		26490000,		N'WiFi') 
INSERT INTO ProductOption VALUES(7,	31990000,		30990000,		N'WiFi + 4G') 
GO

-- For Product 8
INSERT INTO ProductOption VALUES(8, 10990000,		9990000,		N'WiFi 32GB') 
INSERT INTO ProductOption VALUES(8,	11990000,		11490000,		N'WiFi 128GB') 
GO

-- For Product 9
INSERT INTO ProductOption VALUES(9, 18490000,		16990000,		N'Tab S6') 
INSERT INTO ProductOption VALUES(9,	20490000,		18990000,		N'Tab S6 Pro') 
GO

-- For Product 10
INSERT INTO ProductOption VALUES(10, 10990000,		9990000,		N'WiFi 32GB') 
INSERT INTO ProductOption VALUES(10, 11990000,		11490000,		N'WiFi 128GB') 
GO

-- For Product 11
INSERT INTO ProductOption VALUES(11, 30990000,		28990000,		N'Rog Strix G512') 
INSERT INTO ProductOption VALUES(11, 32990000,		30990000,		N'Rog Strix G512 Pro') 
GO

-- For Product 12
INSERT INTO ProductOption VALUES(12, 25490000,		24490000,		N'Rog Strix G512') 
INSERT INTO ProductOption VALUES(12, 28490000,		26990000,		N'Rog Strix G512 Pro') 
GO

-- For Product 13
INSERT INTO ProductOption VALUES(13, 20190000,		19190000,		N'EJ232TS') 
INSERT INTO ProductOption VALUES(13, 23490000,		22990000,		N'EJ232TS Pro') 
GO

-- For Product 14
INSERT INTO ProductOption VALUES(14, 17990000,		16990000,		N'EJ1734T') 
INSERT INTO ProductOption VALUES(14, 21490000,		19990000,		N'EJ1734T Pro') 
GO

-- For Product 15
INSERT INTO ProductOption VALUES(15, 40890000,		38890000,		N'8VK71AV') 
INSERT INTO ProductOption VALUES(15, 44990000,		43290000,		N'8VK71AV Pro') 
GO

-- For Product 16
INSERT INTO ProductOption VALUES(16, 31490000,		29990000,		N'8XS68PA') 
INSERT INTO ProductOption VALUES(16, 33490000,		31790000,		N'8XS68PA Pro') 
GO

-- For Product 17
INSERT INTO ProductOption VALUES(17, 29490000,		28490000,		N'8ST69PA') 
INSERT INTO ProductOption VALUES(17, 31690000,		29990000,		N'8ST69PA Pro') 
GO

-- For Product 18
INSERT INTO ProductOption VALUES(18, 22490000,		21990000,		N'171M2PA') 
INSERT INTO ProductOption VALUES(18, 25690000,		23990000,		N'171M2PA Pro') 
GO

-- For Product 19
INSERT INTO ProductOption VALUES(19, 19490000,		18990000,		N'81X1001UVN') 
INSERT INTO ProductOption VALUES(19, 24690000,		22990000,		N'81X1001UVN Pro') 
GO

-- For Product 20
INSERT INTO ProductOption VALUES(20, 27490000,		26990000,		N'82AS007BVN') 
INSERT INTO ProductOption VALUES(20, 32690000,		30990000,		N'82AS007BVN Pro') 
GO

-- For Product 21
INSERT INTO ProductOption VALUES(21, 21990000,		19791000,		N'LTE 40mm viền thép') 
INSERT INTO ProductOption VALUES(21, 22990000,		20691000,		N'LTE 44mm viền thép') 
GO

-- For Product 22
INSERT INTO ProductOption VALUES(22, 22990000,		21490000,		N'81S1501UKN') 
INSERT INTO ProductOption VALUES(22, 24990000,		23691000,		N'81S1501UKN Pro') 
GO

-- For Product 23
INSERT INTO ProductOption VALUES(23, 19990000,		18100000,		N'16TZ501UOK') 
INSERT INTO ProductOption VALUES(23, 22990000,		21691000,		N'16TZ501UOK Pro') 
GO

-- For Product 24
INSERT INTO ProductOption VALUES(24, 10990000,		9341000,		N'44mm hồng') 
INSERT INTO ProductOption VALUES(24, 11990000,		10191000,		N'LTE 40mm đen') 
INSERT INTO ProductOption VALUES(24, 11990000,		10191000,		N'LTE 40mm hồng') 
INSERT INTO ProductOption VALUES(24, 12990000,		11041000,		N'44mm đen')	 
INSERT INTO ProductOption VALUES(24, 12990000,		11041000,		N'44mm trắng')	
GO

-- For Product 25
INSERT INTO ProductOption VALUES(25, 1990000,		1490000,		N'VXN4254GL trắng') 
INSERT INTO ProductOption VALUES(25, 1990000,		1490000,		N'VXN4254GL đen')  
GO

-- For Product 26
INSERT INTO ProductOption VALUES(26, 1999000,		1499000,		N'A1281 Fabric trắng') 
INSERT INTO ProductOption VALUES(26, 1999000,		1499000,		N'A1281 Fabric đen')  
GO

-- For Product 27
INSERT INTO ProductOption VALUES(27, 1399000,		1000000,		N'QE10007PQ trắng') 
INSERT INTO ProductOption VALUES(27, 1399000,		1000000,		N'QE10007PQ xám')  
GO

-- For Product 28
INSERT INTO ProductOption VALUES(28, 2999000,		2799000,		N'HD30F trắng') 
INSERT INTO ProductOption VALUES(28, 2999000,		2799000,		N'HD30F xám')  
GO

-- For Product 29
INSERT INTO ProductOption VALUES(29, 2600000,		2210000,		N'A2041 trắng') 
INSERT INTO ProductOption VALUES(29, 2600000,		2210000,		N'A2041 xám')  
GO

-- For Product 30
INSERT INTO ProductOption VALUES(30, 8990000,		8490000,		N'WH-1000XM4 trắng') 
INSERT INTO ProductOption VALUES(30, 8990000,		8490000,		N'WH-1000XM4 đen')  
GO

-- For Product 31
INSERT INTO ProductOption VALUES(31, 7690000,		6690000,		N'MX422') 
INSERT INTO ProductOption VALUES(31, 7990000,		6990000,		N'MX432')  
GO

-- For Product 32
INSERT INTO ProductOption VALUES(32, 6990000,		5990000,		N'MWP22') 
INSERT INTO ProductOption VALUES(32, 7590000,		6590000,		N'MWP32')  
GO

-- For Product 33
INSERT INTO ProductOption VALUES(33, 5990000,		4990000,		N'Sạc không dây') 
INSERT INTO ProductOption VALUES(33, 4990000,		3990000,		N'Sạc có dây')  
GO

-- For Product 34
INSERT INTO ProductOption VALUES(34, 4290000,		3890000,		N'WI-C600N trắng') 
INSERT INTO ProductOption VALUES(34, 4290000,		3890000,		N'WI-C600N đen')  
GO

-------------------------------------------------------------------------------
-- INSERT Comment 
INSERT INTO Comment VALUES(2,	1,	15,		1,	5,	N'Sản phẩm tốt!',			N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh.')
INSERT INTO Comment VALUES(2,	2,	10,		2,	4,	N'Sản hphẩm chất lượng!',	N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh. Nhân viên nhiệt tình.')
INSERT INTO Comment VALUES(3,	1,	14,		3,	4,	N'Sản phẩm tốt!',			N'Sán phẩm giá tốt, đẹp, phù hợp với mọi người.')
INSERT INTO Comment VALUES(3,	2,	16,		4,	5,	N'Sản phẩm tốt!',			N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh.')
INSERT INTO Comment VALUES(4,	1,	9,		5,	4,	N'Sản hphẩm chất lượng!',	N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh. Nhân viên nhiệt tình.')
INSERT INTO Comment VALUES(4,	3,	8,		6,	4,	N'Sản phẩm tốt!',			N'Sán phẩm giá tốt, đẹp, phù hợp với mọi người.')
INSERT INTO Comment VALUES(2,	4,	3,		7,	5,	N'Sản phẩm tốt!',			N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh.')
INSERT INTO Comment VALUES(2,	5,	8,		3,	4,	N'Sản hphẩm chất lượng!',	N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh. Nhân viên nhiệt tình.')
INSERT INTO Comment VALUES(3,	6,	43,		4,	4,	N'Sản phẩm tốt!',			N'Sán phẩm giá tốt, đẹp, phù hợp với mọi người.')
INSERT INTO Comment VALUES(4,	7,	43,		2,	5,	N'Sản phẩm tốt!',			N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh.')
INSERT INTO Comment VALUES(3,	8,	13,		5,	4,	N'Sản hphẩm chất lượng!',	N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh. Nhân viên nhiệt tình.')
INSERT INTO Comment VALUES(2,	9,	20,		6,	4,	N'Sản phẩm tốt!',			N'Sán phẩm giá tốt, đẹp, phù hợp với mọi người.')
INSERT INTO Comment VALUES(3,	10,	32,		3,	5,	N'Sản phẩm tốt!',			N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh.')
INSERT INTO Comment VALUES(2,	11,	22,		2,	4,	N'Sản hphẩm chất lượng!',	N'Sán phẩm mới mua, dùng rất tốt, chụp hình rất đẹp, giá tốt giao hàng nhanh. Nhân viên nhiệt tình.')
INSERT INTO Comment VALUES(4,	12,	55,		12,	4,	N'Sản phẩm tốt!',			N'Sán phẩm giá tốt, đẹp, phù hợp với mọi người.')
GO
 
--	CustomerID INT FOREIGN KEY REFERENCES Customer(ID),
--	ProductID INT FOREIGN KEY REFERENCES Product(ID),
--	LikeNumber INT,
--	DisLikeNumber INT,
--	StarNumber INT,
--	Reason NVARCHAR(100),
--	Description NVARCHAR(500),

-------------------------------------------------------------------------------
--INSERT BillStatus
INSERT INTO BillStatus VALUES(N'Chờ duyệt')					-- 1
INSERT INTO BillStatus VALUES(N'Đang vận chuyển')			-- 2
INSERT INTO BillStatus VALUES(N'Chờ thanh toán')			-- 3
INSERT INTO BillStatus VALUES(N'Đơn hàng thành công')		-- 4
INSERT INTO BillStatus VALUES(N'Đơn hàng đã hủy')			-- 5
GO

-------------------------------------------------------------------------------
-- INSERT Bill
INSERT INTO Bill VALUES(GETDATE(), 19490000,	4,	2,	0)
INSERT INTO Bill VALUES(GETDATE(), 35990000,	4,	2,	0)
INSERT INTO Bill VALUES(GETDATE(), 19490000,	4,	3,	0)

-------------------------------------------------------------------------------
-- INSERT BillInfo
INSERT INTO BillInfo VALUES(1,	19490000,	1,	1,	1)
INSERT INTO BillInfo VALUES(1,	35990000,	2,	4,	2)
INSERT INTO BillInfo VALUES(1,	19490000,	1,	1,	3)
 
-------------------------------------------------------------------------------
-- INSERT Notification
INSERT INTO Notification VALUES(2,	N'Đổi mật khẩu!',	GETDATE(),	0,	N'Hãy thay đổi mật khẩu thường xuyên để nâng cao bảo mật. Ngoài ra: 1) Không nên sử dụng chung mật khẩu của email với mật khẩu của các tài khoản khác. 2) Luôn đăng xuất khỏi các tài khoản sau khi sử dụng trên thiết bị công cộng hoặc thiết bị không phải của bản thân.')
INSERT INTO Notification VALUES(2,	N'Thông báo!',		GETDATE(),	0,	N'Từ ngày 26/06/2020, Tiki áp dụng phí vận chuyển dựa trên khối lượng/ kích thước đóng gói của các sản phẩm trong đơn hàng, và khoảng cách giữa địa chỉ nhận hàng và kho của Tiki/nhà cung cấp/nhà bán hàng.')
INSERT INTO Notification VALUES(2,	N'Đổi mật khẩu!',	GETDATE(),	0,	N'Hãy thay đổi mật khẩu thường xuyên để nâng cao bảo mật. Ngoài ra: 1) Không nên sử dụng chung mật khẩu của email với mật khẩu của các tài khoản khác. 2) Luôn đăng xuất khỏi các tài khoản sau khi sử dụng trên thiết bị công cộng hoặc thiết bị không phải của bản thân.')
INSERT INTO Notification VALUES(2,	N'Thông báo!',		GETDATE(),	0,	N'Từ ngày 26/06/2020, Tiki áp dụng phí vận chuyển dựa trên khối lượng/ kích thước đóng gói của các sản phẩm trong đơn hàng, và khoảng cách giữa địa chỉ nhận hàng và kho của Tiki/nhà cung cấp/nhà bán hàng.')
INSERT INTO Notification VALUES(3,	N'Đổi mật khẩu!',	GETDATE(),	0,	N'Hãy thay đổi mật khẩu thường xuyên để nâng cao bảo mật. Ngoài ra: 1) Không nên sử dụng chung mật khẩu của email với mật khẩu của các tài khoản khác. 2) Luôn đăng xuất khỏi các tài khoản sau khi sử dụng trên thiết bị công cộng hoặc thiết bị không phải của bản thân.')
INSERT INTO Notification VALUES(3,	N'Thông báo!',		GETDATE(),	0,	N'Từ ngày 26/06/2020, Tiki áp dụng phí vận chuyển dựa trên khối lượng/ kích thước đóng gói của các sản phẩm trong đơn hàng, và khoảng cách giữa địa chỉ nhận hàng và kho của Tiki/nhà cung cấp/nhà bán hàng.')
INSERT INTO Notification VALUES(3,	N'Đổi mật khẩu!',	GETDATE(),	0,	N'Hãy thay đổi mật khẩu thường xuyên để nâng cao bảo mật. Ngoài ra: 1) Không nên sử dụng chung mật khẩu của email với mật khẩu của các tài khoản khác. 2) Luôn đăng xuất khỏi các tài khoản sau khi sử dụng trên thiết bị công cộng hoặc thiết bị không phải của bản thân.')
INSERT INTO Notification VALUES(3,	N'Thông báo!',		GETDATE(),	0,	N'Từ ngày 26/06/2020, Tiki áp dụng phí vận chuyển dựa trên khối lượng/ kích thước đóng gói của các sản phẩm trong đơn hàng, và khoảng cách giữa địa chỉ nhận hàng và kho của Tiki/nhà cung cấp/nhà bán hàng.')
INSERT INTO Notification VALUES(4,	N'Đổi mật khẩu!',	GETDATE(),	0,	N'Hãy thay đổi mật khẩu thường xuyên để nâng cao bảo mật. Ngoài ra: 1) Không nên sử dụng chung mật khẩu của email với mật khẩu của các tài khoản khác. 2) Luôn đăng xuất khỏi các tài khoản sau khi sử dụng trên thiết bị công cộng hoặc thiết bị không phải của bản thân.')
INSERT INTO Notification VALUES(4,	N'Thông báo!',		GETDATE(),	0,	N'Từ ngày 26/06/2020, Tiki áp dụng phí vận chuyển dựa trên khối lượng/ kích thước đóng gói của các sản phẩm trong đơn hàng, và khoảng cách giữa địa chỉ nhận hàng và kho của Tiki/nhà cung cấp/nhà bán hàng.')
INSERT INTO Notification VALUES(4,	N'Đổi mật khẩu!',	GETDATE(),	0,	N'Hãy thay đổi mật khẩu thường xuyên để nâng cao bảo mật. Ngoài ra: 1) Không nên sử dụng chung mật khẩu của email với mật khẩu của các tài khoản khác. 2) Luôn đăng xuất khỏi các tài khoản sau khi sử dụng trên thiết bị công cộng hoặc thiết bị không phải của bản thân.')
INSERT INTO Notification VALUES(4,	N'Thông báo!',		GETDATE(),	0,	N'Từ ngày 26/06/2020, Tiki áp dụng phí vận chuyển dựa trên khối lượng/ kích thước đóng gói của các sản phẩm trong đơn hàng, và khoảng cách giữa địa chỉ nhận hàng và kho của Tiki/nhà cung cấp/nhà bán hàng.')
 
-------------------------------------------------------------------------------
-- INSERT DiscountCode
-- happy new year 2021	 
INSERT INTO DiscountCode VALUES('8b607a16e992ad321740bd6aa50ff1ec', 5)
-- hoa nở không màu
INSERT INTO DiscountCode VALUES('6037290199D816C288B5A4941586B98A', 10) 