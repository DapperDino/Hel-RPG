using Hel.Inputs;
using Hel.Items;
using Hel.Targeting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Abilities
{
    [RequireComponent(typeof(Animator))]
    public class AbilityActionHandler : MonoBehaviour
    {
        [SerializeField] private AbilityCastData abilityCastData;

        private Animator animator = null;
        private Coroutine currentActionsCoroutine = null;
        private AbilityAction currentAction = null;
        private bool isWaitingForAnimationCallback = false;

        private static readonly int hashBaseLocomotion = Animator.StringToHash("BaseLocomotion");

        public List<ITargetable> CurrentTargets { get; set; } = new List<ITargetable>();

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            CheckForInterrupt();
        }

        private void CheckForInterrupt()
        {
            if (currentActionsCoroutine == null) { return; }
            if (!currentAction.IsInterruptible) { return; }
            if (!PlayerInputManager.MovementKeyDown) { return; }

            Interrupt();
        }

        public void Interrupt()
        {
            currentAction.Interrupt(abilityCastData);
            StopCoroutine(currentActionsCoroutine);
            ClearActionData();
        }

        public void TriggerActions(IUseable useable)
        {
            if (currentActionsCoroutine != null) { return; }

            if (animator.IsInTransition(0)) { return; }

            if (animator.GetCurrentAnimatorStateInfo(0).tagHash != hashBaseLocomotion) { return; }

            foreach (AbilityRequirement abilityRequirement in useable.AbilityRequirements)
            {
                if (!abilityRequirement.IsMet()) { return; }
            }

            currentActionsCoroutine = StartCoroutine(DoActions(useable.AbilityActions));
        }

        private IEnumerator DoActions(List<AbilityAction> abilityActions)
        {
            foreach (AbilityAction abilityAction in abilityActions)
            {
                currentAction = abilityAction;
                yield return abilityAction.Trigger(abilityCastData);
            }

            ClearActionData();
        }

        private void ClearActionData()
        {
            currentAction = null;
            currentActionsCoroutine = null;
            CurrentTargets = new List<ITargetable>();
        }

        public IEnumerator WaitForAnimationCallback()
        {
            isWaitingForAnimationCallback = true;

            yield return new WaitUntil(() => !isWaitingForAnimationCallback);
        }

        public void ContinueCurrentAction()
        {
            isWaitingForAnimationCallback = false;
        }
    }
}
