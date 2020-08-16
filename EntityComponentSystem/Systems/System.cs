using EntityComponentSystem.Core;
using EntityComponentSystem.Entities;
using System.Collections.Generic;

namespace EntityComponentSystem.Systems
{
  public abstract class System : ISystem
  {
    internal IEntityManager EntityManager { get; set; }

    public abstract void Execute(double deltaTime);
    public abstract void Setup();
    public virtual void TearDown()
    {
      EntityManager = null;
    }

    protected IEnumerable<IEntity> IterateAllEntities()
    {
      foreach (IEntity entity in EntityManager.Iterate(It.AllActive()))
        yield return entity;
    }
  }
}
