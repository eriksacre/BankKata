﻿using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankKata.Sql.Persistence.Tests
{
    public class DatabaseCleaner
    {
        private readonly string _connectionString;

        // Note:
        // Order is important. First truncate the tables that reference
        // other tables.
        private static readonly List<string> TablesToClear = new List<string>
        {
            "Transactions"
        };

        public DatabaseCleaner(string connectionString)
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