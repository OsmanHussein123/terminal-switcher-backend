using ContainerService.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContainerService.data
{

    public class CommandContext
    {
        public IMongoCollection<Command> Commands { get; }
        private readonly MongoDBConfig _settings;
        public CommandContext(IOptions<MongoDBConfig> settings)
        {

            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            Commands = database.GetCollection<Command>(_settings.CommandCollectionName);
        }


        public async Task CreateAsync(Command newCommand) =>
    await Commands.InsertOneAsync(newCommand);

        public async Task<List<Command>> GetAsync() =>
    await Commands.Find(_ => true).ToListAsync();
    }
}
