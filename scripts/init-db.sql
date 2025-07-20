-- Criar o banco de dados userTransactions se não existir
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'userTransactions')
BEGIN
    CREATE DATABASE userTransactions;
    PRINT 'Database userTransactions created successfully.';
END
ELSE
BEGIN
    PRINT 'Database userTransactions already exists.';
END
GO

-- Usar o banco de dados userTransactions
USE userTransactions;
GO

PRINT 'Database userTransactions is ready for use.';
GO
