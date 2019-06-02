using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Abilities
{
    /// <summary>
    /// 
    /// </summary>
    public class AbilityChannelUI : MonoBehaviour
    {
        [Required] [SerializeField] private AbilityChannelDataHolder abilityChannelDataHolder = null;
        [Required] [SerializeField] private CanvasGroup channelUICanvasGroup = null;
        [Required] [SerializeField] private Slider channelBarSlider = null;
        [Required] [SerializeField] private TextMeshProUGUI spellNameText = null;
        [SerializeField] private float fadeTime = 0.25f;

        public void StartChannelUI()
        {
            StartCoroutine(HandleChannelUI());
        }

        public void InterruptChannel()
        {
            StopAllCoroutines();
            abilityChannelDataHolder.Finish();
            StartCoroutine(Fade(false));
        }

        private IEnumerator HandleChannelUI()
        {
            StartCoroutine(Fade(true));

            spellNameText.text = abilityChannelDataHolder.CurrentChannelable.Name;

            while (!abilityChannelDataHolder.FinishedChanneling)
            {
                channelBarSlider.value = 1 - (abilityChannelDataHolder.RemainingChannelTime / abilityChannelDataHolder.CurrentChannelable.ChannelDuration);

                yield return null;
            }

            StartCoroutine(Fade(false));
        }

        private IEnumerator Fade(bool fadeIn)
        {
            float currentAlpha = channelUICanvasGroup.alpha;
            int targetAlpha = fadeIn ? 1 : 0;

            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                channelUICanvasGroup.alpha = Mathf.Lerp(currentAlpha, targetAlpha, t / fadeTime);
                yield return null;
            }

            channelUICanvasGroup.alpha = targetAlpha;
        }
    }
}
