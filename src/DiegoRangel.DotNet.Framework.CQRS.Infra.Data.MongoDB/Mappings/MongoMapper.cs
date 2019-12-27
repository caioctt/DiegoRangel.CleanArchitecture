using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using MongoDB.Bson.Serialization;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Mappings
{
    public class MongoMapper : IMongoMapper
    {
        public void ApplyMappings()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(Entity)))
                return;

            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.MapIdMember(x => x.Id);
                cm.UnmapMember(x => x.ValidationResult);
            });
        }
    }
}