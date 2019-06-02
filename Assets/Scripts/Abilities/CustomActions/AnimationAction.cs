using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to play an animation and wait for a callback if required.
    /// </summary>
    [Serializable]
    public class AnimationAction : AbilityAction
    {
        [SerializeField] private string animationClipName = "New Clip Name";
        [SerializeField] [Range(0, 1)] private float crossfadeDuration = 0.25f;
        [SerializeField] private bool waitForCallback = false;

        private int AnimationHash { get { return Animator.StringToHash(animationClipName); } }

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Smoothly transition to the desired animation clip
            abilityCastData.Animator.CrossFade(AnimationHash, crossfadeDuration);

            //Wait for the animation clip to trigger an animation event callback
            if (waitForCallback)
            {
                yield return abilityCastData.AbilityActionHandler.WaitForAnimationCallback();
            }
        }
    }
}