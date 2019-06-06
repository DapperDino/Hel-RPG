using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Player
{
    /// <summary>
    /// Used to tick and listen to events for the player stats data holder.
    /// </summary>
    public class PlayerStatsSystem : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerStatsDataHolder playerStatsDataHolder = null;

        private void Update() => playerStatsDataHolder.Tick();
    }
}