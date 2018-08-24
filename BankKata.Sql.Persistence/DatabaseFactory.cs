using System.Data.SqlClient;
using NPoco;

namespace BankKata.Sql.Persistence
{
    public class DatabaseFactory
    {
        private readonly string _connectionString;

        public DatabaseFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IDatabase NewDatabase()
        {
            return new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance);
        }
    }
}