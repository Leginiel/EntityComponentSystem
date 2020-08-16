namespace EntityComponentSystem.Components
{
  internal class ComponentElementFactory : IComponentElementFactory
  {
    public IComponent CreateInstanced<ComponentType>(ComponentType value)
    {
      IInstancedComponent<ComponentType> result = new InstancedComponent<ComponentType>();
      result.AddInstance(value);

      return result;
    }
    public IComponent Create<ComponentType>(ComponentType value)
    {
      IComponent<ComponentType> result = new Component<ComponentType>();
      result.Value = value;

      return result;
    }
  }
}
