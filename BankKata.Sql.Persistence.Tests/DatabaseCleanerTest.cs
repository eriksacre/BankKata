using NUnit.Framework;

namespace BankKata.Sql.Persistence.Tests
{
    public class DatabaseCleanerTest
    {
        [Test]
        public void VerifyAllTablesAreIncluded()
        {
            var cleaner = new DatabaseCleaner();
            
            var missingTables = cleaner.MissingTables();
            
            Assert.That(missingTables, Is.Empty);
        }
    }
}