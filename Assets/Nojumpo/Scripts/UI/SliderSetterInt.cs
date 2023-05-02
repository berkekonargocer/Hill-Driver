using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Nojumpo.UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderSetterInt : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [Tooltip("Slider to set")]
        Slider _slider;

        [Tooltip("Float Variable Scriptable Object Value to equalize to the Slider value")]
        [SerializeField] IntVariableSO variable;


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
