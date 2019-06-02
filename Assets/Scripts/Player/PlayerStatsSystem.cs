using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Player
{
    public class PlayerStatsSystem : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerStatsDataHolder playerStatsDataHolder = null;

        private void Update()
        {
            playerStatsDataHolder.Tick();
        }
    }
}