using System.Collections.ObjectModel;
using System.Linq;
using BankKata.Domain;
using JetBrains.Annotations;
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
                return db.Fetch<TransactionDto>()
                    .OrderBy(x => x.Id)
                    .Select(transaction => new Transaction(transaction.TransactionDate, transaction.Amount))
                    .ToList()
                    .AsReadOnly();
            }
        }

        [TableName("Transactions")]
        private class TransactionDto
        {
            [UsedImplicitly]
            public int Id { get; }
            public string TransactionDate { get; set; }
            public int Amount { get; set; }
        }
    }
}