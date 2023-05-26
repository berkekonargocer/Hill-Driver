using UnityEngine;
using Nojumpo.EditorAttributes;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewReadOnlyInspectorIntVariableSO", menuName = "Nojumpo/Scriptable Objects/Datas/Variable/New ReadOnly Inspector Int Variable SO")]
    public class ReadOnlyInspectorIntVariableSO : ScriptableObject
    {
        
#if UNITY_EDITOR

        [TextArea]
        [SerializeField] string _developerDescription;

#endif
        [ReadOnlyInspector] [SerializeField] int _value;
        public int Value { get { return _value; } set { _value = value; } }
        
        
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void SetValue(int value) {
            Value = value;
        }

        public void SetValue(ReadOnlyInspectorIntVariableSO value) {
            Value = value.Value;
        }

        public void ApplyChange(int changeAmount) {
            Value += changeAmount;
        }

        public void ApplyChange(ReadOnlyInspectorIntVariableSO changeAmount) {
            Value += changeAmount.Value;
        }
    }
}