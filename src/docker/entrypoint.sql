CREATE DATABASE financas_simples_db

USE financas_simples_db



CREATE TABLE financas_simples_db.dbo.Users
(
    Id        uniqueidentifier                                  NOT NULL,
    Name      varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
    Email     varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Password  varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    CreatedOn datetime                                          NOT NULL,
    UpdatedOn datetime                                          NULL,
    Gender    int                                               NOT NULL,
    BirthDate datetime                                          NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (Id)
)



CREATE TABLE financas_simples_db.dbo.Accounts
(
    Id        uniqueidentifier NOT NULL,
    UserId    uniqueidentifier NOT NULL,
    CreatedOn datetime         NOT NULL,
    UpdatedOn datetime         NULL,
    CONSTRAINT PK_Accounts PRIMARY KEY (Id)
)


-- financas_simples_db.dbo.Accounts foreign keys

ALTER TABLE financas_simples_db.dbo.Accounts
    ADD CONSTRAINT FK_Accounts_UserId FOREIGN KEY (UserId) REFERENCES financas_simples_db.dbo.Users (Id)


CREATE TABLE financas_simples_db.dbo.Balance
(
    Id        uniqueidentifier NOT NULL,
    AccountId uniqueidentifier NOT NULL,
    UserId    uniqueidentifier NOT NULL,
    Value     decimal(18, 0)   NOT NULL,
    CreatedOn datetime         NOT NULL,
    UpdatedOn datetime         NULL,
    CONSTRAINT PK_Balance PRIMARY KEY (Id)
)


-- financas_simples_db.dbo.Balance foreign keys

ALTER TABLE financas_simples_db.dbo.Balance
    ADD CONSTRAINT FK_Balance_AccountId FOREIGN KEY (AccountId) REFERENCES financas_simples_db.dbo.Accounts (Id)
GO
ALTER TABLE financas_simples_db.dbo.Balance
    ADD CONSTRAINT FK_Balance_UserId FOREIGN KEY (UserId) REFERENCES financas_simples_db.dbo.Users (Id)


CREATE TABLE financas_simples_db.dbo.Moviments
(
    Id          uniqueidentifier                                  NOT NULL,
    Value       decimal(18, 0)                                    NOT NULL,
    Description varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Category    int                                               NOT NULL,
    [Type]      int                                               NOT NULL,
    AccountId   uniqueidentifier                                  NOT NULL,
    UserId      uniqueidentifier                                  NOT NULL,
    CreatedOn   datetime                                          NOT NULL,
    UpdatedOn   datetime                                          NULL,
    CONSTRAINT PK_Moviment PRIMARY KEY (Id)
)

ALTER TABLE financas_simples_db.dbo.Moviments
    ADD CONSTRAINT FK_Moviments_AccountId FOREIGN KEY (AccountId) REFERENCES financas_simples_db.dbo.Accounts (Id)
GO
ALTER TABLE financas_simples_db.dbo.Moviments
    ADD CONSTRAINT FK_Moviments_UserId FOREIGN KEY (UserId) REFERENCES financas_simples_db.dbo.Users (Id)


CREATE TABLE financas_simples_db.dbo.InstallmentMoviments
(
    Id          uniqueidentifier                                  NOT NULL,
    Value       decimal(18, 0)                                    NOT NULL,
    Description varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Category    int                                               NOT NULL,
    [Type]      int                                               NOT NULL,
    Month       int                                               NOT NULL,
    StartMonth  int                                               NOT NULL,
    EndMonth    int                                               NOT NULL,
    InstallmentsValue  decimal(18, 0)                                    NOT NULL,
    AccountId   uniqueidentifier                                  NOT NULL,
    UserId      uniqueidentifier                                  NOT NULL,
    CreatedOn   datetime                                          NOT NULL,
    UpdatedOn   datetime                                          NULL,
    CONSTRAINT PK_InstallmentMoviments PRIMARY KEY (Id)
)

ALTER TABLE financas_simples_db.dbo.InstallmentMoviments
    ADD CONSTRAINT FK_InstallmentMoviments_AccountId FOREIGN KEY (AccountId) REFERENCES financas_simples_db.dbo.Accounts (Id)
GO
ALTER TABLE financas_simples_db.dbo.InstallmentMoviments
    ADD CONSTRAINT FK_InstallmentMoviments_UserId FOREIGN KEY (UserId) REFERENCES financas_simples_db.dbo.Users (Id)

CREATE TABLE financas_simples_db.dbo.InvestmentMoviments
(
    Id          uniqueidentifier                                  NOT NULL,
    Value       decimal(18, 0)                                    NOT NULL,
    Description varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Type]      int                                               NOT NULL,
    MovimentId  uniqueidentifier 							      NOT NULL,
    AccountId   uniqueidentifier                                  NOT NULL,
    UserId      uniqueidentifier                                  NOT NULL,
    CreatedOn   datetime                                          NOT NULL,
    UpdatedOn   datetime                                          NULL,
    CONSTRAINT PK_InvestmentMoviments PRIMARY KEY (Id)
)

ALTER TABLE financas_simples_db.dbo.InvestmentMoviments
    ADD CONSTRAINT FK_InvestmentMoviments_AccountId FOREIGN KEY (AccountId) REFERENCES financas_simples_db.dbo.Accounts (Id)
GO
ALTER TABLE financas_simples_db.dbo.InvestmentMoviments
    ADD CONSTRAINT FK_InvestmentMoviments_UserId FOREIGN KEY (UserId) REFERENCES financas_simples_db.dbo.Users (Id)
GO
ALTER TABLE financas_simples_db.dbo.InvestmentMoviments
    ADD CONSTRAINT FK_Moviments_MovimentId FOREIGN KEY (MovimentId) REFERENCES financas_simples_db.dbo.Moviments (Id)
    

CREATE TABLE financas_simples_db.dbo.InvestmentBalance
(
    Id        uniqueidentifier NOT NULL,
    AccountId uniqueidentifier NOT NULL,
    UserId    uniqueidentifier NOT NULL,
    Value     decimal(18, 0)   NOT NULL,
    CreatedOn datetime         NOT NULL,
    UpdatedOn datetime         NULL,
    CONSTRAINT PK_InvestmentBalance PRIMARY KEY (Id)
)

ALTER TABLE financas_simples_db.dbo.InvestmentBalance
    ADD CONSTRAINT FK_InvestmentBalance_AccountId FOREIGN KEY (AccountId) REFERENCES financas_simples_db.dbo.Accounts (Id)
GO
ALTER TABLE financas_simples_db.dbo.InvestmentBalance
    ADD CONSTRAINT FK_InvestmentBalance_UserId FOREIGN KEY (UserId) REFERENCES financas_simples_db.dbo.Users (Id)


