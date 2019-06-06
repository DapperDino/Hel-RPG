using UnityEngine;

namespace Hel.Utilities
{
    /// <summary>
    /// Used to move towards a transform over time.
    /// </summary>
    public class HomeInOnTransform : MonoBehaviour
    {
        [SerializeField] private float homingSpeed = 10f;

        private Transform target = null;

        public void Initialise(Transform target) => this.target = target;

        private void Update()
        {
            //Make sure we have a target.
            if (target != null)
            {
                MoveToTarget();
            }
            else
            {
                //Keep going forwards.
                transform.Translate(transform.forward * homingSpeed * Time.deltaTime);
            }
        }

        private void MoveToTarget()
        {
            //Move towards the target and face it.
            transform.position = Vector3.MoveTowards(transform.position, target.position, homingSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}
