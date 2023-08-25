using Nojumpo.ScriptableObjects;
using Nojumpo.Variables;
using UnityEngine;
using UnityEngine.Audio;

namespace Nojumpo.Systems.AudioMixerSystem
{
    public class MixerParameterSetter : MonoBehaviour
    {
        /// <summary>
        /// Takes the _variable and changes the Mixer Parameter value by the _minimum and _maximum values using _curve
        /// </summary>
        [Tooltip("Mixer to set parameters on")]
        [SerializeField] AudioMixer _mixer;

        [Tooltip("Name of the mixer parameter to read and set its value")]
        [SerializeField] string _parameterName;

        [Tooltip("Float Variable Scriptable Object Value to send to the mixer parameter")]
        [SerializeField] FloatVariableSO _variable;

        [Tooltip("Minimum value of the Variable that is mapped to the curve")]
        [SerializeField] FloatReference _minimum;

        [Tooltip("Maximum value of the Variable that is mapped to the curve")]
        [SerializeField] FloatReference _maximum;

        [Tooltip("Curve to evaluate in order to look up a final value to send as the parameter \n" +
            "T=0 when Variable == Min \n" +
            "T=1 when Variable == Max")]
        [SerializeField] AnimationCurve _curve;
        
        
        void Update() {
            float f = Mathf.InverseLerp(_minimum.Value, _maximum.Value, _variable.Value);
            float value = _curve.Evaluate(Mathf.Clamp01(f));
            _mixer.SetFloat(_parameterName, value);
        }
    }
}