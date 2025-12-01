1. Overview<br/>
Service Request Tracker is a .NET 8 MVC application that allows users to create, update, filter, delete, and export Service Request records.<br/>
<br/>

This project demonstrates:<br/>
DB First Approach<br/>
EF Core 8<br/>
MVC Architecture<br/>
PDF Export using Rotativa<br/>
<br/><br/>

2. How to Setup the Database<br/>
Step 1 – Run the Provided SQL Script<br/>
CREATE DATABASE ServiceTracker;<br/>
GO<br/>

USE ServiceTracker;<br/>
GO<br/>
<br/>
CREATE TABLE ServiceRequests (<br/>
    RequestID INT IDENTITY(1,1) PRIMARY KEY, <br/>
    Title NVARCHAR(200) NOT NULL,<br/>
    Description NVARCHAR(MAX) NOT NULL,<br/>
    RequesterName NVARCHAR(100) NOT NULL,<br/>
    Department NVARCHAR(100) NOT NULL,<br/>
    Priority NVARCHAR(20) NOT NULL,<br/>
    Status NVARCHAR(20) NOT NULL DEFAULT 'New',<br/>
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),<br/>
    LastUpdatedOn DATETIME NOT NULL DEFAULT GETDATE()<br/>
);<br/>
GO<br/>
<br/>
-- Sample data<br/>
INSERT INTO ServiceRequests<br/>
    (Title, Description, RequesterName, Department, Priority, Status)<br/>
VALUES<br/>
('Internet not working',<br/>
 'User reports that internet is not working on 3rd floor.',<br/>
 'Mahesh', 'IT', 'High', 'New'),<br/>
('Printer issue',<br/>
 'Printer not printing color pages in cabin 204.',<br/>
 'Ravi', 'Maintenance', 'Medium', 'In Progress');<br/>
GO<br/>
<br/>
This script will:<br/>
✔ Create the database<br/>
✔ Create the table<br/>
✔ Insert sample test data<br/>

<br/><br/>

3. How to Configure the Connection String<br/>
Open appsettings.json:<br/>
{<br/>
  "ConnectionStrings": {<br/>
    "ServiceDB": "Server=AddYourConnectionString;Database=ServiceTracker;Trusted_Connection=True;TrustServerCertificate=True"
  }<br/>
}<br/>

Program.cs<br/>
builder.Services.AddDbContext<ServiceTrackerContext>(options =><br/>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceDB")));<br/><br/>
	
4. .NET / Framework Version Used<br/>
.NET 8 (ASP.NET Core MVC)<br/>
EF Core 8<br/>
C# 12<br/>
Rotativa.AspNetCore (PDF)<br/><br/>

<br/>
5. Assumptions / Approaches Used<br/>
DB First Approach used for generating the context and models.<br/>
Bootstrap 5 used for styling.<br/>
jQuery used for SweetAlert delete.<br/>
PDF export only exports visible table.<br/>
No authentication implemented (assumed not required for assignment).<br/>
