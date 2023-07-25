using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Nojumpo/Scriptable Objects/Data/Variable/New Float Variable")]
    public class FloatVariableSO : ScriptableObject, IVariableSO<float>
    {
#if UNITY_EDITOR

        [Multiline]
        [SerializeField] string developerDescription = string.Empty;

#endif
        
        [Tooltip("Float value to use")]
        [SerializeField] float value;

        [SerializeField] bool useMinMaxValue;

        [SerializeField] float minValue;
        [SerializeField] float maxValue;

        public float Value { get { return value; } set { this.value = value; } }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void SetValue(float value) {
            if (!useMinMaxValue)
            {
                Value = value;
                return;
            }

            if (value < minValue)
            {
                Debug.LogWarning($"Value can not be less than {minValue}");
            }
            else if (value > maxValue)
            {
                Debug.LogWarning($"Value can not be more than {maxValue}");
            }
            else
            {
                Value = value;
            }
        }

        public void SetValue(FloatVariableSO value) {
            if (!useMinMaxValue)
            {
                Value = value.value;
                return;
            }

            if (value.value < minValue)
            {
                Debug.LogWarning($"Value can not be less than {minValue}");
            }
            else if (value.value > maxValue)
            {
                Debug.LogWarning($"Value can not be more than {maxValue}");
            }
            else
            {
                Value = value.value;
            }
        }

        public void ApplyChange(float changeAmount) {
            if (!useMinMaxValue)
            {
                Value += changeAmount;
                return;
            }

            if (Value + changeAmount > maxValue)
            {
                Value = maxValue;
            }
            else if (Value + changeAmount < minValue)
            {
                Value = minValue;
            }
            else
            {
                Value += changeAmount;
            }
        }

        public void ApplyChange(FloatVariableSO changeAmount) {
            if (!useMinMaxValue)
            {
                Value += changeAmount.value;
                return;
            }

            if (Value + changeAmount.value > maxValue)
            {
                Value = maxValue;
            }
            else if (Value + changeAmount.value < minValue)
            {
                Value = minValue;
            }
            else
            {
                Value += changeAmount.value;
            }
        }

        public void SetToMaxValue() {
            value = maxValue;
        }
    }
}
