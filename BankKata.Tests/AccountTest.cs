using NSubstitute;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class AccountTest
    {
        private ITransactionRepository _transactionRepository;
        private Console _console;
        private Clock _clock;
        private const string SystemDate = "12/05/2017";

        [SetUp]
        public void Setup()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            _console = Substitute.For<Console>();
            _clock = Substitute.For<Clock>();
            _clock.GetTodayAsString().Returns(SystemDate);
        }

        [Test]
        public void Deposit_PositiveAmount_StoresTransaction()
        {
            var account = new Account(_transactionRepository, _console, _clock);
            var expectedTransaction = new Transaction(SystemDate, 100);

            account.Deposit(100);

            _transactionRepository.Received().Add(expectedTransaction);
        }

        [Test]
        public void Withdrawal_PositiveAmount_StoresTransactionForNegativeAmount()
        {
            var account = new Account(_transactionRepository, _console, _clock);
            var expectedTransaction = new Transaction(SystemDate, -100);

            account.Withdrawal(100);

            _transactionRepository.Received().Add(expectedTransaction);
        }
    }
}