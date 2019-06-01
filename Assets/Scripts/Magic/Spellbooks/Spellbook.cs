using Hel.Items;
using Hel.Player;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hel.Magic.Spellbooks
{
    public class Spellbook : SerializedMonoBehaviour
    {
        [Required] [SerializeField] private PlayerStatsHolder playerStatsHolder;
        [Required] [SerializeField] private TextMeshProUGUI spellPointsText;
        [Required] [SerializeField] private List<ElementTree> elementTrees = new List<ElementTree>();
        [Required] [SerializeField] private SpellSlot[,] spellSlots = new SpellSlot[7, 3];

        private void Start()
        {
            if (elementTrees.Count >= 1)
            {
                DisplayTree(elementTrees[0]);
            }

            SetSpellPointsText();
        }

        public void SetSpellPointsText()
        {
            spellPointsText.text = $"Spell Points: {playerStatsHolder.CurrencyHolder.GetCurrencyAmount(CurrencyTypes.SpellPoints)}";
        }

        public void DisplayTree(ElementTree elementTree)
        {
            if (!elementTrees.Contains(elementTree)) { return; }

            for (int i = 0; i < spellSlots.GetLength(0); i++)
            {
                for (int j = 0; j < spellSlots.GetLength(1); j++)
                {
                    spellSlots[i, j].Initialise(elementTree.TreeSpells[i, j]);
                }
            }
        }
    }
}
