using EntityComponentSystem.Components;

namespace EntityComponentSystem.Storages
{
  public interface IStorageFactory
  {
    IStorage CreateStorage<T>()
      where T : class, IComponent, new();

  }
}