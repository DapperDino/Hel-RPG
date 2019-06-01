using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class DebugAction : AbilityAction
    {
        [SerializeField] private string debugText;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            Debug.Log(debugText);
            yield return null;
        }
    }
}