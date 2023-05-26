using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Nojumpo/Scriptable Objects/Datas/Variable/New Float Variable")]
    public class FloatVariableSO : ScriptableObject, IVariableSO<float>
    {
#if UNITY_EDITOR

        [Multiline]
        [SerializeField] string developerDescription = string.Empty;

#endif

        [Tooltip("Float value to use")]
        [SerializeField] float _value;
        public float Value { get { return _value; } set { _value = value; } }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void SetValue(float value) {
            Value = value;
        }

        public void SetValue(FloatVariableSO value) {
            Value = value.Value;
        }

        public void ApplyChange(float changeAmount) {
            Value += changeAmount;
        }

        public void ApplyChange(FloatVariableSO changeAmount) {
            Value += changeAmount.Value;
        }
    }
}
