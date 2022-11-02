CREATE DATABASE BK;

USE BK;

CREATE TABLE Scopes(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    AllowedRoute VARCHAR(255) NOT NULL,
)

-- Thiết lập permission của từng scope
CREATE TABLE Permissions(
   Id INT IDENTITY(1,1) PRIMARY KEY,
   Resource NVARCHAR(255) NOT NULL,
   Url VARCHAR(255) NOT NULL,
   Method VARCHAR(10) NOT NULL,
   Action NVARCHAR(255) NOT NULL,
   Filter NVARCHAR(500) NOT NULL DEFAULT '',
   FilterValue NVARCHAR(1000) NOT NULL DEFAULT ''
);

CREATE TABLE Scope_Permission(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Allowed VARCHAR(100) NOT NULL,
    Filter NVARCHAR(500) NOT NULL DEFAULT '',

    Permission_Id INT FOREIGN KEY REFERENCES Permissions(Id),
    Scope_Id INT FOREIGN KEY REFERENCES Scopes(Id),
)

-- Một tài khoản có một scope
CREATE TABLE Users(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Status BIT NOT NULL DEFAULT 1,

    Scope_Id INT FOREIGN KEY REFERENCES Scopes(Id),
)

CREATE TABLE Units(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
)

-- Một tài khoản có thể quản lý nhiều đơn vị
CREATE TABLE User_Unit(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    User_Id INT NOT NULL,
    Unit_Id INT NOT NULL,

    FOREIGN KEY(User_Id) REFERENCES Users(Id),
    FOREIGN KEY(Unit_Id) REFERENCES Units(Id),
)

-- Bảng lưu trữ RFID

CREATE TABLE RFIDs(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CardNumber VARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    ActivationTime DATE DEFAULT NULL,
    Type int NOT NULL,
    IsDistributed BIT NOT NULL DEFAULT 0,
    Status BIT NOT NULL DEFAULT 1,

    UnitId INT FOREIGN KEY REFERENCES Units(Id),
);

-- Bảng lưu trữ thông tin lái xe

CREATE TABLE Drivers(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Avatar NVARCHAR(255),
    EmployeeCode VARCHAR(100) NOT NULL,
    NAME NVARCHAR(50) NOT NULL,
    Sex VARCHAR(100) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Email VARCHAR(50),
    Status BIT NOT NULL DEFAULT 1,

    UnitId INT FOREIGN KEY REFERENCES Units(Id),
    RFIDId INT FOREIGN KEY REFERENCES RFIDs(Id),
)

-- Bảng lưu trữ thông tin kỹ thuật viên ATM
CREATE TABLE ATMTechnicans(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Avatar NVARCHAR(255),
    EmployeeCode VARCHAR(100) NOT NULL,
    NAME NVARCHAR(50) NOT NULL,
    Sex VARCHAR(100) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Email VARCHAR(50),
    Status BIT NOT NULL DEFAULT 1,

    UnitId INT FOREIGN KEY REFERENCES Units(Id),
    RFIDId INT FOREIGN KEY REFERENCES RFIDs(Id),
)

-- Bảng lưu trữ thông tin chủ hàng, thủ quỹ
CREATE TABLE Treasurer(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Avatar NVARCHAR(255),
    EmployeeCode VARCHAR(100) NOT NULL,
    NAME NVARCHAR(50) NOT NULL,
    Sex VARCHAR(100) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Email VARCHAR(50),
    Status BIT NOT NULL DEFAULT 1,

    UnitId INT FOREIGN KEY REFERENCES Units(Id),
    RFIDId INT FOREIGN KEY REFERENCES RFIDs(Id),
)

-- Bảng lưu trữ thông tin điểm giao dịch

CREATE TABLE TransactionPoints(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PointCode VARCHAR(100) NOT NULL,
    PointName NVARCHAR(255) NOT NULL,
    PointType int NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Longtitude FLOAT NOT NULL,
    Latitude FLOAT NOT NULL,
    Branch NVARCHAR(255),
    Contact NVARCHAR(50),
    Phone NVARCHAR(20),
    Fax NVARCHAR(50),

    UnitId INT FOREIGN KEY REFERENCES Units(Id),
);

--Thông tin xe
-- FirstCamPo : vị trí camera thứ nhất, 0: khoang lái, 1: khoang két
CREATE TABLE Cars(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    LicensePlate VARCHAR(30) NOT NULL,
    Type VARCHAR(50) NOT NULL,
    NumberCamera INT NOT NULL,
    FirstCamPo INT DEFAULT 0,
    FirstCamRotation INT,
    SecondCamRotation INT,
    fuel INT,
    LimitedSpeed INT,

    Unit_Id INT FOREIGN KEY REFERENCES Units(Id),
    RFID_Id INT FOREIGN KEY REFERENCES RFIDs(Id),
    Driver_Id INT FOREIGN KEY REFERENCES Drivers(Id),
)
CREATE TABLE Devices (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DeviceNumber VARCHAR(50) NOT NULL,
    IMEI VARCHAR(50) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    MobileCarrier INT NOT NULL,
    ActivationTime DATE DEFAULT NULL,
    Status BIT DEFAULT 1,
    AllowUpdate BIT DEFAULT 1,

    Unit_Id INT FOREIGN KEY REFERENCES Units(Id),
    Car_Id INT FOREIGN KEY REFERENCES Cars(Id),
);

CREATE TABLE SampleRoutes(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RouteCode VARCHAR(50) NOT NULL,
    Type INT NOT NULL,
    Route VARCHAR(1000) NOT NULL,
    Direction NVARCHAR(MAX) NOT NULL,
    WayBack NVARCHAR(MAX) NOT NULL,
    AutoTurnBack BIT NOT NULL DEFAULT 0,
    BeginTime TIME NOT NULL,
    OverTimeAllowed INT NOT NULL,
    ToleranceAllowed INT NOT NULL,
    Distance FLOAT NOT NULL,
    ArrivalTime INT NOT NULL,
    Permanent BIT NOT NULL DEFAULT 1,

    Unit_Id INT FOREIGN KEY REFERENCES Units(Id),
)

CREATE TABLE Segmentations(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BeginTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    BeginDate Date NOT NULL,
    EndDate Date NOT NULL,
    Day VARCHAR(20) NOT NULL,
    Control BIT NOT NULL DEFAULT 1,
    Sms BIT NOT NULL DEFAULT 1,

    Unit_Id INT FOREIGN KEY REFERENCES Units(Id),
    Car_Id INT FOREIGN KEY REFERENCES Cars(Id),
    Driver_Id INT FOREIGN KEY REFERENCES Drivers(Id),
    Route_Id INT FOREIGN KEY REFERENCES SampleRoutes(Id),
    Treasurer_Id INT FOREIGN KEY REFERENCES Treasurer(Id),
    ATMTechnican_Id INT FOREIGN KEY REFERENCES ATMTechnicans(Id),
)

CREATE TABLE Onlines(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Cam1ImgPath nvarchar(256) ,
	Cam2ImgPath nvarchar(256) ,
	RFIDString varchar(256) ,
	AppVersion varchar(50) ,
	ReceivedTime datetime ,
	DeviceTime datetime ,
	EngineOn int,
	StrongBoxOpen bit ,
	IsSos bit ,
	GpsLat float ,
	GpsLon float ,
	NetworkLat float ,
	NetworkLon float ,
	GpsVelocity int ,

    CarId INT FOREIGN KEY REFERENCES Cars(Id),
)

CREATE TABLE Histories(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Cam1ImgPath nvarchar(256) ,
	Cam2ImgPath nvarchar(256) ,
	RFIDString varchar(256) ,
	AppVersion varchar(50) ,
	ReceivedTime datetime ,
	DeviceTime datetime ,
	EngineOn int,
	StrongBoxOpen bit ,
	IsSos bit ,
	GpsLat float ,
	GpsLon float ,
	NetworkLat float ,
	NetworkLon float ,
	GpsVelocity int ,

    CarId INT FOREIGN KEY REFERENCES Cars(Id),
)

CREATE TABLE EditedSegmentationRoutes(
    Id int IDENTITY(1,1) PRIMARY KEY,
    Route VARCHAR(1000) NOT NULL,
    Direction NVARCHAR(MAX) NOT NULL,
    WayBack NVARCHAR(MAX) NOT NULL,
    AutoTurnBack BIT NOT NULL DEFAULT 0,
    EditedIn Date NOT NULL,

    SegmentationId INT FOREIGN KEY REFERENCES Segmentations(Id),
)

CREATE TABLE DailyCar(
    Id int IDENTITY(1,1) PRIMARY KEY,
    TotalKm FLOAT,
    TotalFuel INT,
    RunningTime INT, 
    OpenSafeBox INT,
    Conflict INT, 
    SegmentationId INT,
    RouteDeviation INT,
    TimeDeviation INT,
    ReportTime DATETIME,
    CarLicensePlate VARCHAR(50),
    RouteCode VARCHAR(10),

    CarId INT FOREIGN KEY REFERENCES Cars(Id),
    RouteId INT FOREIGN KEY REFERENCES SampleRoutes(Id)
)

CREATE TABLE DailyKmCar(
    Id int IDENTITY(1,1) PRIMARY KEY,
    Driver NVARCHAR(255),
    Treasure NVARCHAR(255),
    TotalKm FLOAT,
    ReportTime DATETIME,
    UnitName NVARCHAR(20),
    CarLicensePlate VARCHAR(50),

    UnitId INT FOREIGN KEY REFERENCES Units(Id),
    CarId INT FOREIGN KEY REFERENCES Cars(Id),
)

CREATE INDEX idx_deviceTime
ON dbo.Histories (DeviceTime);

-- Scaffold-DbContext "Server=127.0.0.1;Database=BK;Trusted_Connection=False;User Id=sa;Password=Boladinh99.;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f
 DROP DATABASE BK;

-- Scaffold-DbContext "Server=localhost;port=1433;Database=BK;Trusted_Connection=False;User Id=sa;Password=Boladinh99.;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
-- Muốn scaffold thì phải build thử project xem không có lỗi thì mới scaffold được