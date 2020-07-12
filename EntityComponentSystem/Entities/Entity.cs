using EntityComponentSystem.Components;
using System;
using System.Collections.Generic;

namespace EntityComponentSystem.Entities
{
  [Serializable]
  internal sealed class Entity : IEntity
  {
    private readonly Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();

    internal event EventHandler<ComponentEventArgs> ComponentAdded;
    internal event EventHandler<ComponentRequestEventArgs> ComponentRequested;
    internal event EventHandler<ComponentEventArgs> ComponentRemoved;

    public IReadOnlyCollection<IComponent> Components => components.Values;

    public int Id { get; private set; }

    internal void Initialize(int id)
    {
      Id = id;
    }

    public Entity()
    {
      Id = -1;
    }

    public void AddComponent<ComponentType>()
      where ComponentType : class, IComponent, new()
    {
      Type type = typeof(ComponentType);
      ComponentRequestEventArgs args;

      if (components.ContainsKey(type))
        throw new ArgumentException($"Component with type \"{type.Name}\" already exists.", nameof(ComponentType));

      args = new ComponentRequestEventArgs(typeof(ComponentType));

      ComponentRequested?.Invoke(this, args);
      components.Add(type, args.Component);
      ComponentAdded?.Invoke(this, args);
    }

    public void RemoveComponent<ComponentType>()
      where ComponentType : class, IComponent, new()
    {
      Type type = typeof(ComponentType);

      if (components.Remove(type, out IComponent component))
      {
        ComponentRemoved?.Invoke(this, new ComponentEventArgs(component));
      }
      else
      {
        throw new ArgumentException("This component does not exist on this entity", nameof(ComponentType));
      }
    }

    internal void Destroy()
    {
      if (ComponentRemoved != null)
      {
        foreach (IComponent component in Components)
          ComponentRemoved(this, new ComponentEventArgs(component));
      }

      components.Clear();
    }
  }
}
