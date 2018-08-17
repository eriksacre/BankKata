using System;

namespace BankKata
{
    public class Clock
    {
        public virtual string GetTodayAsString()
        {
            return Now().ToString("dd/MM/yyyy");
        }

        protected virtual DateTime Now()
        {
            return DateTime.Now;
        }
    }
}