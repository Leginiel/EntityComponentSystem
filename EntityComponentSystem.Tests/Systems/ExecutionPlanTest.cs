using EntityComponentSystem.Systems;
using Moq;
using Xunit;

namespace EntityComponentSystem.Tests.Systems
{
  public class ExecutionPlanTest
  {
    [Fact]
    public void TestAddSystem_ValidNotExistingSystem_SucecssfullyAdded()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      ExecutionPlan executionPlan = new ExecutionPlan();

      executionPlan.AddSystem(systemMock.Object);

      Assert.Single(executionPlan.Systems);
      Assert.Contains(systemMock.Object, executionPlan.Systems);
    }
    [Fact]
    public void TestAddSystem_ValidExistingSystem_SucecssfullyAdded()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      ExecutionPlan executionPlan = new ExecutionPlan();

      executionPlan.AddSystem(systemMock.Object);
      executionPlan.AddSystem(systemMock.Object);

      Assert.Single(executionPlan.Systems);
      Assert.Contains(systemMock.Object, executionPlan.Systems);
    }
    [Fact]
    public void TestRemoveSystem_ValidExistingSystem_SuccessfullyRemoved()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      ExecutionPlan executionPlan = new ExecutionPlan();

      executionPlan.AddSystem(systemMock.Object);
      executionPlan.RemoveSystem(systemMock.Object);

      Assert.Empty(executionPlan.Systems);
    }
    [Fact]
    public void TestRemoveSystem_NonaddedSystem_NoError()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      ExecutionPlan executionPlan = new ExecutionPlan();

      executionPlan.RemoveSystem(systemMock.Object);

      Assert.Empty(executionPlan.Systems);
    }
    [Fact]
    public void TestContainsSystem_ValidSystem_SucecssfullyAdded()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      ExecutionPlan executionPlan = new ExecutionPlan();
      bool result;
      executionPlan.AddSystem(systemMock.Object);
      result = executionPlan.Contains(systemMock.Object);

      Assert.True(result);
    }
  }
}
