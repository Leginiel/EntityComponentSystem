using EntityComponentSystem.Core;
using System.Collections.Generic;

namespace EntityComponentSystem.Components
{
  public interface IInstancedComponent<ComponentType> : IComponent<HashSet<ComponentType>>
  {
    void AddInstance(ComponentType value);
    void RemoveInstance(ComponentType value);
    new IEnumerable<ComponentType> Iterate(IIteratorExpression expression);

  }
}
