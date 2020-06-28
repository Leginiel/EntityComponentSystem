namespace EntityComponentSystem.Caching
{
  public interface ICacheManager
  {
    bool HasItemInCache<T>()
      where T : class, new();
    void AddItemToCache<T>(T item)
      where T : class, new();
    void AddItemToCache(object item);
    T GetItemFromCache<T>()
      where T : class, new();
  }
}