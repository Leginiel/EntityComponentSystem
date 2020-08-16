using EntityComponentSystem.Components;
using EntityComponentSystem.Core;
using System.Collections.Generic;

namespace EntityComponentSystem.Entities
{
  public interface IEntity : IActivatable
  {
    int Id { get; }
    IReadOnlyList<IComponent> Components { get; }

    IComponent<ComponentType> FindComponent<ComponentType>();
  }
}
