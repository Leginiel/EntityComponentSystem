using System.Collections.Generic;

namespace EntityComponentSystem.Systems
{
  public interface IExecutionPlan
  {
    List<ISystem> Systems { get; }

    void AddSystem(ISystem system);
    bool Contains(ISystem system);
    void RemoveAllSystems();
    void RemoveSystem(ISystem system);
  }
}