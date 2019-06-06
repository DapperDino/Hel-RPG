using UnityEngine;

namespace Hel.Utilities
{
    /// <summary>
    /// Used to move an object by a specified velocity per second.
    /// </summary>
    public class MoveTransformOverTime : MonoBehaviour
    {
        [SerializeField] private Vector3 movementPerSecond = new Vector3();

        public void Initialise(Vector3 movementPerSecond) => this.movementPerSecond = movementPerSecond;

        private void Update() => transform.Translate(movementPerSecond * Time.deltaTime, Space.World);
    }
}

