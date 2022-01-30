using UnityEngine;

namespace Nidavellir
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] ItemKind Kind;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"PickupSpawner triggered by: {other.gameObject}");
            var collider = other.gameObject.GetComponent<PlayerCollider>();
            if (collider != null)
            {
                collider.PlayerController.PickUp(this.Kind);
                Destroy(this.gameObject);
            }
        }
    }
}