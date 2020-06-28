using EntityComponentSystem.Storages;
using EntityComponentSystem.Tests.Components;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EntityComponentSystem.Tests.Storages
{
  public class StorageViewBuilderTest
  {
    [Fact]
    public void TestCreateNewSingleStorageView_4ResultsExpected()
    {
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IStorage<UnitTestComponent>> storageMock = new Mock<IStorage<UnitTestComponent>>();
      UnitTestComponent component = new UnitTestComponent();
      int number = 0;
      List<UnitTestComponent> resultingComponents = new List<UnitTestComponent>();

      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent>()).Returns(storageMock.Object);
      storageManagerMock.Setup(sm => sm.DataLength).Returns(5);
      storageMock.Setup(s => s.GetEntry(It.IsIn(0, 1, 2, 3))).Returns(component);

      Func<IStorageManager, IEnumerable<ValueTuple<UnitTestComponent>>> storageView = StorageViewBuilder.CreateNewStorageView<UnitTestComponent>();
      foreach (ValueTuple<UnitTestComponent> result in storageView(storageManagerMock.Object))
      {
        resultingComponents.Add(result.Item1);
        number++;
      }

      storageMock.Verify(s => s.GetEntry(It.IsAny<int>()), Times.Exactly(5));

      Assert.Equal(4, number);
      foreach (UnitTestComponent comp in resultingComponents)
      {
        Assert.Equal(component, comp);
      }
    }
    [Fact]
    public void TestCreateNewMultipleStorageView_OnlyOneResultExpected()
    {
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IStorage<UnitTestComponent>> storageMock = new Mock<IStorage<UnitTestComponent>>();
      Mock<IStorage<UnitTestComponent2>> storage2Mock = new Mock<IStorage<UnitTestComponent2>>();
      UnitTestComponent component = new UnitTestComponent();
      UnitTestComponent2 component2 = new UnitTestComponent2();
      int number = 0;
      List<ValueTuple<UnitTestComponent, UnitTestComponent2>> resultingComponents = new List<ValueTuple<UnitTestComponent, UnitTestComponent2>>();

      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent2>()).Returns(storage2Mock.Object);
      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent>()).Returns(storageMock.Object);
      storageManagerMock.Setup(sm => sm.DataLength).Returns(5);
      storageMock.Setup(s => s.GetEntry(It.IsIn(0, 1, 4))).Returns(component);
      storage2Mock.Setup(s => s.GetEntry(It.IsIn(1, 2, 3))).Returns(component2);

      Func<IStorageManager, IEnumerable<ValueTuple<UnitTestComponent, UnitTestComponent2>>> storageView = StorageViewBuilder.CreateNewStorageView<UnitTestComponent, UnitTestComponent2>();
      foreach (ValueTuple<UnitTestComponent, UnitTestComponent2> result in storageView(storageManagerMock.Object))
      {
        resultingComponents.Add(result);
        number++;
      }

      storageMock.Verify(s => s.GetEntry(It.IsAny<int>()), Times.Exactly(5));
      storage2Mock.Verify(s => s.GetEntry(It.IsAny<int>()), Times.Exactly(3));

      Assert.Equal(1, number);
      Assert.Equal(new ValueTuple<UnitTestComponent, UnitTestComponent2>(component, component2), resultingComponents[0]);
    }
    [Fact]
    public void TestCreateNewMultipleStorageView_NoResultsExpected()
    {
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<IStorage<UnitTestComponent>> storageMock = new Mock<IStorage<UnitTestComponent>>();
      Mock<IStorage<UnitTestComponent2>> storage2Mock = new Mock<IStorage<UnitTestComponent2>>();
      UnitTestComponent component = new UnitTestComponent();
      UnitTestComponent2 component2 = new UnitTestComponent2();
      int number = 0;
      List<ValueTuple<UnitTestComponent, UnitTestComponent2>> resultingComponents = new List<ValueTuple<UnitTestComponent, UnitTestComponent2>>();

      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent2>()).Returns(storage2Mock.Object);
      storageManagerMock.Setup(sm => sm.GetStorage<UnitTestComponent>()).Returns(storageMock.Object);
      storageManagerMock.Setup(sm => sm.DataLength).Returns(5);
      storageMock.Setup(s => s.GetEntry(It.IsIn(0, 1, 4))).Returns(component);
      storage2Mock.Setup(s => s.GetEntry(It.IsIn(2, 3))).Returns(component2);

      Func<IStorageManager, IEnumerable<ValueTuple<UnitTestComponent, UnitTestComponent2>>> storageView = StorageViewBuilder.CreateNewStorageView<UnitTestComponent, UnitTestComponent2>();
      foreach (ValueTuple<UnitTestComponent, UnitTestComponent2> result in storageView(storageManagerMock.Object))
      {
        resultingComponents.Add(result);
        number++;
      }

      storageMock.Verify(s => s.GetEntry(It.IsAny<int>()), Times.Exactly(5));
      storage2Mock.Verify(s => s.GetEntry(It.IsAny<int>()), Times.Exactly(3));

      Assert.Equal(0, number);
      Assert.Empty(resultingComponents);
    }

  }
}