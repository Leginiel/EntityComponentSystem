using EntityComponentSystem.Components;
using System;

namespace EntityComponentSystem.Storages
{
  public interface IStorage
  {
    event EventHandler<StorageEntryChangedEventArgs> EntryChanged;
    event EventHandler<StorageEntryEventArgs> EntryAdded;
    event EventHandler<StorageEntryEventArgs> EntryRemoved;

    void AddEntry();
    void RemoveEntry(int index);
    void ChangeEntry<ComponentType>(int index, ComponentType entry)
      where ComponentType : class, IComponent, new();

    ComponentType GetEntry<ComponentType>(int index)
      where ComponentType : class, IComponent, new();

    bool IsComponentAvailable(int index);
  }
}
