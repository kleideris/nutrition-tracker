-- Create the database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'NutritionTrackerDB')
BEGIN
    CREATE DATABASE NutritionTrackerDB;
END
GO

USE NutritionTrackerDB;
GO