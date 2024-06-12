// File: YourApp.Infrastructure/Data/DbConnectionFactory.cs

using System.Data;
using System.Data.SqlClient;

namespace NowSoft.Infrastructure.Data
{
    public static class DbConnectionFactory
    {
        private static string _connectionString;

        public static void Initialize(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
