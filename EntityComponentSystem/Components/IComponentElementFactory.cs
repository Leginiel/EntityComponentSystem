namespace EntityComponentSystem.Components
{
  public interface IComponentElementFactory
  {
    IComponent Create<ComponentType>(ComponentType value);
    IComponent CreateInstanced<ComponentType>(ComponentType value);
  }
}