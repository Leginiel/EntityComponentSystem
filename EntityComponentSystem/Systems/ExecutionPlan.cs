using System.Collections.Generic;

namespace EntityComponentSystem.Systems
{
  internal class ExecutionPlan : IExecutionPlan
  {
    public List<ISystem> Systems { get; } = new List<ISystem>();

    public void AddSystem(ISystem system)
    {
      if (!Systems.Contains(system))
        Systems.Add(system);
    }
    public void RemoveSystem(ISystem system)
    {
      Systems.Remove(system);
    }
    public void RemoveAllSystems()
    {
      foreach (ISystem system in Systems)
        system.TearDown();

      Systems.Clear();
    }
    public bool Contains(ISystem system)
    {
      return Systems.Contains(system);
    }
  }
}
