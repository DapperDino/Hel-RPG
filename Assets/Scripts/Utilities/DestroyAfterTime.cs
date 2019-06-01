using UnityEngine;

namespace Hel.Utilities
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float secondsBeforeDestroying;

        private void Start() => Destroy(gameObject, secondsBeforeDestroying);
    }
}
