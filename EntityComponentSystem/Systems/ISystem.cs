namespace EntityComponentSystem.Systems
{
  public interface ISystem
  {
    void Setup();
    void Execute();
    void TearDown();
  }
}
