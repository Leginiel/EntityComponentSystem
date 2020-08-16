namespace EntityComponentSystem.Core
{
  public interface IIteratorExpression
  {
    bool ExpressionMatched(object value);
  }
}