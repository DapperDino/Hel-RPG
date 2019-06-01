using UnityEngine;

namespace Hel.Utilities
{
    public class HomeInOnTransform : MonoBehaviour
    {
        [SerializeField] private float homingSpeed;

        private Transform target;

        public void Initialise(Transform target)
        {
            this.target = target;
        }

        private void Update()
        {
            if (target != null)
            {
                MoveToTarget();
            }
            else
            {
                transform.Translate(transform.forward * homingSpeed * Time.deltaTime);
            }
        }

        private void MoveToTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, homingSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}
