namespace BankKata
{
    public class Account
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly StatementPrinter _statementPrinter;
        private readonly Clock _clock;

        public Account(ITransactionRepository transactionRepository, StatementPrinter statementPrinter, Clock clock)
        {
            _transactionRepository = transactionRepository;
            _statementPrinter = statementPrinter;
            _clock = clock;
        }
        
        public void Deposit(int amount)
        {
            var transaction = new Transaction(_clock.GetTodayAsString(), amount);
            _transactionRepository.Add(transaction);
        }

        public void Withdrawal(int amount)
        {
            var transaction = new Transaction(_clock.GetTodayAsString(), -amount);
            _transactionRepository.Add(transaction);
        }

        public void PrintStatement()
        {
            _statementPrinter.Print(_transactionRepository.All());
        }
    }
}