create table Transactions (
	Id int identity(1,1) not null,
	TransactionDate char(10) not null,
	Amount int not null,

	constraint PK_Transactions primary key(Id)
)

create index Transactions_TransactionDate on Transactions(TransactionDate)
