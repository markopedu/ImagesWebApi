create table Clans
(
	Id int identity
		constraint PK_Clans
			primary key,
	ClanName nvarchar(max)
)
go

create table Quotes
(
	Id int identity
		constraint PK_Quotes
			primary key,
	Text nvarchar(max),
	SamuraiId int not null
		constraint FK_Quotes_Samurais_SamuraiId
			references Samurais
				on delete cascade
)
go

create index IX_Quotes_SamuraiId
	on Quotes (SamuraiId)
go


create table Samurais
(
	Id int identity
		constraint PK_Samurais
			primary key,
	Name nvarchar(max),
	ClanId int
		constraint FK_Samurais_Clans_ClanId
			references Clans
)
go

create index IX_Samurais_ClanId
	on Samurais (ClanId)
go

