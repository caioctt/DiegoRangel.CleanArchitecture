using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Cache
{
    public interface ICacheService
    {
        object GetEntry(string key);
        Task<TItem> GetOrCreateAsync<TItem>(string key, Func<ICacheEntry, Task<TItem>> factory);
        void RemoveEntry(string key);
    }
}