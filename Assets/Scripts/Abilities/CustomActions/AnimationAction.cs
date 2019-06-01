using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class AnimationAction : AbilityAction
    {
        [SerializeField] private string animationClipName;
        [SerializeField] [Range(0, 1)] private float crossfadeDuration = 0.25f;
        [SerializeField] private bool waitForCallback = false;

        private int AnimationHash { get { return Animator.StringToHash(animationClipName); } }

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            abilityCastData.Animator.CrossFade(AnimationHash, crossfadeDuration);

            if (waitForCallback)
            {
                yield return abilityCastData.AbilityActionHandler.WaitForAnimationCallback();
            }
        }
    }
}