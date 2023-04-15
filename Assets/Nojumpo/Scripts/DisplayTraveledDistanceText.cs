using TMPro;
using UnityEngine;

namespace Nojumpo
{
    public class DisplayTraveledDistanceText : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _traveledDistanceText;
        [SerializeField] private Transform _vehicleTransform;

        private Vector2 _vehicleStartPosition = Vector2.zero;
        private Vector2 _traveledDistance = Vector2.zero;
        private Vector2 _lastTraveledDistance = Vector2.zero;

        // ------------------------- UNITY BUILT-IN METHODS ------------------------

        private void Awake() {
            _vehicleStartPosition = _vehicleTransform.position;
        }

        private void Update() {
            
            CalculateTraveledDistance();

            if (_lastTraveledDistance != _traveledDistance)
            {
                TraveledDistanceToText();
                _lastTraveledDistance = _traveledDistance;
            }
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        private void CalculateTraveledDistance() {
            _traveledDistance = (Vector2)_vehicleTransform.position - _vehicleStartPosition;
            _traveledDistance.y = 0;

            if (_traveledDistance.x < 0)
            {
                _traveledDistance.x = 0;
            }
        }

        private void TraveledDistanceToText() {
            _traveledDistanceText.text = $"{_traveledDistance.x.ToString("F0")}m";
        }
    }
}