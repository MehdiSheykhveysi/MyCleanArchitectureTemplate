using App.ApplicationService.Shaared.Attributes;
using App.Domain.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace App.Infrastructure.Caching
{
    [ServiceMark]
    public class InMemoryCacheAdapter : ICacheAdapter
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheAdapter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add<T>(string key, T value)
        {
            _memoryCache.Set(key, value);
        }

        public bool Exist(string key)
        {
            return _memoryCache.TryGetValue(key, out object temp);
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
