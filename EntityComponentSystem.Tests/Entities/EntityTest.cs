using EntityComponentSystem.Components;
using EntityComponentSystem.Entities;
using EntityComponentSystem.Tests.Components;
using System;
using Xunit;

namespace EntityComponentSystem.Tests.Entities
{
  public class EntityTest
  {
    [Fact]
    public void TestAddComponent_ValidNonExistingComponent_ComponentSuccessfullyAdded()
    {
      Entity entity = new Entity();
      UnitTestComponent component = new UnitTestComponent();
      void handler(object sender, ComponentRequestEventArgs args) => args.SetComponent(component);

      entity.ComponentRequested += handler;

      var added = Assert.RaisesAny<ComponentEventArgs>(handler => entity.ComponentAdded += handler,
                                                       handler => entity.ComponentAdded -= handler,
                                                       () => entity.AddComponent<UnitTestComponent>());
      entity.ComponentRequested -= handler;
      Assert.Single(entity.Components);
      Assert.Contains(component, entity.Components);
      Assert.Equal(component, added.Arguments.Component);
    }
    [Fact]
    public void TestAddComponent_ValidExistingComponent_ThrowsArgumentException()
    {
      Entity entity = new Entity();
      UnitTestComponent component = new UnitTestComponent();
      void handler(object sender, ComponentRequestEventArgs args) => args.SetComponent(component);

      entity.ComponentRequested += handler;
      entity.AddComponent<UnitTestComponent>();
      Assert.Throws<ArgumentException>(() => entity.AddComponent<UnitTestComponent>());

      entity.ComponentRequested -= handler;
      Assert.Single(entity.Components);
      Assert.Contains(component, entity.Components);
    }
    [Fact]
    public void TestRemoveComponent_ValidExistingComponent_ComponentRemoveSuccessfully()
    {
      Entity entity = new Entity();
      UnitTestComponent component = new UnitTestComponent();
      void handler(object sender, ComponentRequestEventArgs args) => args.SetComponent(component);

      entity.ComponentRequested += handler;
      entity.AddComponent<UnitTestComponent>();

      var e = Assert.Raises<ComponentEventArgs>(handler => entity.ComponentRemoved += handler,
                                                handler => entity.ComponentRemoved -= handler,
                                                () => entity.RemoveComponent<UnitTestComponent>());
      entity.ComponentRequested -= handler;
      Assert.Empty(entity.Components);
    }
    [Fact]
    public void TestRemoveComponent_ValidNonExistingComponent_ThrowArgumentException()
    {
      Entity entity = new Entity();

      Assert.Throws<ArgumentException>(() => entity.RemoveComponent<UnitTestComponent>());
    }
    [Fact]
    public void TestDestroy_AllEventsThrown()
    {
      Entity entity = new Entity();
      UnitTestComponent component = new UnitTestComponent();
      void handler(object sender, ComponentRequestEventArgs args) => args.SetComponent(component);

      entity.ComponentRequested += handler;
      entity.AddComponent<UnitTestComponent>();
      var e = Assert.Raises<ComponentEventArgs>(handler => entity.ComponentRemoved += handler,
                                                handler => entity.ComponentRemoved -= handler,
                                                () => entity.Destroy());

      entity.ComponentRequested -= handler;

      Assert.Empty(entity.Components);
      Assert.Equal(component, e.Arguments.Component);
    }
  }
}
