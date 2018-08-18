using System.Collections.ObjectModel;

namespace BankKata
{
    public interface IStatementPrinter
    {
        void Print(ReadOnlyCollection<Transaction> transactions);
    }
}