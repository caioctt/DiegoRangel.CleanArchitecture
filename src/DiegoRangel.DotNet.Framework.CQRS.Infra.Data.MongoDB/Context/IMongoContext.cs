using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context
{
    public interface IMongoContext : IDisposable
    {
        void RegisterConventions();
        void AddCommand(Func<Task> func);
        Task<int> SaveChangesAsync();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}