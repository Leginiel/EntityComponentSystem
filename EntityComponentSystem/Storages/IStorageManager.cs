using EntityComponentSystem.Components;

namespace EntityComponentSystem.Storages
{
  public interface IStorageManager
  {
    int DataLength { get; }
    bool Contains<T>()
      where T : class, IComponent, new();
    IStorage<T> GetStorage<T>()
      where T : class, IComponent, new();
    void AddDataEntry();
  }
}
