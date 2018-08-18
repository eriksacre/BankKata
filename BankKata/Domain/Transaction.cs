namespace BankKata.Domain
{
    public struct Transaction
    {
        public string Date { get; }
        public int Amount { get; }

        public Transaction(string date, int amount)
        {
            Date = date;
            Amount = amount;
        }
    }
}