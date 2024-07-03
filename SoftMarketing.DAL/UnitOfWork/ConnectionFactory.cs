using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace SoftMarketing.DAL.UnitOfWork
{
    public sealed class ConnectionFactory
    {
        private static string connectionString;

        public static DbConnection CreateConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                connectionString = builder.Build().GetSection("AppSettings").GetSection("ConnectionStrings").GetSection("DevDB").Value;
            }
            var conn = new MySqlConnection(connectionString);
            return conn;
        }
    }
}
