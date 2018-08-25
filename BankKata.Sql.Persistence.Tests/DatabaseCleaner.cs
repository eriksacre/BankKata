using System.Collections.Generic;

namespace BankKata.Sql.Persistence.Tests
{
    public class DatabaseCleaner : DatabaseCleanerBase
    {
        public DatabaseCleaner() : base(TestConfiguration.ConnectionString)
        {
            // Note:
            // Order is important. First truncate the tables that reference
            // other tables.
            TablesToClear = new List<string>
            {
                "Transactions"
            };
            
            TablesNotToClear = new List<string>
            {
                "SchemaVersions"
            };
        }

        protected override List<string> TablesToClear { get; }
        
        protected override List<string> TablesNotToClear { get; }
    }
}