using UnityEngine;

namespace Hel.Utilities
{
    public class MoveTransformOverTime : MonoBehaviour
    {
        [SerializeField] private Vector3 movementPerSecond;

        public void Initialise(Vector3 movementPerSecond)
        {
            this.movementPerSecond = movementPerSecond;
        }

        private void Update()
        {
            transform.Translate(movementPerSecond * Time.deltaTime, Space.World);
        }
    }
}

