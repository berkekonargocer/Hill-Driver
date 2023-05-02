using System;
using UnityEngine;
using Nojumpo.ScriptableObjects;

namespace Nojumpo.Variables
{
    [Serializable]
    public class IntReference
    {
        [Tooltip("On = Use the Constant Value value that set in this script \n" +
            "Off = Use a Float Variable Scriptable Object value")]
        [SerializeField] bool useConstant = true;

        [Tooltip("Constant Value to use ")]
        [SerializeField] int constantValue;

        [Tooltip("Float Variable Scriptable Object Value to read from")]
        [SerializeField] IntVariableSO variable;

        public int Value { get { return useConstant ? constantValue : variable.Value; } }
    }
}