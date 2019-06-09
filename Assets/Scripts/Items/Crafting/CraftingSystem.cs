using Hel.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Items.Crafting
{
    public class CraftingSystem : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerInventory playerInventory = null;

        [Header("Recipe Button")]
        [Required] [SerializeField] private GameObject recipeButtonPrefab = null;
        [Required] [SerializeField] private Transform recipeButtonHolderTransform = null;

        [Header("Selected Recipe")]
        [Required] [SerializeField] private Button craftButton = null;
        [Required] [SerializeField] private Image resultImage = null;
        [Required] [SerializeField] private TextMeshProUGUI resultNameText = null;
        [Required] [SerializeField] private TextMeshProUGUI resultDescriptionText = null;
        [Required] [SerializeField] private MaterialRequirementTab[] materialRequirementTabs = new MaterialRequirementTab[8];

        [SerializeField] private Recipe[] allRecipes = new Recipe[0];

        private Recipe selectedRecipe = null;

        [Button]
        private void SetRecipes() => allRecipes = Resources.LoadAll<Recipe>("Recipes");

        private void Start()
        {
            SpawnRecipeButtons();

            SetRecipe(allRecipes[0]);

            craftButton.onClick.AddListener(Craft);
        }

        public void SetRecipe(Recipe recipe)
        {
            selectedRecipe = recipe;

            UpdateSelectedRecipeUI();
        }

        private void Craft()
        {
            selectedRecipe.Craft(playerInventory.ItemHolder);

            UpdateSelectedRecipeUI();
        }

        public void UpdateSelectedRecipeUI()
        {
            if (selectedRecipe != null)
            {
                resultImage.sprite = selectedRecipe.Result.item.Icon;
                resultNameText.text = selectedRecipe.Result.item.ColouredName;
                resultDescriptionText.text = selectedRecipe.Result.item.GetInfoDisplayText();

                resultImage.enabled = true;
                resultNameText.enabled = true;
                resultDescriptionText.enabled = true;

                int materialCount = selectedRecipe.Materials.Count;

                for (int i = 0; i < materialRequirementTabs.Length; i++)
                {
                    if (i < materialCount)
                    {
                        materialRequirementTabs[i].SetData(playerInventory.ItemHolder, selectedRecipe.Materials[i]);
                    }
                    else
                    {
                        materialRequirementTabs[i].Clear();
                    }
                }

                craftButton.interactable = selectedRecipe.CanCraft(playerInventory.ItemHolder);
            }
            else
            {
                resultImage.enabled = false;
                resultNameText.enabled = false;
                resultDescriptionText.enabled = false;
            }
        }

        private void SpawnRecipeButtons()
        {
            foreach (Recipe recipe in allRecipes)
            {
                RecipeButton recipeButton = Instantiate(recipeButtonPrefab, recipeButtonHolderTransform)
                    .GetComponent<RecipeButton>();

                recipeButton.Initialise(this, recipe);
            }
        }
    }
}
