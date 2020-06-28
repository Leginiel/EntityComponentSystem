using EntityComponentSystem.Components;

namespace EntityComponentSystem.Storages
{
  public class StorageEntryChangedEventArgs : StorageEntryEventArgs
  {
    public IComponent NewValue { get; }

    public StorageEntryChangedEventArgs(int index, IComponent oldValue, IComponent newValue)
      : base(index, oldValue)
    {
      NewValue = newValue;
    }
  }
}
