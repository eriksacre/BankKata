using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class AccountTest
    {
        private ITransactionRepository _transactionRepository;
        private IStatementPrinter _statementPrinter;
        private IClock _clock;
        private const string SystemDate = "12/05/2017";

        [SetUp]
        public void Setup()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            _statementPrinter = Substitute.For<IStatementPrinter>();
            _clock = Substitute.For<IClock>();
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