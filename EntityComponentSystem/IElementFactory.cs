namespace EntityComponentSystem
{
  internal interface IElementFactory<ElementType>
    where ElementType : class
  {
    ElementType Create();
  }
}
