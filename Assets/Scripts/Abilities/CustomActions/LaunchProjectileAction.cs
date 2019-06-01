using Hel.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class LaunchProjectileAction : AbilityAction
    {
        [SerializeField] private GameObject projectileToLaunch;
        [SerializeField] private LaunchType launchType;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float launchSpeed;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            GameObject projectileInstance = GameObject.Instantiate(projectileToLaunch, abilityCastData.RightHandTransform.position, Quaternion.identity);

            Transform mainCameraTransform = Camera.main.transform;

            Vector3 targetPos = mainCameraTransform.position + mainCameraTransform.forward * 1000f;

            RaycastHit hit;
            if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out hit, 1000f, layerMask))
            {
                targetPos = hit.point;
            }

            Vector3 targetDirection = (targetPos - projectileInstance.transform.position).normalized * launchSpeed;

            switch (launchType)
            {
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
            Transform,
            Rigidbody,
        }
    }
}