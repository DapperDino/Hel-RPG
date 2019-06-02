using Hel.Utilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to initialise a projectile with movement in a calculated direction.
    /// </summary>
    [Serializable]
    public class LaunchProjectileAction : AbilityAction
    {
        [Required] [SerializeField] private GameObject projectileToLaunch = null;
        [SerializeField] private LaunchType launchType = LaunchType.None;
        [SerializeField] private LayerMask layerMask = new LayerMask();
        [SerializeField] private float launchSpeed = 10f;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Spawn in the projectile at the player's right hand.
            GameObject projectileInstance = GameObject.Instantiate(projectileToLaunch, abilityCastData.RightHandTransform.position, Quaternion.identity);

            //Get the forward direction relative to where the main camera is aiming.
            Transform mainCameraTransform = Camera.main.transform;

            //Initialise the target position in case we don't hit anything with the raycast.
            Vector3 targetPos = mainCameraTransform.position + mainCameraTransform.forward * 1000f;

            //Overwrite the target pos if we hit something with the raycast.
            if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out RaycastHit hit, 1000f, layerMask))
            {
                targetPos = hit.point;
            }

            //Calculate the target direction to launch the projectile.
            Vector3 targetDirection = (targetPos - projectileInstance.transform.position).normalized * launchSpeed;

            //Handle the different launch scenarios.
            switch (launchType)
            {
                case LaunchType.None:
                    Debug.LogError("You've forgotten to set the projectile's launch type.");
                    break;

                case LaunchType.Transform:
                    MoveTransformOverTime transformMovement = projectileInstance.GetComponent<MoveTransformOverTime>();
                    transformMovement.Initialise(targetDirection);
                    break;

                case LaunchType.Rigidbody:
                    Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
                    rb.AddForce(targetDirection, ForceMode.VelocityChange);
                    break;
            }

            yield return null;
        }

        private enum LaunchType
        {
            None = -1,
            Transform,
            Rigidbody,
        }
    }
}