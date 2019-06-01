using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Items.Hotbars
{
    public abstract class HotbarItem : SerializedScriptableObject
    {
        [Header("Basic Info")]
        [SerializeField] private new string name;
        [Required] [SerializeField] private Sprite icon;

        public string Name { get { return name; } }
        public abstract string ColouredName { get; }
        public Sprite Icon { get { return icon; } }

        public abstract string GetInfoDisplayText();
    }
}