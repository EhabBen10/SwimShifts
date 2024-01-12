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

CREATE TABLE [WaterSamples] (
    [Id] int NOT NULL IDENTITY,
    [FritKlor] decimal(18,6) NULL,
    [Bundklor] decimal(18,6) NULL,
    [Differace] decimal(18,6) NULL,
    [Ph] decimal(18,6) NULL,
    [AutoFritKlor] decimal(18,6) NULL,
    [AutoPH] decimal(18,6) NULL,
    [WaterSampleTime] datetime2 NULL,
    CONSTRAINT [PK_WaterSamples] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240107113303_InitialCreate', N'7.0.13');
GO

COMMIT;
GO

