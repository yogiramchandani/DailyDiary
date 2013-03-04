using DailyDiaryApi.App_Start;
using DailyDiaryApi.Properties;
using Newtonsoft.Json.Serialization;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;

namespace DailyDiaryApi
{
    public class BootStrap
    {
        public void Configure(HttpConfiguration config)
        {
            RouteConfig.RegisterRoutes(config.Routes);

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                new CamelCasePropertyNamesContractResolver();
        }

        public void InstallDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dailydiarydb"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "Master";
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                
                using (var cmd = conn.CreateCommand())
                {
                    var dbSchema = Resources.DbSchema;
                    foreach (var sql in dbSchema.Split(new[]{"GO"}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();    
                    }
                }
            }
        }

        public void UninstallDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dailydiarydb"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "Master";
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                var dropDb =
                    "IF EXISTS (SELECT name from master.dbo.sysdatabases WHERE name=N'DailyDiary') DROP DATABASE [DailyDiary];";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = dropDb;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}