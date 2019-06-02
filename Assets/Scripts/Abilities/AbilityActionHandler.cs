using Hel.Inputs;
using Hel.Items;
using Hel.Targeting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Abilities
{
    /// <summary>
    /// The brain of all ability action logic
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AbilityActionHandler : MonoBehaviour
    {
        [SerializeField] private AbilityCastData abilityCastData = new AbilityCastData();

        private Animator animator = null;
        private Coroutine currentActionsCoroutine = null;
        private AbilityAction currentAction = null;
        private bool isWaitingForAnimationCallback = false;

        private static readonly int hashBaseLocomotion = Animator.StringToHash("BaseLocomotion");

        public List<ITargetable> CurrentTargets { get; set; } = new List<ITargetable>();

        private void Start() => animator = GetComponent<Animator>();

        private void Update() => CheckForInterrupt();

        private void CheckForInterrupt()
        {
            //Check whether the current action should be interrupted.
            if (currentActionsCoroutine == null) { return; }
            if (!currentAction.IsInterruptible) { return; }
            if (!PlayerInputManager.MovementKeyDown) { return; }

            Interrupt();
        }

        public void Interrupt()
        {
            //Reset everything to do with ability actions.
            currentAction.Interrupt(abilityCastData);
            StopCoroutine(currentActionsCoroutine);
            ClearActionData();
        }

        public void TriggerActions(IUseable useable)
        {
            //Check whether we are able to start the new useable.
            if (currentActionsCoroutine != null) { return; }
            if (animator.IsInTransition(0)) { return; }
            if (animator.GetCurrentAnimatorStateInfo(0).tagHash != hashBaseLocomotion) { return; }

            //Loop through all of the useable's requirements.
            foreach (AbilityRequirement abilityRequirement in useable.AbilityRequirements)
            {
                //If any of the requirements are not met then return.
                if (!abilityRequirement.IsMet()) { return; }
            }

            //Start using the useable
            currentActionsCoroutine = StartCoroutine(DoActions(useable.AbilityActions));
        }

        private IEnumerator DoActions(List<AbilityAction> abilityActions)
        {
            //Loop through all of the actions for the useable.
            foreach (AbilityAction abilityAction in abilityActions)
            {
                //Cache the current action
                currentAction = abilityAction;

                //Pause execution here until the current action's "Trigger" coroutine is complete.
                yield return abilityAction.Trigger(abilityCastData);
            }

            ClearActionData();
        }

        private void ClearActionData()
        {
            //Reset any variables that were to do with the actions that have just been finished.
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
