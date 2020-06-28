using EntityComponentSystem.Components;

namespace EntityComponentSystem.Storages
{
  public class StorageFactory : IStorageFactory
  {
    public IStorage<T> CreateStorage<T>()
      where T : class, IComponent, new()
    {
      return new Storage<T>();
    }
  }
}
