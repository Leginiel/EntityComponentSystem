using EntityComponentSystem.Caching;
using EntityComponentSystem.Entities;
using EntityComponentSystem.Storages;
using EntityComponentSystem.Systems;
using EntityComponentSystem.Tests.Components;
using Moq;
using System;
using Xunit;

namespace EntityComponentSystem.Tests
{
  public class ContextTest
  {
    [Fact]
    public void TestRegisterSystem_ValidSystem_SuccessfullyRegistered()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      context.RegisterSystem<ValueTuple<UnitTestComponent>>(systemMock.Object);

      Assert.True(context.ContainsSystem<ValueTuple<UnitTestComponent>>(systemMock.Object));
    }
    [Fact]
    public void TestRegisterSystem_Null_ThrowsArgumentNullException()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      Assert.Throws<ArgumentNullException>(() => context.RegisterSystem<ValueTuple<UnitTestComponent>>(null));
    }
    [Fact]
    public void TestRegisterSystem_ValidExistingSystem_ThrowsArgumentException()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      context.RegisterSystem<ValueTuple<UnitTestComponent>>(systemMock.Object);
      Assert.Throws<ArgumentException>(() => context.RegisterSystem<ValueTuple<UnitTestComponent>>(systemMock.Object));

      Assert.True(context.ContainsSystem<ValueTuple<UnitTestComponent>>(systemMock.Object));
    }

    [Fact]
    public void TestUnregisterSystem_ValidSystem_SuccessfullyUnregistered()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      context.RegisterSystem<ValueTuple<UnitTestComponent>>(systemMock.Object);
      context.UnregisterSystem(systemMock.Object);

      Assert.False(context.ContainsSystem<ValueTuple<UnitTestComponent>>(systemMock.Object));
    }
    [Fact]
    public void TestUnregisterSystem_Null_ThrowsArgumentNullException()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      Assert.Throws<ArgumentNullException>(() => context.UnregisterSystem(null));
    }
    [Fact]
    public void TestUnregisterSystem_ValidNonExistingSystem_ThrowsArgumentException()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      Assert.Throws<ArgumentException>(() => context.UnregisterSystem(systemMock.Object));

      Assert.False(context.ContainsSystem<ValueTuple<UnitTestComponent>>(systemMock.Object));
    }
    [Fact]
    public void TestCreateEntity_NoParameter_ValidEntityFromCache()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);
      Entity entity = new Entity();

      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<Entity>()).Returns(entity);

      IEntity result = context.CreateEntity();

      Assert.Equal(entity, result);
      cacheManagerMock.Verify((cm) => cm.GetItemFromCache<Entity>(), Times.Once);
    }
    [Fact]
    public void TestCreateComponent_NoParameter_ValidComponent()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);
      UnitTestComponent component = new UnitTestComponent();

      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<UnitTestComponent>()).Returns(component);

      UnitTestComponent result = context.CreateComponent<UnitTestComponent>();

      cacheManagerMock.Verify((cm) => cm.GetItemFromCache<UnitTestComponent>(), Times.Once);
      Assert.Equal(component, result);
    }
    [Fact]
    public void TestDestroyEntityWithoutComponents_ValidIndex_EntitySuccessfullyDestroyed()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<Entity>()).Returns(new Entity());

      IEntity entity = context.CreateEntity();

      cacheManagerMock.Setup((cm) => cm.AddItemToCache(It.Is<Entity>(e => e.Equals(entity)))).Verifiable();

      context.DestroyEntity(entity.Id);

      cacheManagerMock.VerifyAll();
    }
    [Fact]
    public void TestDestroyEntityWithoutComponents_InvalidIndex_ThrowsArgumentOutOfRangeException()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      Assert.Throws<ArgumentOutOfRangeException>(() => context.DestroyEntity(1));
    }
    [Fact]
    public void TestDestroyEntityWithoutComponents_ValidEntity_EntitySuccessfullyDestroyed()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<Entity>()).Returns(new Entity());
      IEntity entity = context.CreateEntity();

      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<Entity>(e => e.Equals(entity)))).Verifiable();

      context.DestroyEntity(entity);

      cacheManagerMock.VerifyAll();
    }
    [Fact]
    public void TestDestroyEntityWithoutComponents_InvalidEntity_ThrowsArgumentNullException()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);

      Assert.Throws<ArgumentNullException>(() => context.DestroyEntity(null));
    }
    [Fact]
    public void DestroyAllEntitiesWithoutComponents_NoParameter_Successful()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<Entity>()).Returns(new Entity());

      IEntity entity = context.CreateEntity();
      IEntity entity2 = context.CreateEntity();

      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<Entity>(e => e.Equals(entity)))).Verifiable();
      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<Entity>(c => c.Equals(entity2)))).Verifiable();

      context.DestroyAllEntities();

      cacheManagerMock.VerifyAll();
    }
    [Fact]
    public void TestDestroyEntityWithComponents_ValidIndex_EntitySuccessfullyDestroyed()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IStorage> storageMock = new Mock<IStorage>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);
      UnitTestComponent component = new UnitTestComponent();
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<Entity>()).Returns(new Entity());
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<UnitTestComponent>()).Returns(component);
      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent>()).Returns(storageMock.Object);

      IEntity entity = context.CreateEntity();
      entity.AddComponent<UnitTestComponent>();

      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<Entity>(e => e.Equals(entity)))).Verifiable();
      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<object>(c => c.Equals(component)))).Verifiable();

      context.DestroyEntity(entity.Id);

      cacheManagerMock.VerifyAll();
    }
    [Fact]
    public void TestDestroyEntityWithComponents_ValidEntity_EntitySuccessfullyDestroyed()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IStorage> storageMock = new Mock<IStorage>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);
      UnitTestComponent component = new UnitTestComponent();
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<Entity>()).Returns(new Entity());
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<UnitTestComponent>()).Returns(component);
      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent>()).Returns(storageMock.Object);
      IEntity entity = context.CreateEntity();
      entity.AddComponent<UnitTestComponent>();

      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<Entity>(e => e.Equals(entity)))).Verifiable();
      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<object>(c => c.Equals(component)))).Verifiable();

      context.DestroyEntity(entity);

      cacheManagerMock.VerifyAll();
    }
    [Fact]
    public void DestroyAllEntitiesWithComponents_NoParameter_Successful()
    {
      Mock<ICacheManager> cacheManagerMock = new Mock<ICacheManager>();
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IStorage> storageMock = new Mock<IStorage>();
      Mock<IExecutor> executorMock = new Mock<IExecutor>();
      IContext context = new Context(cacheManagerMock.Object, storageManagerMock.Object, executorMock.Object);
      UnitTestComponent component = new UnitTestComponent();
      UnitTestComponent2 component2 = new UnitTestComponent2();
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<Entity>()).Returns(new Entity());
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<UnitTestComponent>()).Returns(component);
      cacheManagerMock.Setup((cm) => cm.GetItemFromCache<UnitTestComponent2>()).Returns(component2);
      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent>()).Returns(storageMock.Object);
      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent2>()).Returns(storageMock.Object);

      IEntity entity = context.CreateEntity();
      IEntity entity2 = context.CreateEntity();
      entity.AddComponent<UnitTestComponent>();
      entity2.AddComponent<UnitTestComponent2>();

      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<Entity>(e => e.Equals(entity)))).Verifiable();
      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<object>(c => c.Equals(component)))).Verifiable();
      cacheManagerMock.Setup(cm => cm.AddItemToCache(It.Is<object>(c => c.Equals(component2)))).Verifiable();

      context.DestroyAllEntities();

      cacheManagerMock.VerifyAll();
    }
  }
}
