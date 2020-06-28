using System;
using System.Diagnostics.CodeAnalysis;

namespace EntityComponentSystem.Components
{
  [ExcludeFromCodeCoverage]
  internal class ComponentRequestEventArgs : ComponentEventArgs
  {
    public Type RequestedType { get; }
    public void SetComponent(IComponent component)
    {
      Component = component;
    }

    public ComponentRequestEventArgs(Type requestedType)
      : base(null)
    {
      RequestedType = requestedType;
    }
  }
}
