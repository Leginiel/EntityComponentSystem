using EntityComponentSystem.Components;
using EntityComponentSystem.Entities;
using EntityComponentSystem.Systems;
using System.Runtime.CompilerServices;

namespace EntityComponentSystem
{
  public interface IContext
  {
    IEntity CreateEntity();
    void DestroyEntity(IEntity entity);
    void DestroyEntity(int index);
    void DestroyAllEntities();
    void RegisterSystem<T>(ISystem system)
      where T : ITuple;
    void UnregisterSystem(ISystem system);
    void UnregisterAllSystems();
    bool ContainsSystem<T>(ISystem system)
      where T : ITuple;
    T CreateComponent<T>()
      where T : class, IComponent, new();
    void Update(double deltaTime);
  }
}
