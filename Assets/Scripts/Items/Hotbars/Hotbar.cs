using Hel.SavingLoading;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Items.Hotbars
{
    /// <summary>
    /// Used to handle saving and loading of the player's hotbar slots.
    /// </summary>
    public class Hotbar : MonoBehaviour, ISaveable
    {
        [Required] [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[10];

        public int LoadPriority { get { return 200; } }

        public void Add(HotbarItem itemToAdd)
        {
            //Loop through each hotbar slot.
            foreach (HotbarSlot hotbarSlot in hotbarSlots)
            {
                //If the item is successfully added to the slot then return.
                if (hotbarSlot.AddItem(itemToAdd)) { return; }
            }
        }

        public void Save()
        {
            //Create a new instance of hotbar save data.
            HotbarSaveData hotbarSaveData = new HotbarSaveData();

            //Loop through each hotbar slot.
            foreach (HotbarSlot hotbarSlot in hotbarSlots)
            {
                //Add the slot's data to the hotbar save data.
                hotbarSaveData.AddSlotData(hotbarSlot.SlotItem, hotbarSlot.SlotIndex);
            }

            //Save the hotbar data.
            GameSaveHandler.SaveFile("player_hotbar", hotbarSaveData);
        }

        public void Load()
        {
            //Create a new instance of hotbar save data.
            HotbarSaveData hotbarSaveData = new HotbarSaveData();

            //Load the hotbar data.
            GameSaveHandler.LoadFile("player_hotbar", hotbarSaveData);

            //Loop through each hotbar slot in the loaded data.
            foreach (HotbarSlotData hotbarSlotData in hotbarSaveData.hotbarSlotData)
            {
                //Set the slot's item to the item loaded from the hotbar save file.
                hotbarSlots[hotbarSlotData.slotIndex].SlotItem = hotbarSlotData.hotbarItem;
            }
        }

        /// <summary>
        /// Stores all of the slots' save data.
        /// </summary>
        [Serializable]
        private class HotbarSaveData
        {
            public List<HotbarSlotData> hotbarSlotData = new List<HotbarSlotData>();

            public void AddSlotData(HotbarItem hotbarItem, int slotIndex) => hotbarSlotData.Add(new HotbarSlotData(hotbarItem, slotIndex));
        }

        /// <summary>
        /// Stores the data of an individual slot.
        /// </summary>
        [Serializable]
        private struct HotbarSlotData
        {
            public HotbarItem hotbarItem;
            public int slotIndex;

            public HotbarSlotData(HotbarItem hotbarItem, int slotIndex)
            {
                this.hotbarItem = hotbarItem;
                this.slotIndex = slotIndex;
            }
        }
    }
}