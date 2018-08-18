using System;

namespace BankKata
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