namespace Hel.Abilities
{
    /// <summary>
    /// Base class for all ability requirements.
    /// </summary>
    public abstract class AbilityRequirement
    {
        public abstract bool IsMet();
        public abstract string GetDisplayText();
    }
}