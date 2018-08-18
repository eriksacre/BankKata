using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class AccountTest
    {
        private ITransactionRepository _transactionRepository;
        private Console _console;
        private StatementPrinter _statementPrinter;
        private Clock _clock;
        private const string SystemDate = "12/05/2017";

        [SetUp]
        public void Setup()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            // NOTE:
            // The account test should have no knowledge of Console,
            // nor does it care about constructor args for StatementPrinter.
            // We could avoid this by working through an interface IStatementPrinter
            // but then we would define an interface for the sole purpose of
            // facilitating this test. Tradeoff!
            _console = Substitute.For<Console>();
            _statementPrinter = Substitute.For<StatementPrinter>(_console);
            _clock = Substitute.For<Clock>();
            _clock.GetTodayAsString().Returns(SystemDate);
        }

        [Test]
        public void Deposit_PositiveAmount_StoresTransaction()
        {
            var account = new Account(_transactionRepository, _statementPrinter, _clock);
            var expectedTransaction = new Transaction(SystemDate, 100);

            account.Deposit(100);

            _transactionRepository.Received().Add(expectedTransaction);
        }

        [Test]
        public void Withdrawal_PositiveAmount_StoresTransactionForNegativeAmount()
        {
            var account = new Account(_transactionRepository, _statementPrinter, _clock);
            var expectedTransaction = new Transaction(SystemDate, -100);

            account.Withdrawal(100);

            _transactionRepository.Received().Add(expectedTransaction);
        }

        [Test]
        public void PrintStatement_MultipleTransactionsInRepo_PrintsAllTransactions()
        {
            var transactionList = new List<Transaction>().AsReadOnly();
            _transactionRepository.All().Returns(transactionList);
            var account = new Account(_transactionRepository, _statementPrinter, _clock);
            
            account.PrintStatement();

            _statementPrinter.Received().Print(transactionList);
        }
    }
}