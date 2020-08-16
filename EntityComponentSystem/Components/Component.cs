using EntityComponentSystem.Core;
using System;
using System.Collections.Generic;

namespace EntityComponentSystem.Components
{
  [Serializable]
  internal class Component<ComponentType> : IComponent<ComponentType>
  {
    public ComponentType Value { get; set; }

    public bool Enabled { get; set; }

    public IEnumerable<ComponentType> Iterate(IIteratorExpression expression)
    {
      if (expression.ExpressionMatched(this))
      {
        yield return Value;
      }
    }
  }
}
