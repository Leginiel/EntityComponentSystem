using EntityComponentSystem.Storages;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EntityComponentSystem.Systems
{
  public abstract class System<T> : ISystem
    where T : ITuple
  {
    protected IStorageManager StorageManager { get; }
    protected Func<IStorageManager, IEnumerable<T>> StorageView { get; private set; }

    public System(IStorageManager storageManager)
    {
      StorageManager = storageManager;
    }

    public abstract void Execute();
    public virtual void Setup()
    {
      StorageView = StorageViewBuilder.CreateNewStorageView<T>();
    }

    public virtual void TearDown()
    {
      StorageView = null;
    }
  }
}
