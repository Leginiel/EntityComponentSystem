using System.Collections.Generic;

namespace EntityComponentSystem.Systems
{
  public interface IExecutor
  {
    List<IExecutionPlan> ExecutionPlans { get; }

    void Execute(double deltaTime);
    void RegisterSystem(ISystem system);
    void UnregisterSystem(ISystem system);
    void UnregisterAllSystems();
    bool ContainsSystem(ISystem system);
  }
}