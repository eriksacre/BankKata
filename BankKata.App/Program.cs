using BankKata.Domain;
using BankKata.External;

namespace BankKata.App
{
    class Program
    {
        static void Main()
        {
            var transactionRepository = new TransactionRepository();
            var console = new Console();
            var statementPrinter = new StatementPrinter(console);
            var clock = new Clock();
            var account = new Account(transactionRepository, statementPrinter, clock);
            
            account.Deposit(1000);
            account.Withdraw(100);
            account.Deposit(500);
            
            account.PrintStatement();
        }
    }
}