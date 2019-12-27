using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Settings;
using MongoDB.Driver;
using mongo = MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context
{
    public class MongoClient : IMongoClient
    {
        private readonly MongoSettings _mongoSettings;

        public MongoClient(MongoSettings mongoSettings)
        {
            _mongoSettings = mongoSettings;
        }

        public IMongoDatabase GetDatabase()
        {
            var client = new mongo.MongoClient(_mongoSettings.Connection);
            return client.GetDatabase(_mongoSettings.ApplicationDatabase);
        }
    }
}