

-- Create Categories table
CREATE TABLE Categories (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    Description nvarchar(500),
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    IsActive bit NOT NULL DEFAULT 1
);

-- Create Products table
CREATE TABLE Products (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    Description nvarchar(500),
    Price decimal(18,2) NOT NULL,
    CategoryId int NOT NULL,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt datetime2 NULL,
    IsActive bit NOT NULL DEFAULT 1,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Insert sample data
INSERT INTO Categories (Name, Description) VALUES 
('Electronics', 'Electronic devices and gadgets'),
('Books', 'Physical and digital books'),
('Clothing', 'Apparel and accessories');

INSERT INTO Products (Name, Description, Price, CategoryId) VALUES 
('Laptop', 'High-performance laptop', 999.99, 1),
('Smartphone', 'Latest smartphone model', 699.99, 1),
('Programming Book', 'Learn programming fundamentals', 49.99, 2),
('T-Shirt', 'Comfortable cotton t-shirt', 19.99, 3);