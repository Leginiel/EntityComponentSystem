using EntityComponentSystem.Core;
using System.Collections.Generic;

namespace EntityComponentSystem.Components
{
  public interface IComponent : IActivatable
  {
  }

  public interface IComponent<ComponentType> : IComponent
  {
    ComponentType Value { get; set; }
    IEnumerable<ComponentType> Iterate(IIteratorExpression expression);
  }
}
