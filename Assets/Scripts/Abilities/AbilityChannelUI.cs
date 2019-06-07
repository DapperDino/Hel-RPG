using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Abilities
{
    /// <summary>
    /// Used to display item channelling progress.
    /// </summary>
    public class AbilityChannelUI : MonoBehaviour
    {
        [Required] [SerializeField] private AbilityChannelDataHolder abilityChannelDataHolder = null;
        [Required] [SerializeField] private Slider channelBarSlider = null;
        [Required] [SerializeField] private TextMeshProUGUI abilityNameText = null;

        private Animator animator = null;

        private static readonly int hashFadeIn = Animator.StringToHash("FadeIn");

        private void Start() => animator = GetComponent<Animator>();

        public void StartChannelUI() => StartCoroutine(HandleChannelUI());

        public void InterruptChannel()
        {
            StopAllCoroutines();
            abilityChannelDataHolder.Finish();
            animator.SetBool(hashFadeIn, false);
        }

        private IEnumerator HandleChannelUI()
        {
            animator.SetBool(hashFadeIn, true);

            abilityNameText.text = abilityChannelDataHolder.CurrentChannelable.Name;

            while (!abilityChannelDataHolder.FinishedChanneling)
            {
                channelBarSlider.value = 1 - (abilityChannelDataHolder.RemainingChannelTime / abilityChannelDataHolder.CurrentChannelable.ChannelDuration);

                yield return null;
            }

            channelBarSlider.value = 1f;

            animator.SetBool(hashFadeIn, false);
        }
    }
}
