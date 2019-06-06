using UnityEngine;

namespace Hel.OnContact
{
    /// <summary>
    /// Used to destroy self on contact.
    /// </summary>
    public class DestroyOnContact : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) => Destroy(gameObject);
    }
}
