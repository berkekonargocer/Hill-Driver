using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nojumpo
{
    public class FuelCannister : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] [FormerlySerializedAs("_vehicleFuel")] private FloatVariableSO vehicleFuel;
        [SerializeField] [FormerlySerializedAs("_fuelRefillAmount")] private float fuelRefillAmount;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        private void OnTriggerEnter2D(Collider2D collision) {
            RefillFuel();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        private void RefillFuel() {
            vehicleFuel.ApplyChange(fuelRefillAmount);
            Destroy(gameObject);
        }
    }
}
