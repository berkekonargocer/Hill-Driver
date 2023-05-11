using Nojumpo.ScriptableObjects;
using UnityEngine;

namespace Nojumpo
{
    public class VehicleSoundsController : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("COMPONENTS")]
        VehicleController _vehicleController;
        [SerializeField] FloatVariableSO _vehicleFuel;

        [Header("VEHICLE ENGINE SOUND SETTINGS")]
        [SerializeField] AudioSource _vehicleEngineSound;
        [SerializeField] float _engineSoundMinimumPitch = 0.3f;
        [SerializeField] float _engineSoundMaximumPitch = 1.5f;

        /// <summary>
        /// Couldn't find any usable audios for these
        /// </summary>
        //[Header("VEHICLE HIT SOUND SETTINGS")]
        //[SerializeField] AudioSource _vehicleHitSound;
        //[SerializeField] float _maximumHitVolume;
        //[SerializeField] float _minimumHitVolume;

        //[Header("VEHICLE WHEEL ROLLING SOUND SETTINGS")]
        //[SerializeField] AudioSource _vehicleWheelRollingSound;

        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            _vehicleController = GetComponent<VehicleController>();
        }

        void FixedUpdate() {
            ChangeEngineSoundPitch();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void ChangeEngineSoundPitch() {
            if (_vehicleFuel.Value <= 0)
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, 0, 2f * Time.deltaTime);
                return;
            }

            if (_vehicleController.MoveInput.y == 0)
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, _engineSoundMinimumPitch, 0.6f * Time.deltaTime);
                return;
            }

            if (Mathf.Abs(_vehicleController.MoveInput.y) * 2 > _engineSoundMaximumPitch)
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, _engineSoundMaximumPitch, 0.6f * Time.deltaTime);
            }
            else
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, Mathf.Abs(_vehicleController.MoveInput.y) * 2, 0.6f * Time.deltaTime);
            }
        }

    }
}
 