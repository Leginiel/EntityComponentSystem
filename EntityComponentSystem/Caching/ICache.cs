namespace EntityComponentSystem.Caching
{
  internal interface ICache<CacheType>
    where CacheType : class
  {
    IElementFactory<CacheType> Factory { set; }

    void Add(CacheType item);
    CacheType GetNext();
  }
}