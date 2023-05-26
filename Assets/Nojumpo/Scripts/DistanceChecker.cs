using System;
using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class DistanceChecker : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] GameObject currentPosition;
        [SerializeField] GameObject arrivingDestination;

        [SerializeField] ReadOnlyInspectorIntVariableSO currentDistance;
        [SerializeField] ReadOnlyInspectorIntVariableSO totalDistance;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            Invoke(nameof(CheckTotalDistance), 0.1f);
        }
        void Update() {
            CheckCurrentDistance();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void CheckCurrentDistance() {
            currentDistance.Value = (int)Mathf.Abs(currentPosition.transform.position.x - arrivingDestination.transform.position.x);
        }

        void CheckTotalDistance() {
            totalDistance.Value = currentDistance.Value;
        } 
    }
}
