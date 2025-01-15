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
CREATE TABLE [Doctors] (
    [DoctorID] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Specialization] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY ([DoctorID])
);

CREATE TABLE [Patients] (
    [PatientID] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY ([PatientID])
);

CREATE TABLE [Appointments] (
    [AppointmentID] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [Time] time NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [PatientID] int NOT NULL,
    [DoctorID] int NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY ([AppointmentID]),
    CONSTRAINT [FK_Appointments_Doctors_DoctorID] FOREIGN KEY ([DoctorID]) REFERENCES [Doctors] ([DoctorID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointments_Patients_PatientID] FOREIGN KEY ([PatientID]) REFERENCES [Patients] ([PatientID]) ON DELETE CASCADE
);

CREATE INDEX [IX_Appointments_DoctorID] ON [Appointments] ([DoctorID]);

CREATE INDEX [IX_Appointments_PatientID] ON [Appointments] ([PatientID]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250115160804_InitialCreate', N'9.0.0');

COMMIT;
GO

