using EntityComponentSystem.Core;
using System.Collections.Generic;

namespace EntityComponentSystem.Entities
{
  public interface IEntityManager
  {
    HashSet<IEntity> Entities { get; }

    IEntity CreateEntity();
    void DestroyAllEntities();
    void DestroyEntity(IEntity entity);
    IEnumerable<IEntity> Iterate(IIteratorExpression expression);
  }
}