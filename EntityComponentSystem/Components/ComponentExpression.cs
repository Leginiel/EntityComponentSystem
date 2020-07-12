using System;
using System.Runtime.CompilerServices;

namespace EntityComponentSystem.Components
{
  public struct ComponentExpression<T>
    where T : ITuple
  {
    public Type[] Types { get; }
    public ComponentExpressionModes Mode { get; }

    public ComponentExpression(Type[] types, ComponentExpressionModes mode)
    {
      Types = types;
      Mode = mode;
    }
  }
}
