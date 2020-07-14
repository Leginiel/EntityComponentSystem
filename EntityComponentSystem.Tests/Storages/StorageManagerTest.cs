using EntityComponentSystem.Storages;
using EntityComponentSystem.Tests.Components;
using Moq;
using Xunit;

namespace EntityComponentSystem.Tests.Storages
{
  public class StorageManagerTest
  {
    [Fact]
    public void TestGetStorage_NoParameter_ValidStorage()
    {
      Mock<IStorage> storageMock = new Mock<IStorage>();
      Mock<IStorageFactory> storageFactoryMock = new Mock<IStorageFactory>();
      IStorageManager storageManager = new StorageManager(storageFactoryMock.Object);

      storageFactoryMock.Setup(sm => sm.CreateStorage<UnitTestComponent>()).Returns(storageMock.Object);

      IStorage result = storageManager.GetStorage<UnitTestComponent>();

      Assert.True(storageManager.Contains<UnitTestComponent>());
      Assert.Equal(storageMock.Object, result);
    }
    [Fact]
    public void TestGetStorage_NewStorageAfterDataExisitngAlready_Null()
    {
      Mock<IStorage> storageMock = new Mock<IStorage>();
      Mock<IStorage> storageMock2 = new Mock<IStorage>();
      Mock<IStorageFactory> storageFactoryMock = new Mock<IStorageFactory>();
      IStorageManager storageManager = new StorageManager(storageFactoryMock.Object);

      storageFactoryMock.Setup(sm => sm.CreateStorage<UnitTestComponent>()).Returns(storageMock.Object);
      storageFactoryMock.Setup(sm => sm.CreateStorage<UnitTestComponent2>()).Returns(storageMock2.Object);
      storageManager.GetStorage<UnitTestComponent>();
      storageManager.AddDataEntry();
      storageManager.AddDataEntry();

      IStorage result = storageManager.GetStorage<UnitTestComponent2>();

      storageMock2.Verify(s => s.AddEntry(), Times.Exactly(2));

      Assert.NotNull(result);
      Assert.True(storageManager.Contains<UnitTestComponent2>());
      Assert.Equal(storageMock2.Object, result);
    }
    [Fact]
    public void AddDataEntry_NoParameter_DataEntryAdded()
    {
      Mock<IStorage> storageMock = new Mock<IStorage>();
      Mock<IStorageFactory> storageFactoryMock = new Mock<IStorageFactory>();
      IStorageManager storageManager = new StorageManager(storageFactoryMock.Object);

      storageFactoryMock.Setup(sm => sm.CreateStorage<UnitTestComponent>()).Returns(storageMock.Object);
      storageManager.GetStorage<UnitTestComponent>();

      storageManager.AddDataEntry();

      Assert.Equal(1, storageManager.DataLength);
      storageMock.Verify(s => s.AddEntry(), Times.Once);
    }
    [Fact]
    public void AddDataEntry_NoParameterMultipleStorages_DataEntryAdded()
    {
      Mock<IStorage> storageMock = new Mock<IStorage>();
      Mock<IStorage> storageMock2 = new Mock<IStorage>();
      Mock<IStorageFactory> storageFactoryMock = new Mock<IStorageFactory>();
      IStorageManager storageManager = new StorageManager(storageFactoryMock.Object);

      storageFactoryMock.Setup(sm => sm.CreateStorage<UnitTestComponent>()).Returns(storageMock.Object);
      storageFactoryMock.Setup(sm => sm.CreateStorage<UnitTestComponent2>()).Returns(storageMock2.Object);
      storageManager.GetStorage<UnitTestComponent>();
      storageManager.GetStorage<UnitTestComponent2>();

      storageManager.AddDataEntry();

      Assert.Equal(1, storageManager.DataLength);
      storageMock.Verify(s => s.AddEntry(), Times.Once);
      storageMock2.Verify(s => s.AddEntry(), Times.Once);
    }
  }
}
