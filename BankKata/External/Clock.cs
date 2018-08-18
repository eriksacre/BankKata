using System;
using BankKata.Domain;

namespace BankKata.External
{
    public class Clock : IClock
    {
        public string GetTodayAsString()
        {
            return Now().ToString("dd/MM/yyyy");
        }

        protected virtual DateTime Now()
        {
            return DateTime.Now;
        }
    }
}