using Hel.Targeting;
using Hel.Utilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to initialise a homing projectile with the currently collected targets.
    /// </summary>
    [Serializable]
    public class HomingProjectileAction : AbilityAction
    {
        [Required] [SerializeField] private GameObject projectileToShoot = null;
        [SerializeField] private int shotsPerTarget = 1;
        [SerializeField] private float delayBetweenShots = 0f;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Loop for the number of shots we wish to fire at each target.
            for (int i = 0; i < shotsPerTarget; i++)
            {
                //Loop through each target that we have collected.
                foreach (ITargetable target in abilityCastData.AbilityActionHandler.CurrentTargets)
                {
                    //Make sure the target still exists.
                    if (target == null) { continue; }

                    //Spawn in the projectile at the player's right hand.
                    GameObject projectileInstance = GameObject.Instantiate(projectileToShoot, abilityCastData.RightHandTransform.position, Quaternion.identity);

                    //Initialise the homing script with the target's transform component.
                    projectileInstance.GetComponent<HomeInOnTransform>().Initialise(target.TargetTransform);
                }

                //Only wait for the delay if it isn't our last shot.
                if (i != shotsPerTarget - 1) { yield return new WaitForSeconds(delayBetweenShots); }
            }

            yield return null;
        }
    }
}