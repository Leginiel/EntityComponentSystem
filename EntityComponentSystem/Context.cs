using EntityComponentSystem.Caching;
using EntityComponentSystem.Components;
using EntityComponentSystem.Entities;
using EntityComponentSystem.Storages;
using EntityComponentSystem.Systems;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EntityComponentSystem
{
  public class Context : IContext
  {
    private readonly ICacheManager cacheManager;
    private readonly IStorageManager storageManager;
    private readonly List<IEntity> entities = new List<IEntity>();
    private readonly List<ISystem> systems = new List<ISystem>();

    public Context(ICacheManager cacheManager, IStorageManager storageManager)
    {
      this.cacheManager = cacheManager;
      this.storageManager = storageManager;
    }

    public IEntity CreateEntity()
    {
      Entity entity = cacheManager.GetItemFromCache<Entity>();

      if (entity.Id == -1)
      {
        entity.Initialize(entities.Count);
        entities.Add(entity);
      }
      else
      {
        entities[entity.Id] = entity;
      }

      entity.ComponentRemoved += EntityComponentRemoved;
      entity.ComponentRequested += EntityComponentRequested;

      return entity;
    }



    public T CreateComponent<T>()
      where T : class, IComponent, new()
    {
      return cacheManager.GetItemFromCache<T>();
    }

    public void DestroyAllEntities()
    {
      for (int i = 0; i < entities.Count; i++)
      {
        DestroyEntity(i);
      }
    }

    public void DestroyEntity(IEntity entity)
    {
      if (entity == null)
        throw new ArgumentNullException();

      DestroyEntity(entity.Id);
    }

    public void DestroyEntity(int index)
    {
      if (index < 0 || index >= entities.Count)
      {
        throw new ArgumentOutOfRangeException();
      }
      Entity entity = (Entity)entities[index];
      entity.Destroy();
      entity.ComponentRemoved -= EntityComponentRemoved;
      cacheManager.AddItemToCache(entity);
      entities[index] = null;
    }

    public void RegisterSystem<T>(ISystem system)
      where T : ITuple
    {
      CheckNull(system);

      if (!systems.Contains(system))
      {
        systems.Add(system);
      }
      else
      {
        throw new ArgumentException("The provided system is already registered in the context.");
      }
    }

    public void UnregisterSystem<T>(ISystem system)
      where T : ITuple
    {
      CheckNull(system);

      if (!systems.Remove(system))
      {
        throw new ArgumentException("The provided system wasn't registered in the context.");
      }
    }

    public bool ContainsSystem<T>(ISystem system)
      where T : ITuple
    {
      return systems.Contains(system);
    }

    private void EntityComponentRemoved(object sender, ComponentEventArgs e)
    {
      cacheManager.AddItemToCache(e.Component);
    }
    private void EntityComponentRequested(object sender, ComponentRequestEventArgs e)
    {
      MethodInfo method = cacheManager.GetType().GetMethod(nameof(CacheManager.GetItemFromCache));
      method = method.MakeGenericMethod(e.RequestedType);
      e.SetComponent((IComponent)method.Invoke(cacheManager, Array.Empty<object>()));
    }
    private void CheckNull(ISystem system)
    {
      if (system == null)
        throw new ArgumentNullException(nameof(system));
    }
  }
}
