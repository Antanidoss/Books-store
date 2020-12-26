using BooksStore.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infrastructure.Implementation
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            return (T)_memoryCache.Get(key);
        }

        public bool IsSet(string key)
        {
            return _memoryCache.Get(key) != null;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void Set<T>(string key, object data, int cacheTime)
        {
            if(string.IsNullOrEmpty(key) | data == null | cacheTime <= 0)
            {
                return;
            }

            var memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheTime));
            _memoryCache.Set(key, data, memoryCacheEntryOptions);
        }
    }
}
