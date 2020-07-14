using EntityComponentSystem.Storages;
using EntityComponentSystem.Tests.Components;
using System;
using Xunit;

namespace EntityComponentSystem.Tests.Storages
{
  public class StorageTest
  {
    [Fact]
    public void AddEntry_NoParameter_Successful()
    {
      IStorage storage = new Storage<UnitTestComponent>();

      var e = Assert.Raises<StorageEntryEventArgs>(handler => storage.EntryAdded += handler,
                                                   handler => storage.EntryAdded -= handler,
                                                   () => storage.AddEntry());

      Assert.Equal(0, e.Arguments.Index);
      Assert.Null(e.Arguments.Value);
    }

    [Fact]
    public void ChangeEntry_ValidIndexValidComponent_Successful()
    {
      IStorage storage = new Storage<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      storage.AddEntry();

      var e = Assert.Raises<StorageEntryChangedEventArgs>(handler => storage.EntryChanged += handler,
                                                          handler => storage.EntryChanged -= handler,
                                                          () => storage.ChangeEntry(0, component));
      Assert.Equal(0, e.Arguments.Index);
      Assert.Null(e.Arguments.Value);
      Assert.Equal(component, e.Arguments.NewValue);
      Assert.Equal(component, storage.GetEntry<UnitTestComponent>(0));
    }
    [Fact]
    public void ChangeEntry_InvalidIndexValidComponent_IndexOutOfRangeException()
    {
      IStorage storage = new Storage<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      Assert.Throws<IndexOutOfRangeException>(() => storage.ChangeEntry(0, component));
    }
    [Fact]
    public void ChangeEntry_ValidIndexNull_Successful()
    {
      IStorage storage = new Storage<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      storage.AddEntry();
      storage.ChangeEntry(0, component);

      var e = Assert.Raises<StorageEntryChangedEventArgs>(handler => storage.EntryChanged += handler,
                                                          handler => storage.EntryChanged -= handler,
                                                          () => storage.ChangeEntry<UnitTestComponent>(0, null));
      Assert.Equal(0, e.Arguments.Index);
      Assert.Null(e.Arguments.NewValue);
      Assert.Equal(component, e.Arguments.Value);
      Assert.Null(storage.GetEntry<UnitTestComponent>(0));
    }
    [Fact]
    public void RemoveEntry_ValidIndexNonExistingComponent_Successful()
    {
      IStorage storage = new Storage<UnitTestComponent>();

      storage.AddEntry();

      var e = Assert.Raises<StorageEntryEventArgs>(handler => storage.EntryRemoved += handler,
                                                   handler => storage.EntryRemoved -= handler,
                                                   () => storage.RemoveEntry(0));
      Assert.Equal(0, e.Arguments.Index);
      Assert.Null(e.Arguments.Value);
    }
    [Fact]
    public void RemoveEntry_ValidIndexExistingComponent_Successful()
    {
      IStorage storage = new Storage<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      storage.AddEntry();
      storage.ChangeEntry(0, component);
      var e = Assert.Raises<StorageEntryEventArgs>(handler => storage.EntryRemoved += handler,
                                                   handler => storage.EntryRemoved -= handler,
                                                   () => storage.RemoveEntry(0));
      Assert.Equal(0, e.Arguments.Index);
      Assert.Equal(component, e.Arguments.Value);
    }
    [Fact]
    public void RemoveEntry_InvalidIndex_IndexOutOfRangeException()
    {
      IStorage storage = new Storage<UnitTestComponent>();

      Assert.Throws<IndexOutOfRangeException>(() => storage.RemoveEntry(0));
    }

    [Fact]
    public void GetEntry_ValidIndexNonExistingComponent_Null()
    {
      IStorage storage = new Storage<UnitTestComponent>();

      storage.AddEntry();
      Assert.Null(storage.GetEntry<UnitTestComponent>(0));
    }
    [Fact]
    public void GetEntry_ValidIndexExistingComponent_ComponentData()
    {
      IStorage storage = new Storage<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      storage.AddEntry();
      storage.ChangeEntry(0, component);
      Assert.Equal(component, storage.GetEntry<UnitTestComponent>(0));
    }
    [Fact]
    public void GetEntry_InvalidIndex_IndexOutOfRangeException()
    {
      IStorage storage = new Storage<UnitTestComponent>();

      Assert.Throws<IndexOutOfRangeException>(() => storage.GetEntry<UnitTestComponent>(0));
    }
    [Fact]
    public void IsComponentAvailable_ValidIndexNoComponent_False()
    {
      IStorage storage = new Storage<UnitTestComponent>();

      storage.AddEntry();

      Assert.False(storage.IsComponentAvailable(0));
    }
    [Fact]
    public void IsComponentAvailable_ValidIndexComponent_True()
    {
      IStorage storage = new Storage<UnitTestComponent>();
      UnitTestComponent component = new UnitTestComponent();

      storage.AddEntry();
      storage.ChangeEntry(0, component);

      Assert.True(storage.IsComponentAvailable(0));
    }
    [Fact]
    public void IsAvailable_InvalidIndex_IndexOutOfRangeException()
    {
      IStorage storage = new Storage<UnitTestComponent>();

      Assert.Throws<IndexOutOfRangeException>(() => storage.IsComponentAvailable(0));
    }
  }
}
