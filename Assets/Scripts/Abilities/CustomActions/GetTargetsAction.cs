using Hel.Events.CustomEvents;
using Hel.Targeting;
using Hel.Utilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Abilities
{
    /// <summary>
    /// Used to initialise a target getter to return targets based on its type.
    /// </summary>
    [Serializable]
    public class GetTargetsAction : AbilityAction
    {
        [Required] [SerializeField] private TargetGetter targetGetter = null;
        [Required] [SerializeField] private TargetGetterEvent onStartGettingTargets = null;
        [Required] [SerializeField] private VoidEvent onEndGettingTargets = null;

        private static readonly int hashIsTargeting = Animator.StringToHash("IsTargeting");

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Set the animator to be targeting.
            abilityCastData.Animator.SetBool(hashIsTargeting, true);

            //Alert any listeners that we are now getting targets.
            onStartGettingTargets.Raise(targetGetter);

            //Start a coroutine to get targets.
            CoroutineWithData getTargetsCoroutine = new CoroutineWithData(targetGetter.GetTargets());
            yield return getTargetsCoroutine.Run();

            //Break if we did not get back a list of targets.
            if (!(getTargetsCoroutine.Result is List<ITargetable> targets)) { yield break; }

            //Set the currently stored targets to be the targets that were just collected.
            abilityCastData.AbilityActionHandler.CurrentTargets = targets;

            FinishGettingTargets(abilityCastData);
        }

        public override void Interrupt(AbilityCastData abilityCastData)
        {
            FinishGettingTargets(abilityCastData);
        }

        private void FinishGettingTargets(AbilityCastData abilityCastData)
        {
            //Alert any listeners that we have now finished getting targets.
            onEndGettingTargets.Raise();

            //Set the animator to not be targeting.
            abilityCastData.Animator.SetBool(hashIsTargeting, false);
        }
    }
}