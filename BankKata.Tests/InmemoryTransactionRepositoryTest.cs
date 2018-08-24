using BankKata.Domain;
using BankKata.External;

namespace BankKata.Tests
{
    public class InmemoryTransactionRepositoryTest : TransactionRepositoryBaseTest
    {
        protected override ITransactionRepository NewRepository()
        {
            return new InMemoryTransactionRepository();
        }
    }
}