using Hel.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Targeting
{
    [Serializable]
    public class SphereTargetAimGetter : TargetGetter
    {
        [SerializeField] private float range;
        [SerializeField] private float radius;
        [SerializeField] private int minTargets;
        [SerializeField] private LayerMask layerMask;

        public override IEnumerator GetTargets()
        {
            Transform mainCameraTransform = Camera.main.transform;

            while (true)
            {
                CurrentTargets = new List<ITargetable>();

                Vector3 point = GetPoint(mainCameraTransform);

                Collider[] colliders = Physics.OverlapSphere(point, radius);
                foreach (Collider collider in colliders)
                {
                    ITargetable target = collider.GetComponent<ITargetable>();
                    if (target == null) { continue; }
                    CurrentTargets.Add(target);
                }

                if (CurrentTargets.Count >= minTargets)
                {
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

