using NSubstitute;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class AccountTest
    {
        private const string SystemDate = "12/05/2017";

        [Test]
        public void Deposit_PositiveAmount_StoresTransaction()
        {
            var transactionRepository = Substitute.For<ITransactionRepository>();
            var console = Substitute.For<Console>();
            var clock = Substitute.For<Clock>();
            clock.GetTodayAsString().Returns(SystemDate);
            var account = new Account(transactionRepository, console, clock);
            var expectedTransaction = new Transaction(SystemDate, 100);

            account.Deposit(100);

            transactionRepository.Received().Add(expectedTransaction);
        }
    }
}