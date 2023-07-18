using System.Collections;
using Nojumpo.Managers;
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
        void OnEnable() {
            GameManager.onLevelCompleted += MinimumEngineSound;
            GameManager.onLevelCompleted += DisableUpdate;
            GameManager.onGamePaused += PauseAudio;
            GameManager.onGameResumed += ResumeAudio;
        }

        void OnDisable() {
            GameManager.onLevelCompleted -= MinimumEngineSound;
            GameManager.onLevelCompleted -= DisableUpdate;
            GameManager.onGamePaused -= PauseAudio;
            GameManager.onGameResumed -= ResumeAudio;
        }

        void Awake() {
            SetComponents();
        }

        void FixedUpdate() {
            ChangeEngineSoundPitch();
        }

        
        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {

            _vehicleController = GetComponent<VehicleController>();
        }
        
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

            _vehicleEngineSound.pitch = Mathf.Abs(_vehicleController.MoveInput.y) * 2 > _engineSoundMaximumPitch ?
                Mathf.Lerp(_vehicleEngineSound.pitch, _engineSoundMaximumPitch, 0.6f * Time.deltaTime) :
                Mathf.Lerp(_vehicleEngineSound.pitch, Mathf.Abs(_vehicleController.MoveInput.y) * 2, 0.6f * Time.deltaTime);
        }

        void MinimumEngineSound() {
            StartCoroutine(LowerEngineSoundToMinimum());
        }

        void DisableUpdate() {
            enabled = false;
        }

        void PauseAudio(int numberToDivide) {
            _vehicleEngineSound.Pause();
        }

        void ResumeAudio(int numberToMultiply) {
            _vehicleEngineSound.Play();
        }
        
        IEnumerator LowerEngineSoundToMinimum() {
            while (_vehicleEngineSound.pitch > _engineSoundMinimumPitch)
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, _engineSoundMinimumPitch, 0.6f * Time.deltaTime);
                yield return null;
            }
        }
    }
}
