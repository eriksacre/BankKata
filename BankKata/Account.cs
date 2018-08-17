using System;

namespace BankKata
{
    public class Account
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly Console _console;
        private readonly Clock _clock;

        public Account(ITransactionRepository transactionRepository, Console console, Clock clock)
        {
            _transactionRepository = transactionRepository;
            _console = console;
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
        }
    }
}