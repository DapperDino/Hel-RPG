using Hel.Extensions;
using Hel.Inputs;
using UnityEngine;

namespace Hel.Player.StateBehaviours
{
    public class PlayerLocomotionStateBehaviour : SceneLinkedSMB<PlayerMovementController>
    {
        private static readonly int hashAttack = Animator.StringToHash("Attack");
        private static readonly int hashIsAiming = Animator.StringToHash("IsAiming");
        private static readonly int hashWeaponType = Animator.StringToHash("WeaponType");

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.Move();

            animator.ResetTrigger(hashAttack);

            if ((PlayerInputManager.RightClickHeld || PlayerInputManager.LeftClickHeld) && animator.GetInteger(hashWeaponType) == 1)
            {
                animator.SetBool(hashIsAiming, true);
            }
            else if (PlayerInputManager.LeftClickPressed)
            {
                animator.SetTrigger(hashAttack);
            }
        }
    }
}
