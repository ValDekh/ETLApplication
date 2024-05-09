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
GO

CREATE TABLE [ETLData] (
    [Id] int NOT NULL IDENTITY,
    [TpepPickupDatetime] datetime2 NOT NULL,
    [TpepDropoffDatetime] datetime2 NOT NULL,
    [PassengerCount] int NOT NULL,
    [TripDistance] real NOT NULL,
    [StoreAndFwdFlag] nvarchar(max) NOT NULL,
    [PULocationID] int NOT NULL,
    [DOLocationID] int NOT NULL,
    [FareAmount] real NOT NULL,
    [TipAmount] real NOT NULL,
    CONSTRAINT [PK_ETLData] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240509122022_InitialDB', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ETLData]') AND [c].[name] = N'StoreAndFwdFlag');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ETLData] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ETLData] ALTER COLUMN [StoreAndFwdFlag] nvarchar(max) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ETLData]') AND [c].[name] = N'PassengerCount');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ETLData] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ETLData] ALTER COLUMN [PassengerCount] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240509145248_AddNullableProperties', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE INDEX [IX_ETLData_PULocationID_TipAmount] ON [ETLData] ([PULocationID], [TipAmount]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240509181709_AddNewIndexIntoPULocationIDAndTipAmount', N'8.0.4');
GO

COMMIT;
GO

