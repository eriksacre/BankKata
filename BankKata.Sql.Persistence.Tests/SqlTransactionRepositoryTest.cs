using System.Data.SqlClient;
using BankKata.Domain;
using BankKata.Tests;
using NUnit.Framework;

namespace BankKata.Sql.Persistence.Tests
{
    [TestFixture]
    public class SqlTransactionRepositoryTest : TransactionRepositoryBaseTest
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BankKata;Trusted_Connection=True";

        protected override ITransactionRepository NewRepository()
        {
            return new SqlTransactionRepository();
        }

        [SetUp]
        public void Setup()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var truncate = new SqlCommand("truncate table Transactions", connection);
                truncate.ExecuteNonQuery();
            }
        }
    }
}