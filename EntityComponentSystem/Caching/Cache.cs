using System;
using System.Collections.Concurrent;

namespace EntityComponentSystem.Caching
{
  internal class Cache<CacheType> : ICache<CacheType>
    where CacheType : class, new()
  {
    private readonly ConcurrentBag<CacheType> cache = new ConcurrentBag<CacheType>();

    public int Count => cache.Count;
    public CacheType GetNext()
    {
      if (!cache.TryTake(out CacheType item))
      {
        item = new CacheType();
      }

      return item;
    }
    public void Add(CacheType item)
    {
      if (item == null)
        throw new ArgumentNullException();

      cache.Add(item);
    }
    public void Add(object item)
    {
      if (item == null)
        throw new ArgumentNullException();

      cache.Add((CacheType)item);
    }
  }
}
