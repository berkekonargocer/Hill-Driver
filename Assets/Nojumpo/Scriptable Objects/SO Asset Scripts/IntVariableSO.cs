using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Nojumpo/Scriptable Objects/Datas/Variables/New Integer Variable")]
    public class IntVariableSO : ScriptableObject, IVariableSO<int>
    {
        
#if UNITY_EDITOR

        [TextArea]
        [SerializeField] string developerDescription;

#endif

        [SerializeField] int _value;
        public int Value { get { return _value; } set { _value = value; } }
        
        
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void SetValue(int value) {
            Value = value;
        }

        public void SetValue(IntVariableSO value) {
            Value = value.Value;
        }

        public void ApplyChange(int changeAmount) {
            Value += changeAmount;
        }

        public void ApplyChange(IntVariableSO changeAmount) {
            Value += changeAmount.Value;
        }
    }
}