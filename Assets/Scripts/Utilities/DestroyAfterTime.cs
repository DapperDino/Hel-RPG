using UnityEngine;

namespace Hel.Utilities
{
    /// <summary>
    /// Used to destory an object after a specified number of seconds.
    /// </summary>
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float secondsBeforeDestroying = 1f;

        private void Start() => Destroy(gameObject, secondsBeforeDestroying);
    }
}
