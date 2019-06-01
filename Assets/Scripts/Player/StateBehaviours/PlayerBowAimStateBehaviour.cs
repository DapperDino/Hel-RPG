using Hel.Events.CustomEvents;
using Hel.Extensions;
using Hel.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Player.StateBehaviours
{
    public class PlayerBowAimStateBehaviour : SceneLinkedSMB<PlayerMovementController>
    {
        [Required] [SerializeField] private VoidEvent onBowFired;

        private static readonly int hashFireArrow = Animator.StringToHash("FireArrow");
        private static readonly int hashIsDrawingArrow = Animator.StringToHash("IsDrawingArrow");

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!PlayerInputManager.LeftClickHeld)
            {
                animator.SetTrigger(hashFireArrow);
                onBowFired.Raise();
                animator.SetBool(hashIsDrawingArrow, false);
            }
        }
    }
}
