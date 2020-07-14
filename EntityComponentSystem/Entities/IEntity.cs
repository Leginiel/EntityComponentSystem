using EntityComponentSystem.Components;
using System.Collections.Generic;

namespace EntityComponentSystem.Entities
{
  public interface IEntity
  {
    IReadOnlyCollection<IComponent> Components { get; }
    int Id { get; }

    void AddComponent<ComponentType>()
      where ComponentType : class, IComponent, new();
    ComponentType GetComponent<ComponentType>()
      where ComponentType : class, IComponent, new();
    void RemoveComponent<ComponentType>()
      where ComponentType : class, IComponent, new();
  }
}
