using EntityComponentSystem.Core;
using System;
using System.Collections.Generic;

namespace EntityComponentSystem.Components
{
  [Serializable]
  internal sealed class InstancedComponent<ComponentType> : Component<HashSet<ComponentType>>, IInstancedComponent<ComponentType>
  {
    public void AddInstance(ComponentType value)
    {
      Value.Add(value);
    }

    public void RemoveInstance(ComponentType value)
    {
      Value.Remove(value);
    }
    public new IEnumerable<ComponentType> Iterate(IIteratorExpression expression)
    {
      if (expression.ExpressionMatched(this))
      {
        foreach (ComponentType value in Value)
        {
          yield return value;
        }
      }
    }
  }
}
