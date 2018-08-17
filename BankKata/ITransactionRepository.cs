using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankKata
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        ReadOnlyCollection<Transaction> All();
    }
}