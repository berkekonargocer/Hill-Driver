using UnityEngine;
using Nojumpo.Interfaces;

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