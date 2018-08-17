namespace BankKata.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactionRepository = new TransactionRepository();
            var console = new Console();
            var statementPrinter = new StatementPrinter(console);
            var clock = new Clock();
            var account = new Account(transactionRepository, statementPrinter, clock);
            
            account.Deposit(1000);
            account.Withdrawal(100);
            account.Deposit(500);
            
            account.PrintStatement();
        }
    }
}