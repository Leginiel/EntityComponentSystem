namespace EntityComponentSystem.Core
{
  public class AllIteratorExpression : IIteratorExpression
  {
    public bool ExpressionMatched(object value)
    {
      return true;
    }
  }
}
