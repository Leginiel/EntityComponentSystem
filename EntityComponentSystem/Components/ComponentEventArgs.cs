using System;
using System.Diagnostics.CodeAnalysis;

namespace EntityComponentSystem.Components
{
  [ExcludeFromCodeCoverage]
  internal class ComponentEventArgs : EventArgs
  {
    public IComponent Component { get; protected set; }

    public ComponentEventArgs(IComponent component)
    {
      Component = component;
    }
  }
}