using System.Data.SqlClient;
using System.Linq;
using NPoco;
using NPoco.FluentMappings;
using NUnit.Framework;

namespace BankKata.Sql.Persistence.Tests
{
    // This is me trying to discover how NPoco's fluent mappings work
    // as well as why mapping does not work for the Transaction struct
    //
    // Findings:
    // - Fluent mappings are straightforward
    // - NPoco cannot use structs
    // - It requires classes with properties and parameterless constructor
    // - Setters do not need to be public
    public class NPocoDiscovery
    {
        [Test]
        public void ExploreFluentMappings()
        {
            new DatabaseCleaner(MyFactory.ConnectionString).Clean();
            
            var dto = new Dto("xx-xx-xxxx", 1);
            using (var db = MyFactory.DbFactory.GetDatabase())
            {
                db.Insert(dto);
            }

            using (var db = MyFactory.DbFactory.GetDatabase())
            {
                var data = db.Fetch<Dto>().First();
                Assert.That(data.Date, Is.EqualTo("xx-xx-xxxx"));
                Assert.That(data.Amount, Is.EqualTo(1));
            }
        }
    }

    public class Dto
    {
        public Dto() {}
        
        public Dto(string date, int amount)
        {
            Date = date;
            Amount = amount;
        }
        
        public string Date { get; }
        public int Amount { get; }
    }

    public class DtoMapping : Map<Dto>
    {
        public DtoMapping()
        {
            TableName("Transactions");
            Columns(x =>
            {
                x.Column(y => y.Date).WithName("TransactionDate");
                x.Column(y => y.Amount);
            });
        }
    }

    public static class MyFactory
    {
        public const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BankKata;Trusted_Connection=True";
        
        public static NPoco.DatabaseFactory DbFactory { get; }

        static MyFactory()
        {
            var fluentConfig = FluentMappingConfiguration.Configure(new DtoMapping());
            
            DbFactory = NPoco.DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() =>
                    new Database(ConnectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance));
                x.WithFluentConfig(fluentConfig);
            });
        }
    }
}