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
    bool IsComponentAvailable(int index);
  }
  public interface IStorage<ComponentType> : IStorage
    where ComponentType : IComponent
  {

    ComponentType GetEntry(int index);
    void ChangeEntry(int index, ComponentType entry);
  }
}
