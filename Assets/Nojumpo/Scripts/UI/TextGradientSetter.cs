using System.Collections;
using Nojumpo.ScriptableObjects.ScriptableObjectReferences;
using TMPro;
using UnityEngine;

namespace Nojumpo.UI
{
    public class TextGradientSetter : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        TextMeshProUGUI _textToChangeColor;
        [SerializeField] ReadOnlyInspectorIntReference currentValue;
        [SerializeField] ReadOnlyInspectorIntReference maximumValue;
        [SerializeField] Gradient gradient;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }

        void Start() {
            StartCoroutine(ChangeImageColorWithGradient());
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _textToChangeColor = GetComponent<TextMeshProUGUI>();
        }

        IEnumerator ChangeImageColorWithGradient() {
            yield return new WaitForSeconds(0.25f);
            
            while (true)
            {
                float gradientValue = Mathf.Clamp01((float)currentValue.Value / maximumValue.Value);
                _textToChangeColor.color = gradient.Evaluate(gradientValue);
                yield return null;
            }
        }
    }
}
