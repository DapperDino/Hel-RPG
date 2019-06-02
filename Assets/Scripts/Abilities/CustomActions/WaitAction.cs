using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to wait/pause actions for a given duration.
    /// </summary>
    [Serializable]
    public class WaitAction : AbilityAction
    {
        [SerializeField] private float waitTime = 1f;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Wait for the desired time (in seconds).
            yield return new WaitForSeconds(waitTime);
        }
    }
}