using System;
using UnityEngine;

namespace Nojumpo.ScriptableObjects.ScriptableObjectReferences
{
    [Serializable]
    public class ReadOnlyInspectorIntReference
    {
        [Tooltip("On = Use the Constant Value value that set in this script \n" +
            "Off = Use a Integer Variable Scriptable Object value")]
        [SerializeField] bool useConstant = true;

        [Tooltip("Constant Value to use ")]
        [SerializeField] float constantValue;

        [Tooltip("Integer Variable Scriptable Object Value to get the value from")]
        [SerializeField] ReadOnlyInspectorIntVariableSO variable;

        public float Value { get { return useConstant ? constantValue : variable.Value; } }
    }
}