using EntityComponentSystem.Caching;
using EntityComponentSystem.Core;
using System.Collections.Generic;
using System.Linq;

namespace EntityComponentSystem.Entities
{
  internal class EntityManager : IEntityManager
  {
    private readonly ICache<IEntity> cache;
    public HashSet<IEntity> Entities { get; }


    public EntityManager(ICache<IEntity> cache, IElementFactory<IEntity> factory)
    {
      this.cache = cache;
      this.cache.Factory = factory;
    }

    public IEntity CreateEntity()
    {
      return cache.GetNext();
    }
    public void DestroyEntity(IEntity entity)
    {
      ((Entity)entity).RemoveAllComponents();
      Entities.Remove(entity);
      cache.Add(entity);
    }
    public void DestroyAllEntities()
    {
      while (Entities.Count > 0)
      {
        DestroyEntity(Entities.First());
      }
    }
    public IEnumerable<IEntity> Iterate(IIteratorExpression expression)
    {
      foreach (IEntity entity in Entities)
      {
        if (expression.ExpressionMatched(entity))
        {
          yield return entity;
        }
      }
    }
  }
}
