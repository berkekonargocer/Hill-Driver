using Nojumpo.Interfaces;
using Nojumpo.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Nojumpo.UI
{
    public class DisplayTraveledDistanceText : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [Header("COMPONENTS")]
        [SerializeField] IntVariableSO currentDistance;
        TextMeshProUGUI _traveledDistanceText;

        int _oldValue;

        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------

        private void Awake() {
            _traveledDistanceText = GetComponent<TextMeshProUGUI>();
        }

        private void Update() {
            if (currentDistance.Value != _oldValue)
            {
                TraveledDistanceToText();
            }
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        private void TraveledDistanceToText() {
            _traveledDistanceText.text = $"{currentDistance.Value}m";
            _oldValue = currentDistance.Value;
        }
    }
}
