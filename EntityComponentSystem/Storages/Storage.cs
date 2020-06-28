using EntityComponentSystem.Components;
using System;
using System.Collections.Generic;

namespace EntityComponentSystem.Storages
{
  internal class Storage<ComponentType> : IStorage<ComponentType>
    where ComponentType : class, IComponent, new()
  {
    private readonly List<ComponentType> storage = new List<ComponentType>();

    public event EventHandler<StorageEntryChangedEventArgs> EntryChanged;
    public event EventHandler<StorageEntryEventArgs> EntryAdded;
    public event EventHandler<StorageEntryEventArgs> EntryRemoved;

    public void AddEntry()
    {
      storage.Add(null);
      EntryAdded?.Invoke(this, new StorageEntryEventArgs(storage.Count - 1));
    }

    public void ChangeEntry(int index, ComponentType entry)
    {
      CheckBounds(index);
      IComponent oldValue = storage[index];
      storage[index] = entry;

      EntryChanged?.Invoke(this, new StorageEntryChangedEventArgs(index, oldValue, entry));
    }
    public ComponentType GetEntry(int index)
    {
      CheckBounds(index);

      return storage[index];
    }

    public bool IsComponentAvailable(int index)
    {
      CheckBounds(index);

      return storage[index] != null;
    }

    public void RemoveEntry(int index)
    {
      CheckBounds(index);

      IComponent oldValue = storage[index];
      storage[index] = null;
      EntryRemoved?.Invoke(this, new StorageEntryEventArgs(index, oldValue));
    }

    private void CheckBounds(int index)
    {
      if (index < 0 || index >= storage.Count)
        throw new IndexOutOfRangeException();
    }
  }
}
