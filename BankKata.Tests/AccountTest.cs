using System.Collections.Generic;
using BankKata.Domain;
using NSubstitute;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class AccountTest
    {
        private ITransactionRepository _transactionRepository;
        private IStatementPrinter _statementPrinter;
        private IClock _clock;
        private Account _account;
        private const string SystemDate = "12/05/2017";
        private const int Amount = 100;

        [SetUp]
        public void Setup()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            _statementPrinter = Substitute.For<IStatementPrinter>();
            _clock = Substitute.For<IClock>();
            _account = new Account(_transactionRepository, _statementPrinter, _clock);
        }

        [Test]
        public void Deposit_PositiveAmount_StoresTransaction()
        {
            _clock.GetTodayAsString().Returns(SystemDate);

            _account.Deposit(Amount);

            _transactionRepository.Received().Add(new Transaction(SystemDate, Amount));
        }

        [Test]
        public void Withdrawal_PositiveAmount_StoresTransactionForNegativeAmount()
        {
            _clock.GetTodayAsString().Returns(SystemDate);

            _account.Withdraw(Amount);

            _transactionRepository.Received().Add(new Transaction(SystemDate, -Amount));
        }

        [Test]
        public void PrintStatement_MultipleTransactionsInRepo_PrintsAllTransactions()
        {
            var transactionList = new List<Transaction>().AsReadOnly();
            _transactionRepository.All().Returns(transactionList);
            
            _account.PrintStatement();

            _statementPrinter.Received().Print(transactionList);
        }
    }
}