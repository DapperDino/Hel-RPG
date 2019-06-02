namespace Hel.Abilities
{
    /// <summary>
    /// Defines any item that can be put on cooldown.
    /// </summary>
    public interface ICooldownable
    {
        float MaxCooldownDuration { get; }
    }
}