using System;
using System.Collections.Generic;

namespace EntityComponentSystem.Caching
{
  internal class CacheManager : ICacheManager
  {
    private readonly Dictionary<Type, ICache> caches = new Dictionary<Type, ICache>();

    public bool HasItemInCache<T>()
      where T : class, new()
    {
      ICache cache = GetCache(typeof(T));

      return cache.Count > 0;
    }
    public void AddItemToCache<T>(T item)
      where T : class, new()
    {
      AddItemToCache((object)item);
    }
    public void AddItemToCache(object item)
    {
      if (item == null)
        throw new ArgumentNullException();

      ICache cache = GetCache(item.GetType());
      cache.Add(item);
    }
    public T GetItemFromCache<T>()
      where T : class, new()
    {
      ICache<T> cache = (ICache<T>)GetCache(typeof(T));

      return cache.GetNext();
    }

    private ICache GetCache(Type cacheType)
    {
      return (caches.ContainsKey(cacheType)) ? caches[cacheType] : CreateCache(cacheType);
    }
    private ICache CreateCache(Type cacheType)
    {
      Type generic = typeof(Cache<>);
      Type constructed = generic.MakeGenericType(cacheType);
      ICache result = (ICache)Activator.CreateInstance(constructed);

      caches.Add(cacheType, result);
      return result;
    }
  }
}
