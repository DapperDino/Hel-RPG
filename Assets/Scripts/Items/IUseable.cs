using Hel.Abilities;
using System.Collections.Generic;

namespace Hel.Items
{
    public interface IUseable
    {
        List<AbilityRequirement> AbilityRequirements { get; }
        List<AbilityAction> AbilityActions { get; }

        void Use();
    }
}