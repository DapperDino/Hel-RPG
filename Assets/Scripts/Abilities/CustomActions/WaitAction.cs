using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class WaitAction : AbilityAction
    {
        [SerializeField] private float waitTime;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            yield return new WaitForSeconds(waitTime);
        }
    }
}