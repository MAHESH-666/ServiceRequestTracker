1. Overview
Service Request Tracker is a .NET 8 MVC application that allows users to create, update, filter, delete, and export Service Request records.


This project demonstrates:
DB First Approach
EF Core 8
MVC Architecture
PDF Export using Rotativa


2. How to Setup the Database
Step 1 – Run the Provided SQL Script
CREATE DATABASE ServiceTracker;
GO

USE ServiceTracker;
GO

CREATE TABLE ServiceRequests (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    RequesterName NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    Priority NVARCHAR(20) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'New',
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    LastUpdatedOn DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Sample data
INSERT INTO ServiceRequests
    (Title, Description, RequesterName, Department, Priority, Status)
VALUES
('Internet not working',
 'User reports that internet is not working on 3rd floor.',
 'Mahesh', 'IT', 'High', 'New'),
('Printer issue',
 'Printer not printing color pages in cabin 204.',
 'Ravi', 'Maintenance', 'Medium', 'In Progress');
GO

This script will:
✔ Create the database
✔ Create the table
✔ Insert sample test data



3. How to Configure the Connection String
Open appsettings.json:
{
  "ConnectionStrings": {
    "ServiceDB": "Server=AddYourConnectionString;Database=ServiceTracker;Trusted_Connection=True;TrustServerCertificate=True"
  }
}

Program.cs
builder.Services.AddDbContext<ServiceTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceDB")));
	
	
4. .NET / Framework Version Used
.NET 8 (ASP.NET Core MVC)
EF Core 8
C# 12
Rotativa.AspNetCore (PDF)


5. Assumptions / Approaches Used
DB First Approach used for generating the context and models.
Bootstrap 5 used for styling.
jQuery used for SweetAlert delete.
PDF export only exports visible table.
No authentication implemented (assumed not required for assignment).
