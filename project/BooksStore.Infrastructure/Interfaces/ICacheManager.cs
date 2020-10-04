using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infrastructure.Interfaces
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        bool IsSet(string key);
        void Set<T>(string key, object data, int cacheTime);
        void Remove(string key);
    }
}
