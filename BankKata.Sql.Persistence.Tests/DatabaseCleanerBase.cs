using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankKata.Sql.Persistence.Tests
{
    public abstract class DatabaseCleanerBase
    {
        private readonly string _connectionString;

        protected DatabaseCleanerBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void Clean()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                TruncateTables(connection);
            }
        }
        
        protected abstract List<string> TablesToClear { get; }

        private void TruncateTables(SqlConnection connection)
        {
            foreach (var table in TablesToClear)
            {
                Truncate(table, connection);
            }
        }

        private void Truncate(string table, SqlConnection connection)
        {
            var truncate = new SqlCommand($"truncate table {table}", connection);
            truncate.ExecuteNonQuery();
        }
    }
}