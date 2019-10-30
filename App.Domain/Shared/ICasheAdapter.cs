namespace App.Domain.Shared
{
    public interface ICacheAdapter
    {
        bool Exist(string key);
        void Add<TValue>(string key, TValue value);
        TValue Get<TValue>(string key);
        void Remove(string key);
        void AddOrUpdate<TValue>(string key, TValue value);
        void Update<TValue>(string key, TValue value);
    }
}