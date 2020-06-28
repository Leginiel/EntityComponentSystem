using EntityComponentSystem.Components;
using System;
using System.Collections.Generic;

namespace EntityComponentSystem.Storages
{
  internal class StorageManager : IStorageManager
  {
    private readonly Dictionary<Type, IStorage> storages = new Dictionary<Type, IStorage>();
    private readonly IStorageFactory factory;

    public int DataLength { get; private set; }

    public StorageManager(IStorageFactory factory)
    {
      this.factory = factory;
    }

    public void AddDataEntry()
    {
      foreach (IStorage storage in storages.Values)
      {
        storage.AddEntry();
      }
      DataLength++;
    }

    public bool Contains<T>()
      where T : class, IComponent, new()
    {
      return storages.ContainsKey(typeof(T));
    }

    public IStorage<T> GetStorage<T>()
      where T : class, IComponent, new()
    {
      Type storageType = typeof(T);
      IStorage<T> result;

      if (storages.TryGetValue(storageType, out IStorage storage))
      {
        result = (IStorage<T>)storage;
      }
      else
      {
        result = factory.CreateStorage<T>();
        storages.Add(storageType, result);
        AddDataEntriesToNewStorage(result);
      }
      return result;
    }

    private void AddDataEntriesToNewStorage<T>(IStorage<T> result)
      where T : class, IComponent, new()
    {
      for (int i = 0; i < DataLength; i++)
      {
        result.AddEntry();
      }
    }
  }
}
