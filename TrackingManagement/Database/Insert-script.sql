USE BK;

-- Tạo scope
INSERT INTO dbo.Scopes (Name, AllowedRoute) VALUES ('admin', '["/","/histories","/drivers","/treasures","atm-teachnicans","/cars","/devices","/rfids","/transaction-points","/sample-routes","/segmentations","/reports","/users","/permissions","/units"]')
INSERT INTO dbo.Scopes (Name, AllowedRoute) VALUES ('operator', '["/","/histories","/drivers","/treasures","atm-teachnicans","/cars","/devices","/rfids","/transaction-points","/sample-routes","/segmentations","/reports","/users","/permissions","/units"]')
INSERT INTO dbo.Scopes (Name, AllowedRoute) VALUES ('maintenance', '["/","/histories","/drivers","/treasures","atm-teachnicans","/cars","/devices","/rfids","/transaction-points","/sample-routes","/segmentations","/reports","/users","/permissions","/units"]')
SELECT * FROM dbo.Scopes

-- Thêm các permisison
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Report","displayName":"Báo cáo"}', '/api/reports', 'POST', N'Xuất báo cáo', 'body',N'[{"field":"reportType","displayName":"Loại báo cáo","value":[{"valueName":"ReportDriver","description":"Báo cáo lái xe"},{"valueName":"ReportCar","description":"Báo cáo xe"},{"valueName":"ReportKmCar","description":"Báo cáo Km theo xe"}]}]')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Report","displayName":"Báo cáo"}', '/api/reports', 'GET', N'Xem báo cáo', 'query',N'[{"field":"reportType","displayName":"Loại báo cáo","value":[{"valueName":"ReportDriver","description":"Báo cáo lái xe"},{"valueName":"ReportCar","description":"Báo cáo xe"},{"valueName":"ReportKmCar","description":"Báo cáo Km theo xe"}]}]')

INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Car","displayName":"Xe"}', '/api/cars', 'GET', N'Xem danh sách xe', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Car","displayName":"Xe"}', '/api/cars', 'POST', N'Thêm xe mới', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Car","displayName":"Xe"}', '/api/cars', 'PUT', N'Cập nhật thông tin xe', '',N'')

INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Rfid","displayName":"Rfid"}', '/api/rfids', 'GET', N'Xem danh sách thẻ RFID', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Rfid","displayName":"Rfid"}', '/api/rfids', 'POST', N'Thêm thẻ RFID', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Rfid","displayName":"Rfid"}', '/api/rfids', 'PUT', N'Cập nhật thông thẻ RFID', '',N'')

INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Driver","displayName":"Lái xe"}', '/api/members/drivers', 'GET', N'Xem danh sách lái xe', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Driver","displayName":"Lái xe"}', '/api/members/drivers', 'POST', N'Thêm lái xe mới', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Driver","displayName":"Lái xe"}', '/api/members/drivers', 'PUT', N'Cập nhật thông tin lái xe', '',N'')

INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Treasure","displayName":"Chủ hàng"}', '/api/members/treasures', 'GET', N'Xem danh sách chủ hàng', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Treasure","displayName":"Chủ hàng"}', '/api/members/treasures', 'POST', N'Thêm chủ hàng mới', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Treasure","displayName":"Chủ hàng"}', '/api/members/treasures', 'PUT', N'Cập nhật thông tin chủ hàng', '',N'')

INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"ATM-Technican","displayName":"Kỹ thuật viên ATM"}', '/api/members/atm-technicans', 'GET', N'Xem danh sách kỹ thuật viên ATM', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"ATM-Technican","displayName":"Kỹ thuật viên ATM"}', '/api/members/atm-technicans', 'POST', N'Thêm kỹ thuật viên ATM mới', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"ATM-Technican","displayName":"Kỹ thuật viên ATM"}', '/api/members/atm-technicans', 'PUT', N'Cập nhật thông tin kỹ thuật viên ATM', '',N'')

INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Route","displayName":"Tuyến mẫu"}', '/api/routes', 'GET', N'Xem danh sách tuyến mẫu', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Route","displayName":"Tuyến mẫu"}', '/api/routes', 'POST', N'Thêm tuyến mẫu mới', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Route","displayName":"Tuyến mẫu"}', '/api/routes', 'PUT', N'Cập nhật thông tin tuyến mẫu', '',N'')

INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Transaction Point","displayName":"Điểm giao dịch"}', '/api/transaction-points', 'GET', N'Xem danh sách điểm giao dịch', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Transaction Point","displayName":"Điểm giao dịch"}', '/api/transaction-points', 'POST', N'Thêm điểm giao dịch mới', '',N'')
INSERT INTO dbo.Permissions (Resource, Url, Method, Action,Filter, FilterValue) VALUES (N'{"name":"Transaction Point","displayName":"Điểm giao dịch"}', '/api/transaction-points', 'PUT', N'Cập nhật thông tin điểm giao dịch', '',N'')

-- Thêm các scope permission
-- admin
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'[{"field":"reportType","displayName":"Loại báo cáo","value":[{"valueName":"ReportDriver","description":"Báo cáo lái xe"},{"valueName":"ReportCar","description":"Báo cáo xe"},{"valueName":"ReportKmCar","description":"Báo cáo Km theo xe"}]}]',1,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'[{"field":"reportType","displayName":"Loại báo cáo","value":[{"valueName":"ReportDriver","description":"Báo cáo lái xe"},{"valueName":"ReportCar","description":"Báo cáo xe"},{"valueName":"ReportKmCar","description":"Báo cáo Km theo xe"}]}]' ,2,1)

INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,3,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,4,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,5,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,6,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,7,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,8,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,9,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,10,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,11,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,12,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,13,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,14,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,15,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,16,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,17,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,18,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,19,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,20,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,21,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,22,1)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,23,1)

-- operator
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'[{"field":"reportType","displayName":"Loại báo cáo","value":[{"valueName":"ReportDriver","description":"Báo cáo lái xe"},{"valueName":"ReportCar","description":"Báo cáo xe"}]}]',1,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'[{"field":"reportType","displayName":"Loại báo cáo","value":[{"valueName":"ReportDriver","description":"Báo cáo lái xe"},{"valueName":"ReportCar","description":"Báo cáo xe"}]}]' ,2,2)

INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,3,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,4,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,5,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,6,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,7,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,8,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,9,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,10,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,11,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,12,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,13,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,14,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,15,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,16,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,17,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,18,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,19,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,20,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,21,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,22,2)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,23,2)

-- mainternance
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'[{"field":"reportType","displayName":"Loại báo cáo","value":[{"valueName":"ReportCar","description":"Báo cáo xe"},{"valueName":"ReportKmCar","description":"Báo cáo Km theo xe"}]}]',1,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'[{"field":"reportType","displayName":"Loại báo cáo","value":[{"valueName":"ReportCar","description":"Báo cáo xe"},{"valueName":"ReportKmCar","description":"Báo cáo Km theo xe"}]}]' ,2,3)

INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,3,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,4,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,5,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,6,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,7,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,8,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,9,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,10,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,11,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,12,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,13,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,14,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,15,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,16,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,17,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,18,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('false',N'' ,19,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,20,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,21,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,22,3)
INSERT INTO dbo.Scope_Permission (Allowed, Filter, Permission_Id, Scope_Id) VALUES ('true',N'' ,23,3)

SELECT * FROM Scope_Permission

-- Thêm các đơn vị
INSERT INTO dbo.Units (Name) VALUES (N'Hà Nội')
INSERT INTO dbo.Units (Name) VALUES (N'HCM')
INSERT INTO dbo.Units (Name) VALUES (N'Đà Nẵng')
INSERT INTO dbo.Units (Name) VALUES (N'Bách Khoa')
SELECT * FROM dbo.Users

select * from Scopes

UPDATE [dbo].[Scopes]
SET
    [AllowedRoute] = '["/","/histories", "/drivers","/treasurers","/atm-technicans","/cars","/devices","/rfids","/transaction-points","/sample-routes","/segmentations","/reports","/users","/permissions","/units"]'
WHERE Id= 3
-- GO

-- Insert JSON to sqlserver
-- insert to docker: docker cp sample_route.xml aeceeccb8e72:/tmp/sample_route.xml

-- drivers
INSERT INTO dbo.Drivers (Avatar, EmployeeCode, NAME, Sex, Phone, Email, Status,UnitId)
SELECT
   MY_XML.row.query('Avatar').value('.', 'NVARCHAR(255)'),
   MY_XML.row.query('EmployeeCode').value('.', 'VARCHAR(100)'),
   MY_XML.row.query('NAME').value('.', 'NVARCHAR(50)'),
   MY_XML.row.query('Sex').value('.', 'VARCHAR(100)'),
   MY_XML.row.query('Phone').value('.', 'VARCHAR(20)'),
   MY_XML.row.query('Email').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('Status').value('.', 'BIT'),
   MY_XML.row.query('UnitId').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/people.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

-- cars

INSERT INTO dbo.Cars (LicensePlate, Type, NumberCamera, FirstCamPo, FirstCamRotation, SecondCamRotation, fuel,LimitedSpeed,Unit_Id,Driver_Id)
SELECT
   MY_XML.row.query('LicensePlate').value('.', 'VARCHAR(30)'),
   MY_XML.row.query('Type').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('NumberCamera').value('.', 'INT'),
   MY_XML.row.query('FirstCamPo').value('.', 'INT'),
   MY_XML.row.query('FirstCamRotation').value('.', 'INT'),
   MY_XML.row.query('SecondCamRotation').value('.', 'INT'),
   MY_XML.row.query('fuel').value('.', 'INT'),
   MY_XML.row.query('LimitedSpeed').value('.', 'INT'),
   MY_XML.row.query('Unit_Id').value('.', 'INT'),
   MY_XML.row.query('Driver_Id').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/car.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

-- online
INSERT INTO dbo.Onlines (Cam1ImgPath, Cam2ImgPath, RFIDString, AppVersion, ReceivedTime, DeviceTime, EngineOn, StrongBoxOpen,IsSos, GpsLat,GpsLon, NetworkLat, NetworkLon, GpsVelocity, CarId)
SELECT
   MY_XML.point.query('Cam1ImgPath').value('.', 'NVARCHAR(256)'),
   MY_XML.point.query('Cam2ImgPath').value('.', 'NVARCHAR(256)'),
   MY_XML.point.query('RFIDString').value('.', 'NVARCHAR(256)'),
   MY_XML.point.query('AppVersion').value('.', 'NVARCHAR(50)'),
   MY_XML.point.query('ReceivedTime').value('.', 'datetime'),
   MY_XML.point.query('DeviceTime').value('.', 'datetime'),
   MY_XML.point.query('EngineOn').value('.', 'int'),
   MY_XML.point.query('StrongBoxOpen').value('.', 'bit'),
   MY_XML.point.query('IsSos').value('.', 'bit'),
   MY_XML.point.query('GpsLat').value('.', 'float'),
   MY_XML.point.query('GpsLon').value('.', 'float'),
   MY_XML.point.query('NetworkLat').value('.', 'float'),
   MY_XML.point.query('NetworkLon').value('.', 'float'),
   MY_XML.point.query('GpsVelocity').value('.', 'INT'),
   MY_XML.point.query('CarId').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/online.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(point);

-- user
INSERT INTO dbo.Users (UserName, Password, Status, Scope_Id)
SELECT
   MY_XML.point.query('UserName').value('.', 'VARCHAR(255)'),
   MY_XML.point.query('Password').value('.', 'VARCHAR(255)'),
   MY_XML.point.query('Status').value('.', 'BIT'),
   MY_XML.point.query('Scope_Id').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/user.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(point);

-- user unit
INSERT INTO dbo.User_Unit (User_Id, Unit_Id)
SELECT
   MY_XML.point.query('User_Id').value('.', 'INT'),
   MY_XML.point.query('Unit_Id').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/user_unit.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(point);

-- history
DECLARE @JSON VARCHAR(MAX)
SELECT @JSON = BulkColumn
FROM OPENROWSET (BULK '/tmp/history.json', SINGLE_CLOB) import
INSERT INTO dbo.Histories
SELECT * 
FROM OPENJSON(@JSON)
WITH  (
	[Cam1ImgPath] [nvarchar](256),
	[Cam2ImgPath] [nvarchar](256),
	[RFIDString] [varchar](256),
	[AppVersion] [varchar](50),
	[ReceivedTime] [datetime],
	[DeviceTime] [datetime],
	[EngineOn] [int],
	[StrongBoxOpen] [bit],
	[IsSos] [bit],
	[GpsLat] [float],
	[GpsLon] [float],
	[NetworkLat] [float],
	[NetworkLon] [float],
	[GpsVelocity] [int],
	[CarId] [int]
)

-- transaction point
INSERT INTO dbo.TransactionPoints (PointCode, PointName, PointType, Address, Longtitude, Latitude, Branch, Contact,Phone, Fax,UnitId)
SELECT
   MY_XML.point.query('PointCode').value('.', 'VARCHAR(100)'),
   MY_XML.point.query('PointName').value('.', 'NVARCHAR(255)'),
   MY_XML.point.query('PointType').value('.', 'INT'),
   MY_XML.point.query('Address').value('.', 'NVARCHAR(255)'),
   MY_XML.point.query('Longtitude').value('.', 'FLOAT'),
   MY_XML.point.query('Latitude').value('.', 'FLOAT'),
   MY_XML.point.query('Branch').value('.', 'NVARCHAR(255)'),
   MY_XML.point.query('Contact').value('.', 'NVARCHAR(50)'),
   MY_XML.point.query('Phone').value('.', 'NVARCHAR(20)'),
   MY_XML.point.query('Fax').value('.', 'NVARCHAR(50)'),
   MY_XML.point.query('UnitId').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/transaction_point.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(point);


SELECT * FROM dbo.DailyCar
-- sample route
INSERT INTO dbo.SampleRoutes (RouteCode, Type, Route, Direction, WayBack,AutoTurnBack, BeginTime, OverTimeAllowed, ToleranceAllowed, Distance, ArrivalTime, Permanent, Unit_Id)
SELECT
   MY_XML.row.query('RouteCode').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('Type').value('.', 'INT'),
   MY_XML.row.query('Route').value('.', 'VARCHAR(1000)'),
   MY_XML.row.query('Direction').value('.', 'NVARCHAR(MAX)'),
   MY_XML.row.query('WayBack').value('.', 'NVARCHAR(MAX)'),
   MY_XML.row.query('AutoTurnBack').value('.', 'BIT'),
   MY_XML.row.query('BeginTime').value('.', 'TIME'),
   MY_XML.row.query('OverTimeAllowed').value('.', 'INT'),
   MY_XML.row.query('ToleranceAllowed').value('.', 'INT'),
   MY_XML.row.query('Distance').value('.', 'FLOAT'),
   MY_XML.row.query('ArrivalTime').value('.', 'INT'),
   MY_XML.row.query('Permanent').value('.', 'INT'),
   MY_XML.row.query('Unit_Id').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/sample_route.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

-- daily car
INSERT INTO dbo.DailyCar (TotalKm, TotalFuel, RunningTime, OpenSafeBox, Conflict, SegmentationId, RouteDeviation,TimeDeviation, ReportTime, CarLicensePlate, RouteCode, CarId, RouteId)
SELECT
   MY_XML.row.query('TotalKm').value('.', 'FLOAT'),
   MY_XML.row.query('TotalFuel').value('.', 'INT'),
   MY_XML.row.query('RunningTime').value('.', 'FLOAT'),
   MY_XML.row.query('OpenSafeBox').value('.', 'INT'),
   MY_XML.row.query('Conflict').value('.', 'INT'),
   MY_XML.row.query('SegmentationId').value('.', 'INT'),
   MY_XML.row.query('RouteDeviation').value('.', 'INT'),
   MY_XML.row.query('TimeDeviation').value('.', 'INT'),
   MY_XML.row.query('ReportTime').value('.', 'DATETIME'),
   MY_XML.row.query('CarLicensePlate').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('RouteCode').value('.', 'VARCHAR(10)'),
   MY_XML.row.query('CarId').value('.', 'INT'),
   MY_XML.row.query('RouteId').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/daily_car.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

SELECT * FROM dbo.TransactionPoints

-- daily_km_car
INSERT INTO dbo.DailyKmCar (Driver, Treasure, TotalKm, ReportTime, UnitName, CarLicensePlate, UnitId, CarId)
SELECT
   MY_XML.row.query('Driver').value('.', 'NVARCHAR(255)'),
   MY_XML.row.query('Treasure').value('.', 'NVARCHAR(255)'),
   MY_XML.row.query('TotalKm').value('.', 'FLOAT'),
   MY_XML.row.query('ReportTime').value('.', 'DATETIME'),
   MY_XML.row.query('UnitName').value('.', 'NVARCHAR(20)'),
   MY_XML.row.query('CarLicensePlate').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('UnitId').value('.', 'INT'),
   MY_XML.row.query('CarId').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/daily_km_car.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

-- SET IDENTITY_INSERT dbo.Histories OFF

-- SELECT * FROM DaiLyCar
-- INNER JOIN Cars ON DaiLyCar.CarId = Cars.Id
-- INNER JOIN SampleRoutes ON DaiLyCar.RouteId = SampleRoutes.Id
-- Where ReportTime >= '2022-06-01 00:00:00' AND ReportTime <= '2022-07-09 00:00:00'
-- AND CarId IN (1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21)


-- people

INSERT INTO dbo.Treasurer (Avatar, EmployeeCode, NAME, Sex, Phone, Email, Status,UnitId)
SELECT
   MY_XML.row.query('Avatar').value('.', 'NVARCHAR(255)'),
   MY_XML.row.query('EmployeeCode').value('.', 'VARCHAR(100)'),
   MY_XML.row.query('NAME').value('.', 'NVARCHAR(50)'),
   MY_XML.row.query('Sex').value('.', 'VARCHAR(100)'),
   MY_XML.row.query('Phone').value('.', 'VARCHAR(20)'),
   MY_XML.row.query('Email').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('Status').value('.', 'BIT'),
   MY_XML.row.query('UnitId').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/people.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

INSERT INTO dbo.ATMTechnicans (Avatar, EmployeeCode, NAME, Sex, Phone, Email, Status,UnitId)
SELECT
   MY_XML.row.query('Avatar').value('.', 'NVARCHAR(255)'),
   MY_XML.row.query('EmployeeCode').value('.', 'VARCHAR(100)'),
   MY_XML.row.query('NAME').value('.', 'NVARCHAR(50)'),
   MY_XML.row.query('Sex').value('.', 'VARCHAR(100)'),
   MY_XML.row.query('Phone').value('.', 'VARCHAR(20)'),
   MY_XML.row.query('Email').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('Status').value('.', 'BIT'),
   MY_XML.row.query('UnitId').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/people.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

-- device

INSERT INTO dbo.Devices (DeviceNumber, IMEI, Phone, MobileCarrier, ActivationTime, Status, AllowUpdate,Unit_Id,Car_Id)
SELECT
   MY_XML.row.query('DeviceNumber').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('IMEI').value('.', 'VARCHAR(50)'),
   MY_XML.row.query('Phone').value('.', 'VARCHAR(15)'),
   MY_XML.row.query('MobileCarrier').value('.', 'INT'),
   MY_XML.row.query('ActivationTime').value('.', 'DATE'),
   MY_XML.row.query('Status').value('.', 'BIT'),
   MY_XML.row.query('AllowUpdate').value('.', 'BIT'),
   MY_XML.row.query('Unit_Id').value('.', 'INT'),
   MY_XML.row.query('Car_Id').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/device.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

-- rfid

INSERT INTO dbo.RFIDs (CardNumber, Description, ActivationTime, Type, IsDistributed, Status, UnitId)
SELECT
   MY_XML.row.query('CardNumber').value('.', 'VARCHAR(100)'),
   MY_XML.row.query('Description').value('.', 'NVARCHAR(255)'),
   MY_XML.row.query('ActivationTime').value('.', 'DAte'),
   MY_XML.row.query('Type').value('.', 'INT'),
   MY_XML.row.query('IsDistributed').value('.', 'BIT'),
   MY_XML.row.query('Status').value('.', 'BIT'),
   MY_XML.row.query('UnitId').value('.', 'INT')
FROM (SELECT CAST(MY_XML AS xml)
      FROM OPENROWSET(BULK '/tmp/rfid.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
      CROSS APPLY MY_XML.nodes('data/row') AS MY_XML(row);

SELECT * FROM Scopes