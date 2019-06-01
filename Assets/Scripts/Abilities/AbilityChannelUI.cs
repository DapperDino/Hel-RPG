using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Abilities
{
    public class AbilityChannelUI : MonoBehaviour
    {
        [Required] [SerializeField] private AbilityChannelSystem abilityChannelSystem = null;
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
            abilityChannelSystem.Finish();
            StartCoroutine(Fade(false));
        }

        private IEnumerator HandleChannelUI()
        {
            StartCoroutine(Fade(true));

            spellNameText.text = abilityChannelSystem.CurrentChannelable.Name;

            while (!abilityChannelSystem.FinishedChanneling)
            {
                channelBarSlider.value = 1 - (abilityChannelSystem.RemainingChannelTime / abilityChannelSystem.CurrentChannelable.ChannelDuration);

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
