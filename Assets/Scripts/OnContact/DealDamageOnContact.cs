using Hel.Combat;
using UnityEngine;

namespace Hel.OnContact
{
    /// <summary>
    /// Used to handle dealing damage on contact.
    /// </summary>
    public class DealDamageOnContact : MonoBehaviour
    {
        [SerializeField] private DamageData damageData = new DamageData();

        private void OnTriggerEnter(Collider other)
        {
            //Get an IDamageable interface component from the collider.
            IDamageable damageable = other.GetComponent<IDamageable>();

            //Make sure the collider is damageable.
            if (damageable == null) { return; }

            //Deal damage to the collision object.
            damageable.DealDamage(damageData);
        }
    }
}

