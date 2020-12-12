create table Users
(
	Id uniqueidentifier not null
		primary key,
	Name varchar(50) not null,
	Email varchar(100) not null,
	Password varchar(100) not null,
	CreatedOn datetime not null,
	UpdatedOn datetime,
	Gender int not null
)
go

create table Accounts
(
	Id uniqueidentifier not null
		primary key,
	UserId uniqueidentifier not null
		references Users,
	CreatedOn datetime not null,
	UpdatedOn datetime
)
go

create table Balance
(
	Id uniqueidentifier not null
		primary key,
	AccountId uniqueidentifier not null
		references Accounts,
	UserId uniqueidentifier not null
		references Users,
	Balance decimal not null,
	CreatedOn datetime not null,
	UpdatedOn datetime
)
go

create table Moviments
(
	Id uniqueidentifier not null
		primary key,
	Value decimal not null,
	Description varchar(100) not null,
	Category int not null,
	Type int not null,
	AccountId uniqueidentifier not null
		references Accounts,
	UserId uniqueidentifier not null
		references Users,
	CreatedOn datetime not null,
	UpdatedOn datetime
)
go

