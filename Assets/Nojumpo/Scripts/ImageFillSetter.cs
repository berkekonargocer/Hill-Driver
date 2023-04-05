using Nojumpo.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Nojumpo.UI
{
    public class ImageFillSetter : MonoBehaviour
    {
        #region Fields

        private Image _imageToSetFill;

        private float _oldValue;

        [SerializeField] private FloatReference _currentValue;
        [SerializeField] private FloatReference _maximumValue;

        #endregion

        #region Unity Methods

        #region Awake

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #region Update

        private void Update()
        {
            if (_currentValue.Value != _oldValue)
            {
                SetImageFill();
            }
        }

        #endregion

        #endregion

        #region Custom Private Methods

        private void SetComponents()
        {
            _imageToSetFill = GetComponent<Image>();

            _oldValue = _currentValue.Value;
        }

        private void SetImageFill()
        {
            _imageToSetFill.fillAmount = Mathf.Clamp01(_currentValue.Value / _maximumValue.Value);
            _oldValue = _currentValue.Value;
        }

        #endregion
    }
}