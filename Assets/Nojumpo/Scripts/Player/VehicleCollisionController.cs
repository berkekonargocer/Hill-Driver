using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo
{
    public class VehicleCollisionController : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Collectable"))
            {
                other.GetComponent<ICollectable>().Collect();
            }
        }
    }
}