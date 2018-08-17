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
            var console = Substitute.For<Console>();
            var statementPrinter = new StatementPrinter();
            var clock = new Clock();
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