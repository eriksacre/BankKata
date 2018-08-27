using BankKata.Domain;
using BankKata.Sql.Persistence;
using BankKata.Sql.Persistence.Tests;
using NSubstitute;
using NUnit.Framework;

namespace BankKata.FeatureTests
{
    public class PrintStatement
    {
        private DatabaseFactory _databaseFactory;
        private ITransactionRepository _transactionRepository;
        private IConsole _console;
        private IStatementPrinter _statementPrinter;
        private IClock _clock;
        private Account _account;

        [SetUp]
        public void Setup()
        {
            new DatabaseCleaner().Clean();
            
            _databaseFactory = new DatabaseFactory(TestConfiguration.ConnectionString);
            _transactionRepository = new SqlTransactionRepository(_databaseFactory);
            _console = Substitute.For<IConsole>();
            _statementPrinter = new StatementPrinter(_console);
            _clock = Substitute.For<IClock>();
            _account = new Account(_transactionRepository, _statementPrinter, _clock);
        }

        [Test]
        public void StatementShouldContainAllTransactionsInReverseChronologicalOrder()
        {
            _clock.GetTodayAsString().Returns(
                "01/04/2014",
                "02/04/2014",
                "10/04/2014"
            );
            _account.Deposit(1000);
            _account.Withdraw(100);
            _account.Deposit(500);
            
            _account.PrintStatement();
            
            Received.InOrder(() =>
            {
                _console.PrintLine("DATE | AMOUNT | BALANCE");
                _console.PrintLine("10/04/2014 | 500.00 | 1400.00");
                _console.PrintLine("02/04/2014 | -100.00 | 900.00");
                _console.PrintLine("01/04/2014 | 1000.00 | 1000.00");
            });
        }
    }
}