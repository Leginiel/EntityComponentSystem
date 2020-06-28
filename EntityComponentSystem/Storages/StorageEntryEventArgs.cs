using EntityComponentSystem.Components;
using System;

namespace EntityComponentSystem.Storages
{
  public class StorageEntryEventArgs : EventArgs
  {
    public int Index { get; }
    public IComponent Value { get; }

    public StorageEntryEventArgs(int index, IComponent value = null)
    {
      Index = index;
      Value = value;
    }
  }
}
