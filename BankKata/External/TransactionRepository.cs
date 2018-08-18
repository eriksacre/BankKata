using System.Collections.Generic;
using System.Collections.ObjectModel;
using BankKata.Domain;

namespace BankKata.External
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
        
        public void Add(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public ReadOnlyCollection<Transaction> All()
        {
            return _transactions.AsReadOnly();
        }
    }
}