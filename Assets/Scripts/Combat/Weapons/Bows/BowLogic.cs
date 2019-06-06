using Hel.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Combat.Weapons.Bows
{
    /// <summary>
    /// Handles the logic for firing an arrow from a bow.
    /// </summary>
    public class BowLogic : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerInventory playerInventory = null;
        [Required] [SerializeField] private AmmunitionType ammunitionType = null;
        [Required] [SerializeField] private Transform arrowSpawnTransform = null;
        [SerializeField] private LayerMask layerMask = new LayerMask();
        [SerializeField] private float arrowLaunchForce = 150f;

        private Transform mainCameraTransform;

        private void Start() => mainCameraTransform = Camera.main.transform;

        public void FireArrow()
        {
            //Make sure the player has the required ammunition.
            if (!playerInventory.ItemHolder.HasAmmunition(ammunitionType)) { return; }

            //Use this default target position in case we don't hit the raycast.
            Vector3 targetPos = mainCameraTransform.position + mainCameraTransform.forward * 1000f;

            //Check if we are aiming at something.
            if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out RaycastHit hit, 1000f, layerMask))
            {
                //If we are aiming at something then set that point to be our target position.
                targetPos = hit.point;
            }

            //Get back data about the ammunition we are using.
            AmmunitionData ammunitionData = playerInventory.ItemHolder.ConsumeAmmunition(ammunitionType);

            //Spawn the projectile at the correct position relative to the bow.
            GameObject arrowInstance = Instantiate(ammunitionData.ammunitionPrefab, arrowSpawnTransform.position, Quaternion.Euler(arrowSpawnTransform.forward));

            //Initialise the projectile's rigidbody with velocity in the target's direction.
            arrowInstance.GetComponent<Rigidbody>().velocity = (targetPos - arrowInstance.transform.position).normalized * arrowLaunchForce;
        }
    }
}
