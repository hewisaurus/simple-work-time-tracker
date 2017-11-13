using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Database.Dapper;
using MySql.Data.MySqlClient;

namespace Database
{
    public class MysqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public MysqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection Create()
        {
            return new MySqlConnection(_connectionString);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
