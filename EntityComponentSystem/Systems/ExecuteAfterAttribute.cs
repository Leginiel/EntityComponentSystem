using System;
using System.Linq;

namespace EntityComponentSystem.Systems
{
  [AttributeUsage(AttributeTargets.Class)]
  public class ExecuteAfterAttribute : Attribute
  {
    public Type[] SystemTypes { get; }

    public ExecuteAfterAttribute(params Type[] systemTypes)
    {
      if (!systemTypes.All(t => typeof(ISystem).IsAssignableFrom(t)))
      {
        throw new ArgumentException($"All provided types are required to implement {nameof(ISystem)}.", nameof(systemTypes));
      }

      SystemTypes = systemTypes;
    }
  }
}
