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
    private readonly IExecutor executor;
    private readonly List<IEntity> entities = new List<IEntity>();

    public Context(ICacheManager cacheManager, IStorageManager storageManager, IExecutor executor)
    {
      this.cacheManager = cacheManager;
      this.storageManager = storageManager;
      this.executor = executor;
    }

    public IEntity CreateEntity()
    {
      Entity entity = cacheManager.GetItemFromCache<Entity>();

      if (entity.Id == -1)
      {
        entity.Initialize(entities.Count);
        entities.Add(entity);
        storageManager.AddDataEntry();
      }
      else
      {
        entities[entity.Id] = entity;
      }

      entity.ComponentRemoved += EntityComponentRemoved;
      entity.ComponentAdded += EntityComponentAdded;
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
      entity.ComponentAdded -= EntityComponentAdded;
      entity.ComponentRemoved -= EntityComponentRemoved;
      entity.ComponentRequested -= EntityComponentRequested;
      cacheManager.AddItemToCache(entity);
      entities[index] = null;
    }

    public void RegisterSystem<T>(ISystem system)
      where T : ITuple
    {
      CheckNull(system);
      executor.RegisterSystem(system);
    }

    public void UnregisterSystem(ISystem system)
    {
      CheckNull(system);
      system.TearDown();
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

    public void Update(double deltaTime)
    {
      executor.Execute(deltaTime);
    }

    private void EntityComponentRemoved(object sender, ComponentEventArgs e)
    {
      cacheManager.AddItemToCache(e.Component);
      ChangeStorageEntry((IEntity)sender, e.Component);
    }
    private void EntityComponentRequested(object sender, ComponentRequestEventArgs e)
    {
      MethodInfo method = cacheManager.GetType().GetMethod(nameof(CacheManager.GetItemFromCache));
      method = method.MakeGenericMethod(e.RequestedType);
      e.SetComponent((IComponent)method.Invoke(cacheManager, Array.Empty<object>()));
    }
    private void EntityComponentAdded(object sender, ComponentEventArgs e)
    {
      ChangeStorageEntry((IEntity)sender, e.Component);
    }

    private void ChangeStorageEntry(IEntity entity, IComponent component)
    {
      Type type = component.GetType();
      IStorage storage;
      MethodInfo method = storageManager.GetType().GetMethod(nameof(storageManager.GetStorage));
      MethodInfo storageMethod = typeof(IStorage).GetMethod(nameof(storage.ChangeEntry));
      method = method.MakeGenericMethod(type);
      storageMethod = storageMethod.MakeGenericMethod(type);
      storage = (IStorage)method.Invoke(storageManager, Array.Empty<object>());
      storageMethod.Invoke(storage, new object[] { entity.Id, component });
    }

    private void CheckNull(ISystem system)
    {
      if (system == null)
        throw new ArgumentNullException(nameof(system));
    }
  }
}
