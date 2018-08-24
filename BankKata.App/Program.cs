using BankKata.Domain;
using BankKata.External;
using BankKata.Sql.Persistence;

namespace BankKata.App
{
    class Program
    {
        static void Main()
        {
            var databaseFactory = new DatabaseFactory("Server=(localdb)\\mssqllocaldb;Database=BankKata;Trusted_Connection=True");
            var transactionRepository = new SqlTransactionRepository(databaseFactory);
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