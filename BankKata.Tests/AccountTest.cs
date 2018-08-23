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
            const string theSystemDate = "12/05/2017";
            const int anAmount = 100;
            _clock.GetTodayAsString().Returns(theSystemDate);

            _account.Deposit(anAmount);

            _transactionRepository.Received().Add(new Transaction(theSystemDate, anAmount));
        }

        [Test]
        public void Withdrawal_PositiveAmount_StoresTransactionForNegativeAmount()
        {
            const string theSystemDate = "1/08/2017";
            const int anAmount = 50;
            _clock.GetTodayAsString().Returns(theSystemDate);

            _account.Withdraw(anAmount);

            _transactionRepository.Received().Add(new Transaction(theSystemDate, -anAmount));
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