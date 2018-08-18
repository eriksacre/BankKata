using NUnit.Framework;
using NSubstitute;

namespace BankKata.Tests.Features
{
    public class PrintStatement
    {
        [Test]
        public void StatementShouldContainAllTransactionsInReverseChronologicalOrder()
        {
            var transactionRepository = new TransactionRepository();
            var console = Substitute.For<IConsole>();
            var statementPrinter = new StatementPrinter(console);
            var clock = Substitute.For<IClock>();
            clock.GetTodayAsString().Returns(
                "01/04/2014",
                "02/04/2014",
                "10/04/2014"
            );
            var account = new Account(transactionRepository, statementPrinter, clock);
            account.Deposit(1000);
            account.Withdrawal(100);
            account.Deposit(500);
            
            account.PrintStatement();
            
            Received.InOrder(() =>
            {
                console.PrintLine("DATE | AMOUNT | BALANCE");
                console.PrintLine("10/04/2014 | 500.00 | 1400.00");
                console.PrintLine("02/04/2014 | -100.00 | 900.00");
                console.PrintLine("01/04/2014 | 1000.00 | 1000.00");
            });
        }
    }
}