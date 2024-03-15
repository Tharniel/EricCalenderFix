using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kalender.Data
{
    internal class DB
    {

        internal static MongoClient GetClient()
        {
            string connectionString = @"mongodb+srv://ericklassonlbs:tge5Wpgo5i64Uj@ericcalender.8vwkxv2.mongodb.net/?retryWrites=true&w=majority&appName=EricCalender";


            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);

            return mongoClient;
        }

        internal static IMongoDatabase GetDatabase()
        {
            var client = GetClient();
            var database = client.GetDatabase("CalenderDB");
            return database;
        }

        public static IMongoCollection<Models.User> UserCollection() 
        {
            var database = GetDatabase();

            IMongoCollection<Models.User> userCollection = database.GetCollection<Models.User>("User");
            return userCollection;

        }
        public static IMongoCollection<Models.Event> TaskCollection()
        {
            var database = GetDatabase();

            var taskCollection = database.GetCollection<Models.Event>("Events");

            return taskCollection;

        }

        public static async Task<List<Models.Event>> GetEventsFromDB()
        {
            var taskCollection = TaskCollection();
            var user = Singletons.Authorized.GetAuthStatus().WhoIsUser();
            var filter = Builders<Models.Event>.Filter.Eq("Username", user);

            var result = await taskCollection.FindAsync(filter);

            return await result.ToListAsync();
        }

        public static async Task<List<Models.User>> GetUserFromDB(string user)
        {
            var userCollection = UserCollection();
            var filter = Builders<Models.User>.Filter.Eq("Name", user);

            var result = await userCollection.FindAsync(filter);

            return await result.ToListAsync();
        }
    } 
}
