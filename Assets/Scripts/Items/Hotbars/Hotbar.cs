using Hel.SavingLoading;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Items.Hotbars
{
    public class Hotbar : MonoBehaviour, ISaveable
    {
        [Required] [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[10];

        public int LoadPriority { get { return 200; } }

        public void Add(HotbarItem itemToAdd)
        {
            foreach (HotbarSlot hotbarSlot in hotbarSlots)
            {
                if (hotbarSlot.AddItem(itemToAdd)) { return; }
            }
        }

        public void Save()
        {
            HotbarSaveData hotbarSaveData = new HotbarSaveData();

            foreach (HotbarSlot hotbarSlot in hotbarSlots)
            {
                hotbarSaveData.AddSlotData(hotbarSlot.SlotItem, hotbarSlot.SlotIndex);
            }

            GameSaveHandler.SaveFile("player_hotbar", hotbarSaveData);
        }

        public void Load()
        {
            HotbarSaveData hotbarSaveData = new HotbarSaveData();

            GameSaveHandler.LoadFile("player_hotbar", hotbarSaveData);

            foreach (HotbarSlotData hotbarSlotData in hotbarSaveData.hotbarSlotData)
            {
                hotbarSlots[hotbarSlotData.slotIndex].SlotItem = hotbarSlotData.hotbarItem;
            }
        }

        [Serializable]
        private class HotbarSaveData
        {
            public List<HotbarSlotData> hotbarSlotData = new List<HotbarSlotData>();

            public void AddSlotData(HotbarItem hotbarItem, int slotIndex) => hotbarSlotData.Add(new HotbarSlotData(hotbarItem, slotIndex));
        }

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