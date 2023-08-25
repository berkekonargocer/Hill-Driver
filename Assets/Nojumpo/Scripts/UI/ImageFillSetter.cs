using Nojumpo.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Nojumpo.UI
{
    public class ImageFillSetter : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        Image _imageToSetFill;
        float _oldValue;
        
        [SerializeField] FloatReference currentValue;
        [SerializeField] FloatReference maximumValue;
        
        [SerializeField] bool useGradientColorChange;
        [SerializeField] Gradient imageGradient;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }

        void Update() {
            if (currentValue.Value != _oldValue)
            {
                SetImageFill();
                
                if (!useGradientColorChange)
                    return;
                
                ChangeImageColorWithGradient();
            }
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _imageToSetFill = GetComponent<Image>();
            _oldValue = currentValue.Value;
        }

        void SetImageFill() {
            _imageToSetFill.fillAmount = Mathf.Clamp01(currentValue.Value / maximumValue.Value);
            _oldValue = currentValue.Value;
        }

        void ChangeImageColorWithGradient() {
            _imageToSetFill.color = imageGradient.Evaluate(_imageToSetFill.fillAmount);
        }
    }
}
