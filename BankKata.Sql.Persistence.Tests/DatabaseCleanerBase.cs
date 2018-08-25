using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BankKata.Sql.Persistence.Tests
{
    public abstract class DatabaseCleanerBase
    {
        private readonly string _connectionString;

        private static readonly List<string> _doNotTruncate = new List<string>
        {
            "SchemaVersions"
        };

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

        public string MissingTables()
        {
            var tables = GetTableNames();
            var tablesToClear = TablesToClear;
            var missingTables = tables
                .Where(tableName => !tablesToClear.Contains(tableName) && !_doNotTruncate.Contains(tableName));

            return string.Join(",", missingTables);
        }

        private List<string> GetTableNames()
        {
            var tables = new List<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE='BASE TABLE'",
                    connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader["TABLE_NAME"].ToString());
                    }
                }
            }

            return tables;
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