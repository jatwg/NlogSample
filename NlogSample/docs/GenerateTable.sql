CREATE TABLE Logs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Timestamp NVARCHAR(30),
    Level NVARCHAR(5),
    Logger NVARCHAR(300),
    Message NVARCHAR(MAX),
    Exception NVARCHAR(MAX)
);