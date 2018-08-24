using System.Collections.ObjectModel;
using System.Linq;
using BankKata.Domain;
using NPoco;
using Transaction = BankKata.Domain.Transaction;

namespace BankKata.Sql.Persistence
{
    public class SqlTransactionRepository : ITransactionRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public SqlTransactionRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        
        public void Add(Transaction transaction)
        {
            var dbTransaction = new TransactionDto
            {
                TransactionDate = transaction.Date,
                Amount = transaction.Amount
            };
            
            using (var db = _databaseFactory.NewDatabase())
            {
                db.Insert(dbTransaction);
            }
        }

        public ReadOnlyCollection<Transaction> AllOrderedByInsertionDate()
        {
            using (var db = _databaseFactory.NewDatabase())
            {
                var transactions = db.Fetch<TransactionDto>("select * from Transactions order by Id");
                return transactions
                    .Select(transaction => new Transaction(transaction.TransactionDate, transaction.Amount))
                    .ToList()
                    .AsReadOnly();
            }
        }

        [TableName("Transactions")]
        private class TransactionDto
        {
            public string TransactionDate { get; set; }
            public int Amount { get; set; }
        }
    }
}