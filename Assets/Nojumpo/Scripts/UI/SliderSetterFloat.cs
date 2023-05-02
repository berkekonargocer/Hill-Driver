using System;
using UnityEngine;
using UnityEngine.UI;
using Nojumpo.ScriptableObjects;

namespace Nojumpo.UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderSetterFloat : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [Tooltip("Slider to set")]
        Slider _slider;

        [Tooltip("Float Variable Scriptable Object Value to equalize to the Slider value")]
        [SerializeField] FloatVariableSO variable;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            _slider = GetComponent<Slider>();
        }
        
        void Update() {
            if (_slider != null && variable != null)
            {
                _slider.value = variable.Value;
            }
        }
    }
}
