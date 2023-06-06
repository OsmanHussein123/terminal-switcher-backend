using ContainerService.models;
using Docker.DotNet.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContainerService.data
{

    public class CommandContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Command> Commands => _database.GetCollection<Command>("Commands");
        public CommandContext(string connectionString, string databaseName)
        {

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

    }
}
