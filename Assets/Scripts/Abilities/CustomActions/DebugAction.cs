using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used as a placeholder action for testing purposes.
    /// </summary>
    [Serializable]
    public class DebugAction : AbilityAction
    {
        [SerializeField] private string debugText = "New Debug Text";

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Outputs the desired string to the console.
            Debug.Log(debugText);

            yield return null;
        }
    }
}