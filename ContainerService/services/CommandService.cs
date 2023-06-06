using ContainerService.data;
using ContainerService.models;
using Docker.DotNet.Models;
using MongoDB.Driver;

namespace ContainerService.services
{
    public class CommandService : ICommandService
    {
        private readonly IMongoCollection<Command> _collection;

        public CommandService(CommandContext context) 
        {
            _collection = context.Commands;
        }
        public async Task<Command> Add(Command command)
        {
            await _collection.InsertOneAsync(command);
            return command;
        }

        public IQueryable<Command> GetAll()
        {
            return _collection.AsQueryable();
        }
        public void Initialize()
        {
            // Add any necessary initialization steps here
            // For example, creating indexes or setting up initial data

            // Create an index on the Id field for faster lookup
            var indexKeys = Builders<Command>.IndexKeys.Ascending(x => x.Id);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Command>(indexKeys, indexOptions);
            _collection.Indexes.CreateOne(indexModel);

            // Add additional initialization steps as needed
        }
    }
}
