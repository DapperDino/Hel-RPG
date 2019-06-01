using Hel.Targeting;
using Hel.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class HomingProjectileAction : AbilityAction
    {
        [SerializeField] private GameObject projectileToShoot;
        [SerializeField] private int shotsPerTarget;
        [SerializeField] private float delayBetweenShots;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            for (int i = 0; i < shotsPerTarget; i++)
            {
                foreach (ITargetable target in abilityCastData.AbilityActionHandler.CurrentTargets)
                {
                    if (target == null) { continue; }

                    GameObject projectileInstance = GameObject.Instantiate(projectileToShoot, abilityCastData.RightHandTransform.position, Quaternion.identity);

                    projectileInstance.GetComponent<HomeInOnTransform>().Initialise(target.TargetTransform);
                }

                if (i != shotsPerTarget - 1) { yield return new WaitForSeconds(delayBetweenShots); }
            }
            yield return null;
        }
    }
}