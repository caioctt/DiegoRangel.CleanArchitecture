using System;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public object GetEntry(string key)
        {
            return _memoryCache.TryGetValue(key, out var obj) ? obj : null;
        }

        public async Task<TItem> GetOrCreateAsync<TItem>(string key, Func<ICacheEntry, Task<TItem>> factory)
        {
            if (_memoryCache.TryGetValue(key, out var obj)) return (TItem)obj;

            var entry = _memoryCache.CreateEntry(key);
            obj = await factory(entry);
            entry.SetValue(obj);

            entry.Dispose();
            return (TItem)obj;
        }

        public void RemoveEntry(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}