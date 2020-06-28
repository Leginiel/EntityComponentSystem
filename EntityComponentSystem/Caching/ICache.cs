namespace EntityComponentSystem.Caching
{
  internal interface ICache
  {
    int Count { get; }
    void Add(object item);
  }
  internal interface ICache<CacheType> : ICache
    where CacheType : class, new()
  {

    void Add(CacheType item);
    CacheType GetNext();
  }
}