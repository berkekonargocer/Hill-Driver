using System;
using Nojumpo.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Nojumpo
{
    [RequireComponent(typeof(Slider))]
    public class SliderMaxAndMinValueSetterInt : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        Slider _slider;
        [SerializeField] IntReference minValue;
        [SerializeField] IntReference maxValue;

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            _slider = GetComponent<Slider>();
        }

        void Start() {
            Invoke(nameof(SetValues), 0.1f);
        }

        
        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetValues() {
            _slider.minValue = minValue.Value;
            _slider.maxValue = maxValue.Value;
        } 
    }
}
