using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Mappings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context
{
    public class MongoDbContext : IMongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly IList<Func<Task>> _commands;

        public MongoDbContext(IMongoClient mongoClient, IMongoMapper mongoMapper)
        {
            _database = mongoClient.GetDatabase();
            _commands = new List<Func<Task>>();
            mongoMapper.ApplyMappings();

            RegisterConventions();
        }

        public void RegisterConventions()
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public async Task<int> SaveChangesAsync()
        {
            var commands = _commands.Select(c => c());
            var count = _commands.Count;

            await Task.WhenAll(commands);
            _commands.Clear();

            return count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}