using Hel.Extensions;
using UnityEngine;

namespace Hel.Player.StateBehaviours
{
    public class PlayerCastingStateBehaviour : SceneLinkedSMB<PlayerMovementController>
    {
        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.FacePlayerForward();
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.FacePlayerForward();
        }
    }
}

