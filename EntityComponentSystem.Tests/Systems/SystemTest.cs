using EntityComponentSystem.Storages;
using EntityComponentSystem.Systems;
using EntityComponentSystem.Tests.Components;
using Moq;
using System;
using Xunit;

namespace EntityComponentSystem.Tests.Systems
{
  public class SystemTest
  {
    [Fact]
    public void TestSetup_NoParameter_SuccessfulSetup()
    {
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<System<ValueTuple<UnitTestComponent>>> systemMock = new Mock<System<ValueTuple<UnitTestComponent>>>(storageManagerMock.Object)
      {
        CallBase = true
      };

      systemMock.Object.Setup();
    }
    [Fact]
    public void TestTearDown_NoParameter_SuccessfulTearDown()
    {
      Mock<IStorageManager> storageManagerMock = new Mock<IStorageManager>();
      Mock<System<ValueTuple<UnitTestComponent>>> systemMock = new Mock<System<ValueTuple<UnitTestComponent>>>(storageManagerMock.Object)
      {
        CallBase = true
      };

      systemMock.Object.TearDown();
    }
  }
}
