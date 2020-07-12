using System;
using System.Runtime.CompilerServices;

namespace EntityComponentSystem.Components
{
  public static class Is
  {
    //public static ComponentExpression<T> OneOf<T>()
    //  where T : ITuple
    //{
    //  Type[] types = GetComponentDefinitions<T>();

    //  return new ComponentExpression<T>(types, ComponentExpressionModes.Single);
    //}

    public static ComponentExpression<T> AnyOf<T>()
      where T : ITuple
    {
      Type[] types = GetComponentDefinitions<T>();

      return new ComponentExpression<T>(types, ComponentExpressionModes.Optional);
    }
    public static ComponentExpression<T> AllOf<T>()
      where T : ITuple
    {
      Type[] types = GetComponentDefinitions<T>();

      return new ComponentExpression<T>(types, ComponentExpressionModes.Required);
    }

    private static Type[] GetComponentDefinitions<T>()
      where T : ITuple
    {
      Type tupleType = typeof(T);
      return tupleType.GetGenericArguments();
    }
  }
}
