using Hel.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Combat.Weapons.Bows
{
    public class BowLogic : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerInventory inventory;
        [Required] [SerializeField] private AmmunitionType ammunitionType;
        [Required] [SerializeField] private Transform arrowSpawnTransform;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float arrowLaunchForce;

        private Transform mainCameraTransform;

        private void Start()
        {
            mainCameraTransform = Camera.main.transform;
        }

        public void FireArrow()
        {
            if (!inventory.ItemHolder.HasAmmunition(ammunitionType)) { return; }

            Vector3 targetPos = mainCameraTransform.position + mainCameraTransform.forward * 1000f;

            RaycastHit hit;
            if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out hit, 1000f, layerMask))
            {
                targetPos = hit.point;
            }

            AmmunitionData ammunitionData = inventory.ItemHolder.ConsumeAmmunition(ammunitionType);

            GameObject arrowInstance = Instantiate(ammunitionData.ammunitionPrefab, arrowSpawnTransform.position, Quaternion.Euler(arrowSpawnTransform.forward));

            arrowInstance.GetComponent<Rigidbody>().velocity = (targetPos - arrowInstance.transform.position).normalized * arrowLaunchForce;
        }
    }
}
