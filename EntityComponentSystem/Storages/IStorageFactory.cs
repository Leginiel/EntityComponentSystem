using EntityComponentSystem.Components;

namespace EntityComponentSystem.Storages
{
  public interface IStorageFactory
  {
    IStorage<T> CreateStorage<T>()
      where T : class, IComponent, new();

  }
}