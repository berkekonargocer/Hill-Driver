using System;
using Nojumpo.ScriptableObjects.Datas.Variable;
using UnityEngine;

namespace Nojumpo.ScriptableObjects.ScriptableObjectReferences
{
    [Serializable]
    public class ReadOnlyInspectorFloatReference
    {
        [Tooltip("On = Use the Constant Value value that set in this script \n" +
            "Off = Use a Float Variable Scriptable Object value")]
        [SerializeField] bool useConstant = true;

        [Tooltip("Constant Value to use ")]
        [SerializeField] float constantValue;

        [Tooltip("Float Variable Scriptable Object Value to read from")]
        [SerializeField] ReadOnlyInspectorFloatVariableSO variable;

        public float Value { get { return useConstant ? constantValue : variable.Value; } }
    }
}