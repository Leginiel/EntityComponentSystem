using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EntityComponentSystem.Systems
{
  internal class Executor : IExecutor
  {
    public List<IExecutionPlan> ExecutionPlans { get; } = new List<IExecutionPlan>();

    public void RegisterSystem(ISystem system)
    {
      AttributeCollection attributes = TypeDescriptor.GetAttributes(system);
      ExecuteAfterAttribute attribute = attributes[typeof(ExecuteAfterAttribute)] as ExecuteAfterAttribute;
      int index = GetExecutionPlanIndex(system, attribute?.SystemTypes);

      if (index >= ExecutionPlans.Count)
        ExecutionPlans.Add(new ExecutionPlan());

      ExecutionPlans[index].AddSystem(system);
    }

    public void UnregisterSystem(ISystem system)
    {
      int index = FindSystem(system.GetType());

      if (index >= 0)
        ExecutionPlans[index].RemoveSystem(system);
    }
    public void UnregisterAllSystems()
    {
      foreach (ExecutionPlan executionPlan in ExecutionPlans)
        executionPlan.RemoveAllSystems();
    }

    public void Execute(double deltaTime)
    {
      foreach (ExecutionPlan plan in ExecutionPlans)
      {
        Parallel.ForEach(plan.Systems, s => s.Execute(deltaTime));
      }
    }
    public bool ContainsSystem(ISystem system)
    {
      return FindSystem(system.GetType()) >= 0;
    }
    private int GetExecutionPlanIndex(ISystem system, Type[] systemTypes)
    {
      int result = FindSystem(system.GetType());

      if (systemTypes != null || result >= 0)
      {
        foreach (Type type in systemTypes)
        {
          result = Math.Max(result, FindSystem(type));
        }
      }
      return result + 1;
    }
    private int FindSystem(Type systemType)
    {
      int result = -1;
      int index = 0;
      while (result < 0 && index < ExecutionPlans.Count)
      {
        result = ExecutionPlans[index].Systems.FindIndex(s => s.GetType().Equals(systemType));
        index++;
      }
      return result;
    }
  }
}
