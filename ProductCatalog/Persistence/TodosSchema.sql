
IF OBJECT_ID('dbo.Todos', 'U') IS NOT NULL
    DROP TABLE dbo.Todos;
GO

CREATE TABLE Todos (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    Priority NVARCHAR(20) NOT NULL DEFAULT 'Medium',
    Category NVARCHAR(100) NULL,
    DueDate DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
GO


INSERT INTO Todos (Title, Description, IsCompleted, Priority, Category, DueDate)
VALUES 
    ('Complete project documentation', 'Write comprehensive documentation for the new project', 0, 'High', 'Work', DATEADD(day, 3, GETUTCDATE())),
    ('Buy groceries', 'Milk, bread, eggs, and vegetables', 0, 'Medium', 'Personal', DATEADD(day, 1, GETUTCDATE())),
    ('Call dentist for appointment', 'Schedule regular checkup appointment', 0, 'Low', 'Health', DATEADD(day, 7, GETUTCDATE())),
    ('Review code changes', 'Review pull requests from team members', 1, 'High', 'Work', GETUTCDATE()),
    ('Exercise for 30 minutes', 'Go for a run or do home workout', 1, 'Medium', 'Health', GETUTCDATE()),
    ('Plan weekend trip', 'Research destinations and book accommodation', 0, 'Low', 'Personal', DATEADD(day, 5, GETUTCDATE())),
    ('Fix authentication bug', 'Investigate and fix the login issue reported by users', 0, 'High', 'Work', DATEADD(day, -1, GETUTCDATE())),
    ('Read technical article', 'Read about clean architecture patterns', 0, 'Low', 'Learning', DATEADD(day, 10, GETUTCDATE())),
    ('Update resume', 'Add recent projects and skills to resume', 1, 'Medium', 'Career', GETUTCDATE()),
    ('Clean up workspace', 'Organize desk and computer files', 0, 'Low', 'Personal', GETUTCDATE());
GO

-- Verify the schema
SELECT 
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Todos'
ORDER BY ORDINAL_POSITION;
GO

-- Show sample data
SELECT TOP 5 * FROM Todos ORDER BY CreatedAt DESC;
GO

