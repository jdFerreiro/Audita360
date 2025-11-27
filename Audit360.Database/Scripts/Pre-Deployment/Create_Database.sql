-- Create database if not exists
IF DB_ID(N'Audit360') IS NULL
BEGIN
    PRINT N'Creating database [Audit360]...';
    CREATE DATABASE [Audit360];
    PRINT N'Database [Audit360] created.';
END
ELSE
BEGIN
    PRINT N'Database [Audit360] already exists.';
END
GO
