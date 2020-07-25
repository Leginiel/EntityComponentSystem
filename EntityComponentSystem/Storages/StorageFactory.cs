using EntityComponentSystem.Components;

namespace EntityComponentSystem.Storages
{
  public class StorageFactory : IStorageFactory
  {
    public IStorage CreateStorage<T>()
      where T : class, IComponent, new()
    {
      return new Storage<T>();
    }
  }
}
