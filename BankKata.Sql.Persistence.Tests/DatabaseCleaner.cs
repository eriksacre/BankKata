using System.Collections.Generic;

namespace BankKata.Sql.Persistence.Tests
{
    public class DatabaseCleaner : DatabaseCleanerBase
    {
        public DatabaseCleaner(string connectionString) : base(connectionString) {}

        // Note:
        // Order is important. First truncate the tables that reference
        // other tables.
        protected override List<string> TablesToClear => new List<string>
        {
            "Transactions"
        };
    }
}