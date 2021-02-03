CREATE TABLE [dbo].[Customers] (
	CustomerId [INT] IDENTITY(1,1) NOT NULL,
	CustomerName [NVARCHAR](100) NOT NULL,
	CustomerEmail [NVARCHAR](150) NOT NULL,
	CustomerMobile [VARCHAR](15) NOT NUll,

	CONSTRAINT PK_Customers_CustomerId PRIMARY KEY (CustomerId)
);

CREATE TABLE [dbo].[Orders] (
	OrderID [INT] IDENTITY (1,1) NOT NULL,
	CustomerID [INT] NOT NULL,
	OrderDate [DATETIME2] NOT NULL, 
	StudentName [CHAR] (100) NOT NULL,
	ClassName [CHAR] (100) NOT NULL,
	DeliveryOption [CHAR] (100) NOT NULL,
	Paid [BIT] NOT NULL,

	CONSTRAINT PK_Orders_OrderId PRIMARY KEY (OrderId),

	CONSTRAINT FK_Orders_CustomerId FOREIGN KEY (CustomerId)
		REFERENCES [dbo].[Customers](CustomerId)
);

CREATE TABLE [dbo].[OrderItem] (
	OrderItemID [INT] IDENTITY (1,1) NOT NULL,
	OrderId [INT] NOT NULL,
	UniformID [INT] NOT NULL,
	Quantity [INT] NOT NULL CHECK (Quantity<0),

	CONSTRAINT FK_OrderItem_OrderId FOREIGN KEY (OrderId)
		REFERENCES [dbo].[Orders](OrderId),

	CONSTRAINT FK_OrderItem_UniformId FOREIGN KEY (UniformId)
		REFERENCES [dbo].[Customers](CustomerId)
);

CREATE TABLE [dbo].[Uniforms] (
	UniformId [INT] IDENTITY (1,1) NOT NULL,
	Description [NVARCHAR] (225) NOT NULL,
	Size [NVARCHAR] (15) NOT NULL,
	Price [DECIMAL] (2) NOT NULL CHECK (Price<0),
	StockLevel [INT] NOT NULL CHECK (StockLevel<0),
	Campus [VARCHAR] (50) NOT NULL,
	Barcode [VARCHAR] (20) NOT NULL,
	VendorBarcode [VARCHAR] (20) NOT NULL,
	
	CONSTRAINT FK_Uniforms_UniformId PRIMARY KEY (UniformId)
);