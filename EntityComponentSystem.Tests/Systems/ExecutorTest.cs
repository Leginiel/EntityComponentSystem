using EntityComponentSystem.Systems;
using Moq;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace EntityComponentSystem.Tests.Systems
{
  public class ExecutorTest
  {
    [Fact]
    public void TestExecute_DeltaTime_ExecutionSuccessful()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Mock<Executor> executorMock = new Mock<Executor>()
      {
        CallBase = true
      };
      Executor executor = executorMock.Object;
      systemMock.Setup(s => s.Execute(0.1)).Verifiable();
      executor.RegisterSystem(systemMock.Object);

      executor.Execute(0.1);

      systemMock.Verify();
    }
    [Fact]
    public void TestExecute_DeltaTimeHierarchicalSystems_ExecutionSuccessful()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Mock<ISystem> systemMock2 = new Mock<ISystem>();
      ExecuteAfterAttribute attribute = new ExecuteAfterAttribute(systemMock.Object.GetType());
      Mock<Executor> executorMock = new Mock<Executor>()
      {
        CallBase = true
      };
      Executor executor = executorMock.Object;
      TypeDescriptor.AddAttributes(systemMock2.Object, attribute);
      systemMock.Setup(s => s.Execute(0.1)).Verifiable();
      systemMock.Setup(s => s.Execute(0.1)).Verifiable();
      executor.RegisterSystem(systemMock.Object);
      executor.RegisterSystem(systemMock2.Object);

      executor.Execute(0.1);
      systemMock.Verify();
      systemMock2.Verify();
    }
    [Fact]
    public void TestRegisterSystem_NoAttribute_RegisterSystemInDefaultExecutionPlan()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Executor executor = new Executor();

      systemMock.Setup(s => s.Execute(It.IsAny<double>())).Verifiable();
      executor.RegisterSystem(systemMock.Object);

      Assert.Single(executor.ExecutionPlans);
      Assert.Contains(systemMock.Object, executor.ExecutionPlans.First().Systems);
    }
    [Fact]
    public void TestRegisterSystem_Attribute_RegisterSystemInSpecialExecutionPlan()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Mock<ISystem> systemMock2 = new Mock<ISystem>();
      Executor executor = new Executor();
      ExecuteAfterAttribute attribute = new ExecuteAfterAttribute(systemMock.Object.GetType());
      TypeDescriptor.AddAttributes(systemMock2.Object, attribute);

      executor.RegisterSystem(systemMock.Object);
      executor.RegisterSystem(systemMock2.Object);

      Assert.Equal(2, executor.ExecutionPlans.Count);
      Assert.Contains(systemMock.Object, executor.ExecutionPlans.First().Systems);
      Assert.Contains(systemMock2.Object, executor.ExecutionPlans.Last().Systems);
    }
    [Fact]
    public void TestUnregisterSystem_UnregisterSuccessful()
    {
      Mock<ISystem> systemMock = new Mock<ISystem>();
      Executor executor = new Executor();

      systemMock.Setup(s => s.Execute(It.IsAny<double>())).Verifiable();
      executor.RegisterSystem(systemMock.Object);

      executor.UnregisterSystem(systemMock.Object);
      Assert.Single(executor.ExecutionPlans);
      Assert.Empty(executor.ExecutionPlans.First().Systems);
    }
  }
}
