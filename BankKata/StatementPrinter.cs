using System;
using System.Collections.ObjectModel;

namespace BankKata
{
    public class StatementPrinter
    {
        public virtual void Print(ReadOnlyCollection<Transaction> transactions)
        {
            throw new NotImplementedException();
        }
    }
}