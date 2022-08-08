CREATE DATABASE Quanliquancafe
GO

USE Quanliquancafe
GO

-- FOOD
-- TABLEFOOD
-- FOODCATEGORY'
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

