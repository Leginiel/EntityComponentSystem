using EntityComponentSystem.Components;
using EntityComponentSystem.Entities;
using EntityComponentSystem.Systems;
using System.Runtime.CompilerServices;

namespace EntityComponentSystem
{
  public interface IContext
  {
    bool ContainsSystem<T>(ISystem system) where T : ITuple;
    IComponent CreateComponent<ComponentType>(ComponentType value, IEntity entity, bool isInstanced);
    IEntity CreateEntity();
    void DestroyAllComponent(IEntity entity);
    void DestroyAllEntities();
    void DestroyComponent(IComponent component, IEntity entity);
    void DestroyEntity(IEntity entity);
    void RegisterSystem(ISystem system);
    void UnregisterSystem(ISystem system);
    void UnregisterAllSystems();
  }
}