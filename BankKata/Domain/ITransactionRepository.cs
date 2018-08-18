using System.Collections.ObjectModel;

namespace BankKata.Domain
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        ReadOnlyCollection<Transaction> All();
    }
}