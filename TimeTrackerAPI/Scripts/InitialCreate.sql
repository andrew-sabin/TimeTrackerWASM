IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [TimeEntries] (
    [Id] int NOT NULL IDENTITY,
    [Project] nvarchar(max) NOT NULL,
    [Start] datetime2 NOT NULL,
    [End] datetime2 NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateUpdated] datetime2 NULL,
    CONSTRAINT [PK_TimeEntries] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250630001057_InitialCreate', N'9.0.6');

COMMIT;
GO

