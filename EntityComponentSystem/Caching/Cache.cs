using System.Collections.Concurrent;

namespace EntityComponentSystem.Caching
{
  internal class Cache<CacheType> : ICache<CacheType>
    where CacheType : class
  {
    private readonly ConcurrentBag<CacheType> cache = new ConcurrentBag<CacheType>();

    public IElementFactory<CacheType> Factory { private get; set; }
    public int Count => cache.Count;

    public CacheType GetNext()
    {
      if (!cache.TryTake(out CacheType item))
      {
        item = Factory.Create();
      }

      return item;
    }
    public void Add(CacheType item)
    {
      cache.Add(item);
    }
    public void Add(object item)
    {
      cache.Add((CacheType)item);
    }
  }
}
