using System.Collections.Generic;
using BankKata.Domain;
using NSubstitute;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class StatementPrinterTest
    {
        private IConsole _console;
        private StatementPrinter _statementPrinter;

        [SetUp]
        public void Setup()
        {
            _console = Substitute.For<IConsole>();
            _statementPrinter = new StatementPrinter(_console);
        }
        
        [Test]
        public void Print_NoTransactions_PrintsHeader()
        {
            var emptyList = new List<Transaction>().AsReadOnly();
            
            _statementPrinter.Print(emptyList);
            
            _console.Received().PrintLine("DATE | AMOUNT | BALANCE");
        }

        [Test]
        public void Print_MultipleTransactions_PrintInReverseChronologicalOrder()
        {
            var transactions = new List<Transaction>
            {
                new Transaction("01/01/2018", 100),
                new Transaction("02/01/2018", -50),
                new Transaction("03/01/2018", 200)
            }.AsReadOnly();
            
            _statementPrinter.Print(transactions);
            
            Received.InOrder(() =>
            {
                _console.PrintLine("DATE | AMOUNT | BALANCE");
                _console.PrintLine("03/01/2018 | 200.00 | 250.00");
                _console.PrintLine("02/01/2018 | -50.00 | 50.00");
                _console.PrintLine("01/01/2018 | 100.00 | 100.00");
            });
        }
    }
}