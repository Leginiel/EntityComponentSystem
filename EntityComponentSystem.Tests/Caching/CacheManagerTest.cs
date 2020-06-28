using EntityComponentSystem.Caching;
using EntityComponentSystem.Components;
using EntityComponentSystem.Tests.Components;
using System;
using Xunit;

namespace EntityComponentSystem.Tests.Caching
{
  public class CacheManagerTest
  {
    [Fact]
    public void TestAddItemToCache_ValidItem_Successful()
    {
      ICacheManager cacheManager = new CacheManager();
      UnitTestComponent component = new UnitTestComponent();

      cacheManager.AddItemToCache(component);

      Assert.True(cacheManager.HasItemInCache<UnitTestComponent>());
    }
    [Fact]
    public void TestAddItemToCache_TypeValidItem_Successful()
    {
      ICacheManager cacheManager = new CacheManager();
      IComponent component = new UnitTestComponent();

      cacheManager.AddItemToCache(component);

      Assert.True(cacheManager.HasItemInCache<UnitTestComponent>());
    }
    [Fact]
    public void TestAddItemToCache_Null_ThrowsArgumentNullException()
    {
      ICacheManager cacheManager = new CacheManager();

      Assert.Throws<ArgumentNullException>(() => cacheManager.AddItemToCache<UnitTestComponent>(null));
    }
    [Fact]
    public void TestGetItemToCache_UnittestComponentRegisteredItems_Successful()
    {
      ICacheManager cacheManager = new CacheManager();
      UnitTestComponent component = new UnitTestComponent();

      cacheManager.AddItemToCache(component);
      UnitTestComponent result = cacheManager.GetItemFromCache<UnitTestComponent>();

      Assert.False(cacheManager.HasItemInCache<UnitTestComponent>());
      Assert.Equal(component, result);
    }
    [Fact]
    public void TestAddItemToCache_UnittestComponentNoregisteredItems_NewItem()
    {
      ICacheManager cacheManager = new CacheManager();

      UnitTestComponent result = cacheManager.GetItemFromCache<UnitTestComponent>();

      Assert.False(cacheManager.HasItemInCache<UnitTestComponent>());
      Assert.NotNull(result);
      Assert.IsType<UnitTestComponent>(result);
    }
  }
}
