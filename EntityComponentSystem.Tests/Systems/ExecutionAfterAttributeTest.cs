using EntityComponentSystem.Systems;
using EntityComponentSystem.Tests.Components;
using Moq;
using System;
using Xunit;

namespace EntityComponentSystem.Tests.Systems
{
  public class ExecutionAfterAttributeTest
  {
    [Fact]
    public void ConstructorTest_ValidSystem_Successful()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      ExecuteAfterAttribute attribute = new ExecuteAfterAttribute(systemMock.Object.GetType());

      Assert.Contains(systemMock.Object.GetType(), attribute.SystemTypes);
    }
    [Fact]
    public void ConstructorTest_InvalidSystem_ThrowsArgumentException()
    {
      ExecuteAfterAttribute attribute;

      Assert.Throws<ArgumentException>(() => attribute = new ExecuteAfterAttribute(typeof(UnitTestComponent)));
    }
  }
}
