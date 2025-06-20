use allwinproject

-- Create the table
CREATE TABLE Tasks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TaskName NVARCHAR(255) NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT 0
);
GO

-- Stored Procedure: GetAllTasks
CREATE PROCEDURE GetAllTasks
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, TaskName, IsCompleted FROM Tasks;
END
GO

-- Stored Procedure: AddTask
CREATE PROCEDURE AddTask
    @TaskName NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Tasks (TaskName, IsCompleted) VALUES (@TaskName, 0);
END
GO

-- Stored Procedure: ToggleTaskCompletion
CREATE PROCEDURE ToggleTaskCompletion
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Tasks
    SET IsCompleted = CASE WHEN IsCompleted = 1 THEN 0 ELSE 1 END
    WHERE Id = @Id;
END
GO

-- Stored Procedure: DeleteTask
CREATE PROCEDURE DeleteTask
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Tasks WHERE Id = @Id;
END
GO
