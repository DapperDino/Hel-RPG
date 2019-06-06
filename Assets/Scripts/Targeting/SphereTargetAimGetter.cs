using Hel.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Targeting
{
    /// <summary>
    /// Used to handle logic for getting multiple targets in a sphere.
    /// </summary>
    [Serializable]
    public class SphereTargetAimGetter : TargetGetter
    {
        [SerializeField] private float range = 15f;
        [SerializeField] private float radius = 1f;
        [SerializeField] private int minTargets = 1;
        [SerializeField] private LayerMask layerMask = new LayerMask();

        public override IEnumerator GetTargets()
        {
            //Get the main camera's transform component.
            Transform mainCameraTransform = Camera.main.transform;

            while (true)
            {
                //Clear the current targets.
                CurrentTargets = new List<ITargetable>();

                //Get the desired point to overlap sphere from.
                Vector3 point = GetPoint(mainCameraTransform);

                //Get all colliders in a sphere.
                Collider[] colliders = Physics.OverlapSphere(point, radius);

                //Loop through each of the colliders.
                foreach (Collider collider in colliders)
                {
                    //Get an ITargetable interface component from the collider.
                    ITargetable target = collider.GetComponent<ITargetable>();

                    //Make sure the collider is targetable.
                    if (target == null) { continue; }

                    //Add to the current targets.
                    CurrentTargets.Add(target);
                }

                //Make sure we have enough targets.
                if (CurrentTargets.Count >= minTargets)
                {
                    //Check to see if the left mouse button was pressed this frame.
                    if (PlayerInputManager.LeftClickPressed)
                    {
                        yield return CurrentTargets;
                        yield break;
                    }
                }

                yield return null;
            }
        }

        private Vector3 GetPoint(Transform mainCameraTransform)
        {
            return Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out RaycastHit hit, range, layerMask)
                ? hit.point
                : mainCameraTransform.position + mainCameraTransform.forward * range;
        }
    }
}

