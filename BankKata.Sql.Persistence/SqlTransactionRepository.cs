using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using BankKata.Domain;
using NPoco;
using Transaction = BankKata.Domain.Transaction;

namespace BankKata.Sql.Persistence
{
    public class SqlTransactionRepository : ITransactionRepository
    {
        private readonly string _connectionString;

        public SqlTransactionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void Add(Transaction transaction)
        {
            var dbTransaction = new DbTransaction
            {
                TransactionDate = transaction.Date,
                Amount = transaction.Amount
            };
            
            using (IDatabase db = new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance))
            {
                db.Insert(dbTransaction);
            }
        }

        public ReadOnlyCollection<Transaction> AllOrderedByTransactionDate()
        {
            using (IDatabase db = new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance))
            {
                var transactions = db.Fetch<DbTransaction>("select * from Transactions order by Id");
                return transactions
                    .Select(transaction => new Transaction(transaction.TransactionDate, transaction.Amount))
                    .ToList()
                    .AsReadOnly();
            }
        }

        [TableName("Transactions")]
        private class DbTransaction
        {
            public int Id { get; set; }
            public string TransactionDate { get; set; }
            public int Amount { get; set; }
        }
    }
}