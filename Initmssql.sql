CREATE DATABASE DapperUnitOfWorkDB
GO
USE DapperUnitOfWorkDB
CREATE TABLE Product
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR (Max) NOT NULL,
    Price INT NOT NULL
)
GO

CREATE TABLE Test
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR (Max) NOT NULL,
    Price INT NOT NULL
)
GO


