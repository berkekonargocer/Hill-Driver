using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Nojumpo/Scriptable Objects/Datas/Variables/New Float Variable")]
    public class FloatVariableSO : ScriptableObject
    {
        #region Fields

#if UNITY_EDITOR

        [Multiline]
        [SerializeField] private string _developerDescription = string.Empty;

#endif

        [Tooltip("Float value to use")]
        [SerializeField] private float _value;

        public float Value { get { return _value; } set { this._value = value; } }

        #endregion



        #region Public Methods

        public void SetValue(float value)
        {
            Value = value;
        }

        public void SetValue(FloatVariableSO value)
        {
            Value = value.Value;
        }

        public void ApplyChange(float changeAmount)
        {
            Value += changeAmount;
        }

        public void ApplyChange(FloatVariableSO changeAmount)
        {
            Value += changeAmount.Value;
        }

        #endregion
    } 
}
