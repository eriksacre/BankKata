using BankKata.Domain;
using BankKata.Tests;
using NUnit.Framework;

namespace BankKata.Sql.Persistence.Tests
{
    [TestFixture]
    public class SqlTransactionRepositoryTest : TransactionRepositoryBaseTest
    {
        protected override ITransactionRepository NewRepository()
        {
            var factory = new DatabaseFactory(TestConfiguration.ConnectionString);
            return new SqlTransactionRepository(factory);
        }

        [SetUp]
        public void Setup()
        {
            new DatabaseCleaner().Clean();
        }
    }
}