using System;
using UnityEngine;
using Nojumpo.ScriptableObjects;

namespace Nojumpo.Variables
{
    [Serializable]
    public class FloatReference
    {
        [Tooltip("On = Use the Constant Value value that set in this script \n" +
            "Off = Use a Float Variable Scriptable Object value")]
        [SerializeField] bool _useConstant = true;

        [Tooltip("Constant Value to use ")]
        [SerializeField] float _constantValue;

        [Tooltip("Float Variable Scriptable Object Value to read from")]
        [SerializeField] FloatVariableSO _variable;

        public float Value { get { return _useConstant ? _constantValue : _variable.Value; } }
    }
}