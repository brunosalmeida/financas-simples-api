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
GO;


CREATE TABLE financas_simples_db.dbo.Accounts
(
    Id        uniqueidentifier NOT NULL,
    UserId    uniqueidentifier NOT NULL,
    CreatedOn datetime         NOT NULL,
    UpdatedOn datetime         NULL,
    CONSTRAINT PK_Accounts PRIMARY KEY (Id)
)
GO;

-- financas_simples_db.dbo.Accounts foreign keys

ALTER TABLE financas_simples_db.dbo.Accounts
    ADD CONSTRAINT FK_Accounts_UserId FOREIGN KEY (UserId) REFERENCES financas_simples_db.dbo.Users (Id)
GO;

CREATE TABLE financas_simples_db.dbo.Balance
(
    Id        uniqueidentifier NOT NULL,
    AccountId uniqueidentifier NOT NULL,
    UserId    uniqueidentifier NOT NULL,
    Value     decimal(18, 0)   NOT NULL,
    CreatedOn datetime         NOT NULL,
    UpdatedOn datetime         NULL,
    CONSTRAINT PK__Balance__3214EC07786D2C62 PRIMARY KEY (Id)
)
GO;

-- financas_simples_db.dbo.Balance foreign keys

ALTER TABLE financas_simples_db.dbo.Balance
    ADD CONSTRAINT FK_Balance_AccountId FOREIGN KEY (AccountId) REFERENCES financas_simples_db.dbo.Accounts (Id)
GO
ALTER TABLE financas_simples_db.dbo.Balance
    ADD CONSTRAINT FK_Balance_UserId FOREIGN KEY (UserId) REFERENCES financas_simples_db.dbo.Users (Id)
GO;


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

