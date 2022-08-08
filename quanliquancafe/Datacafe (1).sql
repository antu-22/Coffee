CREATE DATABASE Quanliquancafe
GO

USE Quanliquancafe

GO

-- FOOD
-- TABLEFOOD
-- FOODCATEGORY
-- ACCOUNT
-- BILL
-- BILLINFO

CREATE TABLE TABLEFOOD
(
       id INT IDENTITY PRIMARY KEY,
	   name NVARCHAR(100) NOT NULL DEFAULT N'no info',
	   status NVARCHAR(100) NOT NULL DEFAULT N'Trống'-- Trống || có người
)
GO

CREATE TABLE ACCOUNT
(
	   UserName NVARCHAR(100) PRIMARY KEY,
	   DisplayName NVARCHAR(100) NOT NULL DEFAULT N'T',
	   PassWord NVARCHAR(100) NOT NULL DEFAULT 0,
	   Type INT NOT NULL DEFAULT 0 -- 1: ADMIN && 0: STAFF
)
GO

CREATE TABLE FOODCATEGORY
(
       id INT IDENTITY PRIMARY KEY,
	   name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đạt tên'
)
GO

CREATE TABLE FOOD
(
       id INT IDENTITY PRIMARY KEY,
	   name NVARCHAR(100) NOT NULL DEFAULT N'no info',
	   idCategory INT NOT NULL,
	   price FLOAT NOT NULL  DEFAULT 0

	   FOREIGN KEY (idCategory) REFERENCES dbo.FOODCATEGORY(id)
)
GO

CREATE TABLE BILL
(
      id INT IDENTITY PRIMARY KEY,
	  DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	  DateCheckOut DATE,
	  idTable INT NOT NULL,
	  status INT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán

	  FOREIGN KEY (idTable) REFERENCES dbo.TABLEFOOD(id)
)
GO

CREATE TABLE BILLINFO
(
        id INT IDENTITY PRIMARY KEY,
		idBill INT NOT NULL,
		idFood INT NOT NULL,
		count INT NOT NULL DEFAULT 0

		FOREIGN KEY (idBill) REFERENCES dbo.BILL(id),
		FOREIGN KEY (idFood) REFERENCES dbo.FOOD(id)
)
GO

INSERT INTO dbo.ACCOUNT
       (      
        UserName,
	    DisplayName,
	    PassWord,
	    Type
        )
VALUES  ( N'ManagerC', -- UserName - nvarchar(100)
          N'ManagerC', -- DisplayName - nvarchar(100)
		  N'1', -- PassWord - nvarchar(1000)
		  1 -- Type - int 
        )
INSERT INTO dbo.ACCOUNT
       (      
        UserName,
	    DisplayName,
	    PassWord,
	    Type
        )
VALUES  ( N'staff', -- UserName - nvarchar(100)
          N'staff', -- DisplayName - nvarchar(100)
		  N'1', -- PassWord - nvarchar(1000)
		  0 -- Type - int 
        )
GO

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.ACCOUNT WHERE UserName = @userName
END
GO

EXEC dbo.USP_GetAccountByUserName @userName = N'ManagerC'
GO

CREATE PROC USP_Login

@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.ACCOUNT WHERE UserName = @userName AND PassWord = @passWord
END
GO

-- thêm bàn

DECLARE @i INT = 1

WHILE @i <= 10
BEGIN 
	INSERT dbo.TABLEFOOD (name) VALUES (N'Bàn ' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END

GO

CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TABLEFOOD
GO

UPDATE dbo.TABLEFOOD SET STATUS = N'Có người' WHERE id = 3


EXEC dbo.USP_GetTableList
GO

--Thêm category

INSERT dbo.FOODCATEGORY
	(name)
VALUES (N'Cà phê')

INSERT dbo.FOODCATEGORY
	(name)
VALUES (N'Nước trái cây') 

INSERT dbo.FOODCATEGORY
	(name)
VALUES (N'Trà sữa') 

INSERT dbo.FOODCATEGORY
	(name)
VALUES (N'Nước ngọt')

--thêm món ăn

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Cà phê đen', 1, 30000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Cà phê sữa', 1, 30000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Espresso', 1, 50000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Cappuccino', 1, 55000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Cafe Latte', 1, 55000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Nước ép việt quất', 2, 30000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Sinh tố dưa hấu', 2, 30000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Trà đào', 2, 15000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Trà sữa OREO Cake Cream', 3, 65000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Trà sữa trân châu đường đen', 3, 55000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Trà sữa matcha đậu đỏ', 3, 60000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Trà sữa sương sáo', 3, 50000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Cocacla', 4, 10000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'Pepsi', 4, 10000)

INSERT dbo.FOOD
	(name, idCategory, price)
VALUES (N'7Up', 4, 10000)

--thêm bill

INSERT dbo.BILL
	(DateCheckIn, DateCheckOut, idTable, status)
VALUES (GETDATE(),null,1,0)

INSERT dbo.BILL
	(DateCheckIn, DateCheckOut, idTable, status)
VALUES (GETDATE(),null,2,0)

INSERT dbo.BILL
	(DateCheckIn, DateCheckOut, idTable, status)
VALUES (GETDATE(),GETDATE(),3,1)

--thêm bill info

INSERT dbo.BILLINFO
	(idBill, idFood, count)
VALUES (1,1,2)

INSERT dbo.BILLINFO
	(idBill, idFood, count)
VALUES (1,3,4)

INSERT dbo.BILLINFO
	(idBill, idFood, count)
VALUES (1,9,1)

INSERT dbo.BILLINFO
	(idBill, idFood, count)
VALUES (2,15,2)

INSERT dbo.BILLINFO
	(idBill, idFood, count)
VALUES (2,1,2)

INSERT dbo.BILLINFO
	(idBill, idFood, count)
VALUES (3,1,2)

GO

SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BILLINFO AS bi, dbo.BILL AS b, dbo.FOOD AS f
WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable = 3


SELECT * FROM dbo.BILL
SELECT * FROM dbo.BILLINFO
SELECT * FROM dbo.FOOD
SELECT * FROM dbo.FOODCATEGORY

GO

CREATE PROC USP_InsertBill
@idTable INT 
AS
BEGIN
	INSERT dbo.BILL
		(DateCheckIn, DateCheckOut, idTable, status)
	VALUES (GETDATE(), NULL, @idTable, 0)
END 
GO


CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN
	DECLARE @isExitBillInfo INT;
	DECLARE @foodCount INT = 1

	SELECT @isExitBillInfo = id, @foodCount = b.count FROM dbo.BILLINFO AS b WHERE id = @idBill AND @idFood =idFood
	IF (@isExitBillInfo > 0)

	BEGIN
		DECLARE @newCount  INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BILLINFO SET count = @foodCount + @count WHERE idFood =@idFood
		ELSE
			DELETE dbo.BILLINFO WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE 
	BEGIN
		INSERT dbo.BILLINFO
			(idBill, idFood, count)
	VALUES (@idBill, @idFood, @count)
	END
END
GO




CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BILLINFO FOR INSERT, UPDATE 
AS
BEGIN
	DECLARE @idBill INT 
	SELECT idBill FROM inserted
	DECLARE @idTable INT 
	SELECT @idTable = idTable FROM dbo.BILL WHERE id = @idBill and status = 0
	UPDATE dbo.TABLEFOOD SET status = N'Có người' WHERE id = @idTable
END
GO

CREATE TRIGGER UTG_UpdateBill
ON dbo.BILL FOR INSERT
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill FROM inserted
	DECLARE @idTable INT 
	SELECT @idTable = idTable FROM dbo.BILL WHERE id = @idBill
	DECLARE @count INT = 0
	SELECT count = COUNT (*) FROM dbo.BILL WHERE idTable = @idTable AND status = 0
	if (@count = 0)
		UPDATE dbo.TABLEFOOD SET status =N'Trống' WHERE id = @idTable
END
GO

CREATE PROC USP_UpdatAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT
	SELECT @isRightPass = COUNT (*) FROM dbo.ACCOUNT WHERE UserName = @userName AND PassWord =@password
	IF (@isRightPass = 1 )
	BEGIN
		IF (@newPassword = NULL OR @newPassword ='')
		BEGIN
			UPDATE dbo.ACCOUNT SET DisplayName=@displayName WHERE UserName = @userName
		END
		ELSE 
			UPDATE dbo.ACCOUNT SET DisplayName=@displayName, PassWord=@newPassword WHERE UserName = @userName
			
			
	END
END
GO
select * from Food