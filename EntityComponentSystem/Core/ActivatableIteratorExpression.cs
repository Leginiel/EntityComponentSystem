namespace EntityComponentSystem.Core
{
  public class ActivatableIteratorExpression : IIteratorExpression
  {
    public bool OnlyActive { get; }
    public bool OnlyInactive { get; }

    public ActivatableIteratorExpression(bool onlyActive, bool onlyInactive)
    {
      OnlyActive = onlyActive;
      OnlyInactive = onlyInactive;
    }

    public bool ExpressionMatched(object value)
    {
      IActivatable activateable = value as IActivatable;

      return activateable != null && (OnlyActive && activateable.Enabled) || (OnlyInactive && !activateable.Enabled);
    }
  }
}
