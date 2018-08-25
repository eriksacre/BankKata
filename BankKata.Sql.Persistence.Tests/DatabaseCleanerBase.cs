using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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

        public List<string> MissingTables()
        {
            return AllTableNames()
                .Where(tableName => !TablesToClear.Contains(tableName) && !TablesNotToClear.Contains(tableName))
                .ToList();
        }

        protected abstract List<string> TablesToClear { get; }

        protected abstract List<string> TablesNotToClear { get; }

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

        private List<string> AllTableNames()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return FetchTableNames(connection);
            }
        }

        private List<string> FetchTableNames(SqlConnection connection)
        {
            var command = new SqlCommand("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE='BASE TABLE'",
                connection);
            using (var reader = command.ExecuteReader())
            {
                var tables = new List<string>();
                AddReaderResultsToList(reader, tables);
                return tables;
            }
        }

        private void AddReaderResultsToList(SqlDataReader reader, List<string> tables)
        {
            while (reader.Read())
            {
                tables.Add(reader["TABLE_NAME"].ToString());
            }
        }
    }
}