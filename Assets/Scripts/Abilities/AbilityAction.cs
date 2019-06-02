using System.Collections;
using UnityEngine;

namespace Hel.Abilities
{
    /// <summary>
    /// Base class for all ability actions.
    /// </summary>
    public abstract class AbilityAction
    {
        [SerializeField] private bool isInterruptible = false;

        public bool IsInterruptible { get { return isInterruptible; } }

        public abstract IEnumerator Trigger(AbilityCastData abilityCastData);

        public virtual void Interrupt(AbilityCastData abilityCastData) { }
    }
}