using Hel.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Targeting
{
    [Serializable]
    public class SingleTargetAimGetter : TargetGetter
    {
        [SerializeField] private float range;
        [SerializeField] private LayerMask layerMask;

        public override IEnumerator GetTargets()
        {
            Transform mainCameraTransform = Camera.main.transform;

            while (true)
            {
                CurrentTargets = new List<ITargetable>();

                RaycastHit hit;
                if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out hit, range, layerMask))
                {
                    ITargetable target = hit.collider.GetComponent<ITargetable>();
                    if (target != null)
                    {
                        CurrentTargets.Add(target);
                        if (PlayerInputManager.LeftClickPressed)
                        {
                            yield return CurrentTargets;
                            yield break;
                        }
                    }
                }
                yield return null;
            }
        }
    }
}

