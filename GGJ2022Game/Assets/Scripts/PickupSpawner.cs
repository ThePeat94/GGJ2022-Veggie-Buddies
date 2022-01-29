using UnityEngine;

namespace Nidavellir
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] ItemKind Kind;

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"PickupSpawner collided with: {other.gameObject}");
            if (other.gameObject.TryGetComponent<PlayerController>(out var controller))
            {
                controller.PickUp(this.Kind);
            }
        }
    }
}