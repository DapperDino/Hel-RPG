using UnityEngine;

namespace Hel.OnContact
{
    public class DestroyOnContact : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) => Destroy(gameObject);
    }
}
