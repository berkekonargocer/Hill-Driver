using Nojumpo.EditorAttributes;
using UnityEngine;

namespace Nojumpo.ScriptableObjects.Datas.Variable
{
    [CreateAssetMenu(fileName = "NewReadOnlyInspectorFloatVariableSO", menuName = "Nojumpo/Scriptable Objects/Datas/Variable/New Read Only Inspector Float Variable SO")]
    public class ReadOnlyInspectorFloatVariableSO : ScriptableObject
    {
        
#if UNITY_EDITOR

        [Multiline]
        [SerializeField] string _developerDescription = string.Empty;

#endif

        [Tooltip("Float value to use")]
        [ReadOnlyInspector] [SerializeField] float _value;
        public float Value { get { return _value; } set { this._value = value; } }


        public void SetValue(float value) {
            Value = value;
        }

        public void SetValue(ReadOnlyInspectorFloatVariableSO value) {
            Value = value.Value;
        }

        public void ApplyChange(float changeAmount) {
            Value += changeAmount;
        }

        public void ApplyChange(ReadOnlyInspectorFloatVariableSO changeAmount) {
            Value += changeAmount.Value;
        }
    }
}