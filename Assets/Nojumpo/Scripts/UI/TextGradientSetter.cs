using Nojumpo.Variables;
using TMPro;
using UnityEngine;

namespace Nojumpo.UI
{
    public class TextGradientSetter : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        TextMeshProUGUI _textToChangeColor;
        [SerializeField] IntReference currentValue;
        [SerializeField] IntReference maximumValue;
        [SerializeField] Gradient gradient;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }

        void Update() {
            ChangeImageColorWithGradient();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _textToChangeColor = GetComponent<TextMeshProUGUI>();
        }

        void ChangeImageColorWithGradient() {
            float gradientValue = Mathf.Clamp01((float)currentValue.Value / maximumValue.Value);
            _textToChangeColor.color = gradient.Evaluate(gradientValue);
        }
    }
}
