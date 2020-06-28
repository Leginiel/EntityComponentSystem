using EntityComponentSystem.Caching;
using EntityComponentSystem.Tests.Components;
using System;
using Xunit;

namespace EntityComponentSystem.Tests.Caching
{
  public class CacheTest
  {
    [Fact]
    public void TestAdd_Null_ArgumentNullException()
    {
      Cache<UnitTestComponent> cache = new Cache<UnitTestComponent>();

      Assert.Throws<ArgumentNullException>(() => cache.Add(null));
    }
    [Fact]
    public void TestAdd_ValidObject_Successful()
    {
      Cache<UnitTestComponent> cache = new Cache<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      cache.Add(component);
      Assert.Equal(1, cache.Count);
    }
    [Fact]
    public void TestAdd_ValidObjectAsObject_Successful()
    {
      Cache<UnitTestComponent> cache = new Cache<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      cache.Add((object)component);
      Assert.Equal(1, cache.Count);
    }
    [Fact]
    public void TestGetNext_EmptyCache_NewObject()
    {
      Cache<UnitTestComponent> cache = new Cache<UnitTestComponent>();

      Assert.NotNull(cache.GetNext());
      Assert.Equal(0, cache.Count);
    }
    [Fact]
    public void TestGetNext_FilledCache_ReusedObject()
    {
      Cache<UnitTestComponent> cache = new Cache<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      cache.Add(component);
      Assert.Equal(component, cache.GetNext());
      Assert.Equal(0, cache.Count);
    }
  }
}
