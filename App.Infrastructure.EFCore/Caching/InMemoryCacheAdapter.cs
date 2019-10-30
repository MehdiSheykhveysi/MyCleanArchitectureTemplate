using App.ApplicationService.Shaared.Attributes;
using App.Domain.Shared;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace App.Infrastructure.Caching
{
    [ServiceMark]
    public class InMemoryCacheAdapter : ICacheAdapter, IDisposable
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheAdapter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        ~InMemoryCacheAdapter()
        {
            Dispose();
        }

        public void Add<TValue>(string key, TValue value)
        {
            _memoryCache.Set(key, value);
        }

        public void Dispose()
        {
            _memoryCache.Dispose();
        }

        public bool Exist(string key)
        {
            return _memoryCache.TryGetValue(key, out object temp);
        }

        public TValue Get<TValue>(string key)
        {
            return _memoryCache.Get<TValue>(key);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void Update<TValue>(string key, TValue value)
        {
            if (Exist(key))
            {
                Remove(key);
                Add(key, value);
            }
        }

        public void AddOrUpdate<TValue>(string key, TValue value)
        {
            if (Exist(key))
            {
                Update(key, value);
            }
            else
            {
                Add(key, value);
            }
        }
    }
}
