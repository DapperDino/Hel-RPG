using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Items.Crafting
{
    public class RecipeButton : MonoBehaviour
    {
        [Required] [SerializeField] private Button recipeButton = null;
        [Required] [SerializeField] private Image resultImage = null;
        [Required] [SerializeField] private TextMeshProUGUI resultNameText = null;

        private Recipe thisRecipe;

        public void Initialise(CraftingSystem craftingSystem, Recipe recipe)
        {
            thisRecipe = recipe;

            recipeButton.onClick.AddListener(() => craftingSystem.SetRecipe(thisRecipe));

            SetButtonUI();
        }

        private void SetButtonUI()
        {
            resultImage.sprite = thisRecipe.Result.item.Icon;
            resultNameText.text = thisRecipe.Result.item.ColouredName;
        }
    }
}
