using EntityComponentSystem.Components;
using EntityComponentSystem.Storages;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EntityComponentSystem.Systems
{
  [Serializable]
  public abstract class System<T> : ISystem
    where T : ITuple
  {
    private readonly ComponentExpression<T> storageViewFilter;

    protected IStorageManager StorageManager { get; }
    protected Func<IStorageManager, IEnumerable<T>> StorageView { get; private set; }

    public System(IStorageManager storageManager, ComponentExpression<T> storageViewFilter)
    {
      StorageManager = storageManager;
      this.storageViewFilter = storageViewFilter;
    }

    public abstract void Execute();
    public virtual void Setup()
    {
      StorageView = StorageViewBuilder.CreateNewStorageView(storageViewFilter);
    }

    public virtual void TearDown()
    {
      StorageView = null;
    }
  }
}
