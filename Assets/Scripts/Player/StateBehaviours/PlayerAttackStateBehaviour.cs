using Hel.Extensions;
using UnityEngine;

namespace Hel.Player.StateBehaviours
{
    public class PlayerAttackStateBehaviour : SceneLinkedSMB<PlayerMovementController>
    {
        private static readonly int hashIsAttacking = Animator.StringToHash("IsAttacking");

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.Move();
        }

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(hashIsAttacking, true);
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(hashIsAttacking, false);
        }
    }
}
