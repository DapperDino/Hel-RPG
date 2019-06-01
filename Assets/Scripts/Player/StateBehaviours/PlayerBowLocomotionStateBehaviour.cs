using Hel.Extensions;
using Hel.Inputs;
using UnityEngine;

namespace Hel.Player.StateBehaviours
{
    public class PlayerBowLocomotionStateBehaviour : SceneLinkedSMB<PlayerMovementController>
    {
        [SerializeField] private Vector3 aimAtOffset;

        private static readonly int hashIsAiming = Animator.StringToHash("IsAiming");

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.Move();
        }

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.AimAtOffset = aimAtOffset;
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.Move();

            monoBehaviour.FacePlayerForward();

            if (!PlayerInputManager.RightClickHeld && !PlayerInputManager.LeftClickHeld)
            {
                animator.SetBool(hashIsAiming, false);
            }
        }

        public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.AimAtOffset = Vector3.zero;
        }
    }
}
