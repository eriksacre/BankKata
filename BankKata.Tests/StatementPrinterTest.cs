using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class StatementPrinterTest
    {
        [Test]
        public void Print_NoTransactions_PrintsHeader()
        {
            var console = Substitute.For<IConsole>();
            var statementPrinter = new StatementPrinter(console);
            var emptyList = new List<Transaction>().AsReadOnly();
            
            statementPrinter.Print(emptyList);
            
            console.Received().PrintLine("DATE | AMOUNT | BALANCE");
        }

        [Test]
        public void Print_MultipleTransactions_PrintInReverseChronologicalOrder()
        {
            var console = Substitute.For<IConsole>();
            var statementPrinter = new StatementPrinter(console);
            var transactions = new List<Transaction>
            {
                new Transaction("01/01/2018", 100),
                new Transaction("02/01/2018", -50),
                new Transaction("03/01/2018", 200)
            }.AsReadOnly();
            
            statementPrinter.Print(transactions);
            
            Received.InOrder(() =>
            {
                console.PrintLine("DATE | AMOUNT | BALANCE");
                console.PrintLine("03/01/2018 | 200.00 | 250.00");
                console.PrintLine("02/01/2018 | -50.00 | 50.00");
                console.PrintLine("01/01/2018 | 100.00 | 100.00");
            });
        }
    }
}