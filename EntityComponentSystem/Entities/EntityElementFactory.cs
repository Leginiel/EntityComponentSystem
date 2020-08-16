namespace EntityComponentSystem.Entities
{
  internal class EntityElementFactory : IElementFactory<Entity>
  {
    private int lastIndex = 0;

    public Entity Create()
    {
      return new Entity(lastIndex++);
    }
  }
}
