using EntityComponentSystem.Components;
using EntityComponentSystem.Entities;
using EntityComponentSystem.Systems;
using System.Runtime.CompilerServices;
using EcsSystem = EntityComponentSystem.Systems.System;

namespace EntityComponentSystem
{
  public class Context : IContext
  {
    private readonly IEntityManager entityManager;
    private readonly IComponentElementFactory factory;
    private readonly IExecutor executor;

    public Context(IEntityManager entityManager, IComponentElementFactory factory, IExecutor executor)
    {
      this.entityManager = entityManager;
      this.factory = factory;
      this.executor = executor;
    }

    public IEntity CreateEntity()
    {
      return entityManager.CreateEntity();
    }
    public void DestroyAllEntities()
    {
      entityManager.DestroyAllEntities();
    }

    public void DestroyEntity(IEntity entity)
    {
      entityManager.DestroyEntity(entity);
    }

    public IComponent CreateComponent<ComponentType>(ComponentType value, IEntity entity, bool isInstanced)
    {
      IComponent result = (isInstanced) ? factory.CreateInstanced(value) : factory.Create(value);
      ((Entity)entity).AddComponent(result);
      return result;
    }

    public void DestroyComponent(IComponent component, IEntity entity)
    {
      ((Entity)entity).RemoveComponent(component);
    }
    public void DestroyAllComponent(IEntity entity)
    {
      ((Entity)entity).RemoveAllComponents();
    }


    public void RegisterSystem(ISystem system)
    {
      EcsSystem sys = (EcsSystem)system;
      sys.EntityManager = entityManager;
      executor.RegisterSystem(system);
    }

    public void UnregisterSystem(ISystem system)
    {
      executor.UnregisterSystem(system);
    }
    public void UnregisterAllSystems()
    {
      executor.UnregisterAllSystems();
    }
    public bool ContainsSystem<T>(ISystem system)
      where T : ITuple
    {
      return executor.ContainsSystem(system);
    }

  }
}
