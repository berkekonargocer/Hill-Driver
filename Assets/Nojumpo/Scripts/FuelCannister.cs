using Nojumpo.ScriptableObjects;
using UnityEngine;

namespace Nojumpo
{
    public class FuelCannister : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] private FloatVariableSO _vehicleFuel;
        [SerializeField] private float _fuelRefillAmount;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        private void OnTriggerEnter2D(Collider2D collision) {
            RefillFuel();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        private void RefillFuel() {
            _vehicleFuel.ApplyChange(_fuelRefillAmount);
            Destroy(gameObject);
        }
    }
}