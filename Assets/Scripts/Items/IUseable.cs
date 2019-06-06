using Hel.Abilities;
using System.Collections.Generic;

namespace Hel.Items
{
    /// <summary>
    /// Defines any item that can be used.
    /// </summary>
    public interface IUseable
    {
        List<AbilityRequirement> AbilityRequirements { get; }
        List<AbilityAction> AbilityActions { get; }

        void Use();
    }
}