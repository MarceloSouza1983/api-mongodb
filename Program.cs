using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;

namespace Api {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();

            var databaseName = "coronavirus";
            var connectionString = "mongodb+srv://<user>:<password>@cluster0.gmfj5.mongodb.net/";
            var client = new MongoClient(connectionString + databaseName);

            var dbList = client.ListDatabases().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList) {
                Console.WriteLine(db);
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}