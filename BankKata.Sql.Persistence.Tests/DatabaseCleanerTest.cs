using NUnit.Framework;

namespace BankKata.Sql.Persistence.Tests
{
    public class DatabaseCleanerTest
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BankKata;Trusted_Connection=True";

        [Test]
        public void VerifyAllTablesAreIncluded()
        {
            var cleaner = new DatabaseCleaner(ConnectionString);
            
            var missingTables = cleaner.MissingTables();
            
            Assert.That(missingTables, Is.EqualTo(""));
        }
    }
}