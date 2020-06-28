using EntityComponentSystem.Components;

namespace EntityComponentSystem.Storages
{
  internal interface IStorageView
  {
    bool Active { get; }
    void Setup();
    void Destroy();
  }

  internal interface IStorageView<T> : IStorageView
    where T : class, IComponent, new()
  {
    T GetEntry(int index);
  }
  internal interface IStorageView<T, T1> : IStorageView
    where T : class, IComponent, new()
    where T1 : class, IComponent, new()
  {
    (T, T1) GetEntry(int index);
  }
  internal interface IStorageView<T, T1, T2> : IStorageView
    where T : class, IComponent, new()
    where T1 : class, IComponent, new()
    where T2 : class, IComponent, new()
  {
    (T, T1, T2) GetEntry(int index);
  }
  internal interface IStorageView<T, T1, T2, T3> : IStorageView
    where T : class, IComponent, new()
    where T1 : class, IComponent, new()
    where T2 : class, IComponent, new()
    where T3 : class, IComponent, new()
  {
    (T, T1, T2, T3) GetEntry(int index);
  }
  internal interface IStorageView<T, T1, T2, T3, T4> : IStorageView
    where T : class, IComponent, new()
    where T1 : class, IComponent, new()
    where T2 : class, IComponent, new()
    where T3 : class, IComponent, new()
    where T4 : class, IComponent, new()
  {
    (T, T1, T2, T3, T4) GetEntry(int index);
  }
}
