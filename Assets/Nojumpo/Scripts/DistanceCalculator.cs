using Nojumpo.ScriptableObjects;
using UnityEngine;

namespace Nojumpo
{
    public class DistanceCalculator : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] Transform currentPosition;
        [SerializeField] Transform arrivingDestination;

        [SerializeField] ReadOnlyInspectorIntVariableSO currentDistance;
        [SerializeField] ReadOnlyInspectorIntVariableSO totalDistance;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            Invoke(nameof(CalculateTotalDistance), 0.1f);
        }
        void Update() {
            CalculateCurrentDistance();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void CalculateCurrentDistance() {
            currentDistance.Value = (int)Mathf.Abs(currentPosition.transform.position.x - arrivingDestination.transform.position.x);
        }

        void CalculateTotalDistance() {
            totalDistance.Value = currentDistance.Value;
        } 
    }
}
