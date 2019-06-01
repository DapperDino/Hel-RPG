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
    [Serializable]
    public class GetTargetsAction : AbilityAction
    {
        [Required] [SerializeField] private TargetGetter targetGetter;
        [Required] [SerializeField] private TargetGetterEvent onStartGettingTargets;
        [Required] [SerializeField] private VoidEvent onEndGettingTargets;

        private static readonly int hashIsTargeting = Animator.StringToHash("IsTargeting");

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            abilityCastData.Animator.SetBool(hashIsTargeting, true);

            onStartGettingTargets.Raise(targetGetter);

            CoroutineWithData getTargetsCoroutine = new CoroutineWithData(targetGetter.GetTargets());
            yield return getTargetsCoroutine.Run();

            if (!(getTargetsCoroutine.Result is List<ITargetable> targets)) { yield break; }

            abilityCastData.AbilityActionHandler.CurrentTargets = targets;

            FinishGettingTargets(abilityCastData);
        }

        public override void Interrupt(AbilityCastData abilityCastData)
        {
            FinishGettingTargets(abilityCastData);
        }

        private void FinishGettingTargets(AbilityCastData abilityCastData)
        {
            onEndGettingTargets.Raise();

            abilityCastData.Animator.SetBool(hashIsTargeting, false);
        }
    }
}