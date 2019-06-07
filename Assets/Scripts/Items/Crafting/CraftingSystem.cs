using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Items.Crafting
{
    public class CraftingSystem : MonoBehaviour
    {
        [SerializeField] private Recipe[] allRecipes = new Recipe[0];

        [Button]
        private void SetRecipes()
        {
            allRecipes = Resources.LoadAll<Recipe>("Recipes");
            foreach (Recipe recipe in allRecipes)
            {
                Debug.Log(recipe.name);
            }
        }
    }
}
