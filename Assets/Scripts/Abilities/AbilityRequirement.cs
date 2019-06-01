namespace Hel.Abilities
{
    public abstract class AbilityRequirement
    {
        public abstract bool IsMet();
        public abstract string GetDisplayText();
    }
}