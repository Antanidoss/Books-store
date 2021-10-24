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
