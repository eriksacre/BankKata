using System.Collections.ObjectModel;

namespace BankKata.Domain
{
    public interface IStatementPrinter
    {
        void Print(ReadOnlyCollection<Transaction> transactions);
    }
}