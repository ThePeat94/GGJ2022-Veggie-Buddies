using UnityEngine;

namespace Nidavellir
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] ItemKind Kind;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"PickupSpawner triggered by: {other.gameObject}");
            var controller = other.gameObject.GetComponentInParent<PlayerController>();
            if (controller != null)
            {
                controller.PickUp(this.Kind);
            }
        }
    }
}