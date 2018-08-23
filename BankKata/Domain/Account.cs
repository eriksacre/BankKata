using System;
using JetBrains.Annotations;

namespace BankKata.Domain
{
    public class Account
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IStatementPrinter _statementPrinter;
        private readonly IClock _clock;

        public Account(ITransactionRepository transactionRepository, IStatementPrinter statementPrinter, IClock clock)
        {
            _transactionRepository = transactionRepository;
            _statementPrinter = statementPrinter;
            _clock = clock;
        }
        
        public void Deposit(int amount)
        {
            EnsureAmountIsValid(amount);
            AddTransactionFor(amount);
        }

        public void Withdraw(int amount)
        {
            EnsureAmountIsValid(amount);
            AddTransactionFor(-amount);
        }

        public void PrintStatement()
        {
            _statementPrinter.Print(_transactionRepository.All());
        }
        
        [AssertionMethod]
        private static void EnsureAmountIsValid(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be a positive value");
            }
        }

        private void AddTransactionFor(int amount)
        {
            var transaction = new Transaction(_clock.GetTodayAsString(), amount);
            _transactionRepository.Add(transaction);
        }
    }
}
