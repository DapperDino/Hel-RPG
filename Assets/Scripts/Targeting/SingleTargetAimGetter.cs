using Hel.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Targeting
{
    /// <summary>
    /// Used to handle logic for getting a single target.
    /// </summary>
    [Serializable]
    public class SingleTargetAimGetter : TargetGetter
    {
        [SerializeField] private float range = 15f;
        [SerializeField] private LayerMask layerMask = new LayerMask();

        public override IEnumerator GetTargets()
        {
            //Get the main camera's transform component.
            Transform mainCameraTransform = Camera.main.transform;

            while (true)
            {
                //Clear the current targets.
                CurrentTargets = new List<ITargetable>();

                //Check to see if we are looking at anything.
                if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out RaycastHit hit, range, layerMask))
                {
                    //Get an ITargetable interface component from the collider.
                    ITargetable target = hit.collider.GetComponent<ITargetable>();

                    //Make sure the collider is targetable.
                    if (target != null)
                    {
                        //Add to the current targets.
                        CurrentTargets.Add(target);

                        //Check to see if the left mouse button was pressed this frame.
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

