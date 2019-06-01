using Hel.Combat;
using UnityEngine;

namespace Hel.OnContact
{
    public class DealDamageOnContact : MonoBehaviour
    {
        [SerializeField] private DamageData damageData;

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable == null) { return; }

            damageable.DealDamage(damageData);
        }
    }
}

