namespace EntityComponentSystem.Systems
{
  public interface ISystem
  {
    void Setup();
    void Execute(double deltaTime);
    void TearDown();
  }
}
