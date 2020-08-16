namespace EntityComponentSystem.Core
{
  public static class It
  {
    public static IIteratorExpression All()
    {
      return new AllIteratorExpression();
    }
    public static IIteratorExpression AllActive()
    {
      return new ActivatableIteratorExpression(true, false);
    }
    public static IIteratorExpression AllInactive()
    {
      return new ActivatableIteratorExpression(false, true);
    }
  }
}
