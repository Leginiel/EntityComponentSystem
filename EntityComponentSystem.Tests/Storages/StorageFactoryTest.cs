using EntityComponentSystem.Storages;
using EntityComponentSystem.Tests.Components;
using Xunit;

namespace EntityComponentSystem.Tests.Storage
{
  public class StorageFactoryTest
  {
    [Fact]
    public void CreateStorage_NoParameter_ValidStorage()
    {
      IStorageFactory storageFactory = new StorageFactory();

      IStorage result = storageFactory.CreateStorage<UnitTestComponent>();

      Assert.NotNull(result);
    }
  }
}
