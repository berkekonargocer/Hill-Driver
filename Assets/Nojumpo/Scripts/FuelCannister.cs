using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nojumpo
{
    public class FuelCannister : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] [FormerlySerializedAs("_vehicleFuel")] FloatVariableSO vehicleFuel;
        [SerializeField] [FormerlySerializedAs("_fuelRefillAmount")] float fuelRefillAmount;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnTriggerEnter2D(Collider2D collision) {
            RefillFuel();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void RefillFuel() {
            vehicleFuel.ApplyChange(fuelRefillAmount);
            Destroy(gameObject);
        }
    }
}
