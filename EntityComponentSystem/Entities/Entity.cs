using EntityComponentSystem.Components;
using System;
using System.Collections.Generic;

namespace EntityComponentSystem.Entities
{
  [Serializable]
  internal sealed class Entity : IEntity
  {
    private readonly List<IComponent> components;

    public int Id { get; }
    public IReadOnlyList<IComponent> Components => components;
    public bool Enabled { get; set; }

    public Entity(int id)
      : this(id, new List<IComponent>()) { }

    public IComponent<ComponentType> FindComponent<ComponentType>()
    {
      return (IComponent<ComponentType>)components.Find(c => c.GetType().Equals(typeof(ComponentType)));
    }

    internal Entity(int id, List<IComponent> components)
    {
      Id = id;
      this.components = components;
    }

    internal void AddComponent(IComponent component)
    {
      components.Add(component);
    }
    internal void RemoveComponent(IComponent component)
    {
      components.Remove(component);
    }
    internal void RemoveAllComponents()
    {
      components.Clear();
    }
  }
}
