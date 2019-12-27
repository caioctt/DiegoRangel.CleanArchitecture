using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context
{
    public interface IMongoClient
    {
        IMongoDatabase GetDatabase();
    }
}