using System;
using Dapper;
using System.IO;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreDapper.Db
{
    public class Seed
    {
        private static IDbConnection _dbConnection;

        public static void CreateDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            var dbFilePath = configuration.GetValue<string>("DBInfo:DbFilePath");
            if (!File.Exists(dbFilePath))
            {
                //SqliteConnection.CreateFile(dbFilePath);
                _dbConnection = new SqliteConnection(connectionString);
                _dbConnection.Open();

                // Create a Product table
                _dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS [Product] (
                        [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [Name] NVARCHAR(128) NOT NULL,
                        [Quantity] INTEGER NULL,
                        [Price] NUMERIC NOT NULL
                    )");

                _dbConnection.Close();
            }
            
        }
    }
}