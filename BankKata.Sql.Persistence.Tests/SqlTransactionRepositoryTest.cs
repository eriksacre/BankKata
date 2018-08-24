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
            throw new System.NotImplementedException();
        }
    }
}