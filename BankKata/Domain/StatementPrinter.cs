using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankKata.Domain
{
    public class StatementPrinter : IStatementPrinter
    {
        private readonly IConsole _console;

        public StatementPrinter(IConsole console)
        {
            _console = console;
        }
        
        public void Print(ReadOnlyCollection<Transaction> transactions)
        {
            PrintHeader();
            PrintStatementsFor(transactions);
        }

        private void PrintHeader()
        {
            _console.PrintLine("DATE | AMOUNT | BALANCE");
        }

        private void PrintStatementsFor(ReadOnlyCollection<Transaction> transactions)
        {
            var statementLines = CreateStatementLines(transactions);
            PrintStatementLinesInReverseChronology(statementLines);
        }

        private List<string> CreateStatementLines(ReadOnlyCollection<Transaction> transactions)
        {
            var statementLines = new List<string>();
            var runningBalance = 0;
            foreach (var transaction in transactions)
            {
                statementLines.Add(CreateStatementLine(transaction, ref runningBalance));
            }

            return statementLines;
        }

        private void PrintStatementLinesInReverseChronology(List<string> statementLines)
        {
            statementLines.Reverse();
            foreach (var statementLine in statementLines)
            {
                _console.PrintLine(statementLine);
            }
        }

        private string CreateStatementLine(Transaction transaction, ref int runningBalance)
        {
            runningBalance += transaction.Amount;
            return 
                transaction.Date + " | " 
                + FormatDecimal(transaction.Amount) + " | " 
                + FormatDecimal(runningBalance);
        }

        private string FormatDecimal(int value)
        {
            return value + ".00";
        }
    }
}