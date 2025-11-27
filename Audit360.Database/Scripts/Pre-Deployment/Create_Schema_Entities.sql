USE [Audit360];
GO

/*
  Tablas principales:
  - Users
  - Roles
  - Audits
  - Responsibles
  - FindingTypes
  - FindingSeverities
  - Findings
  - FollowUpStatuses
  - FollowUps
  - AuditStatuses
*/

-- Roles
IF OBJECT_ID('dbo.Roles', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Roles
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(300) NULL
    );
END
GO

-- Users
IF OBJECT_ID('dbo.Users', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL,
        Email NVARCHAR(200) NOT NULL,
        PasswordHash NVARCHAR(MAX) NOT NULL,
        FullName NVARCHAR(200) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        RoleId INT NOT NULL,
        CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles(Id)
    );
    -- Unique index on Email (matches migrations)
    CREATE UNIQUE INDEX IX_Users_Email ON dbo.Users(Email);
END
GO

-- AuditStatuses
IF OBJECT_ID('dbo.AuditStatuses', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AuditStatuses
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Description NVARCHAR(100) NOT NULL
    );
END
GO

-- Audits
IF OBJECT_ID('dbo.Audits', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Audits
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Area NVARCHAR(100) NOT NULL,
        StartDate DATETIME2 NOT NULL,
        EndDate DATETIME2 NULL,
        AuditStatusId INT NOT NULL,
        ResponsibleId INT NOT NULL,
        CONSTRAINT FK_Audits_AuditStatuses_AuditStatusId FOREIGN KEY (AuditStatusId) REFERENCES dbo.AuditStatuses(Id),
        CONSTRAINT FK_Audits_Responsibles_ResponsibleId FOREIGN KEY (ResponsibleId) REFERENCES dbo.Responsibles(Id)
    );
END
GO

-- Responsibles
IF OBJECT_ID('dbo.Responsibles', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Responsibles
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(150) NOT NULL,
        Email NVARCHAR(200) NOT NULL,
        Area NVARCHAR(100) NOT NULL
    );
END
GO

-- FindingTypes
IF OBJECT_ID('dbo.FindingTypes', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.FindingTypes
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Description NVARCHAR(100) NOT NULL
    );
END
GO

-- FindingSeverities
IF OBJECT_ID('dbo.FindingSeverities', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.FindingSeverities
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Description NVARCHAR(100) NOT NULL,
        Level INT NOT NULL
    );
END
GO

-- Findings
IF OBJECT_ID('dbo.Findings', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Findings
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Description NVARCHAR(3000) NOT NULL,
        FindingTypeId INT NOT NULL,
        SeverityId INT NOT NULL,
        Date DATETIME2 NOT NULL,
        AuditId INT NOT NULL,
        CONSTRAINT FK_Findings_FindingTypes_TypeId FOREIGN KEY (FindingTypeId) REFERENCES dbo.FindingTypes(Id),
        CONSTRAINT FK_Findings_FindingSeverities_SeverityId FOREIGN KEY (SeverityId) REFERENCES dbo.FindingSeverities(Id),
        CONSTRAINT FK_Findings_Audits_AuditId FOREIGN KEY (AuditId) REFERENCES dbo.Audits(Id)
    );
END
GO

-- FollowUpStatuses
IF OBJECT_ID('dbo.FollowUpStatuses', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.FollowUpStatuses
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Description NVARCHAR(100) NOT NULL
    );
END
GO

-- FollowUps
IF OBJECT_ID('dbo.FollowUps', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.FollowUps
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Description NVARCHAR(3000) NOT NULL,
        CommitmentDate DATETIME2 NOT NULL,
        FollowUpStatusId INT NOT NULL,
        FindingId INT NOT NULL,
        CONSTRAINT FK_FollowUps_FollowUpStatuses_FollowUpStatusId FOREIGN KEY (FollowUpStatusId) REFERENCES dbo.FollowUpStatuses(Id),
        CONSTRAINT FK_FollowUps_Findings_FindingId FOREIGN KEY (FindingId) REFERENCES dbo.Findings(Id)
    );
END
GO

-- Seed Roles minimal data (if empty)
IF NOT EXISTS (SELECT 1 FROM dbo.Roles)
BEGIN
    INSERT INTO dbo.Roles (Name, Description) VALUES
    ('Admin', 'Administrator role'),
    ('User', 'Standard user role');
END
GO
