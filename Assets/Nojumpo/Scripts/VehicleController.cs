using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nojumpo
{
    public class VehicleController : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private Rigidbody2D _frontTireRigidbody2D;
        [SerializeField] private Rigidbody2D _backTireRigidbody2D;
        private Rigidbody2D _vehicleRigidbody2D;

        [Header("VEHICLE MOVEMENT SETTINGS")]
        [SerializeField] private float _vehicleMovementSpeed = 150.0f;
        [SerializeField] private float _vehicleRotationSpeed = 300.0f;
        private Vector2 _moveInput = Vector2.zero;

        [Header("VEHICLE FUEL SETTINGS")]
        [SerializeField] private FloatVariableSO _vehicleFuel;
        [SerializeField] private float _fuelDrainAmount = -0.0001f;


        [Header("VEHICLE ENGINE SOUND SETTINGS")]
        [SerializeField] private AudioSource _vehicleEngineSound;
        [SerializeField] private float _engineSoundMinimumPitch;
        [SerializeField] private float _engineSoundMaximumPitch;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            SetComponents();
        }

        private void FixedUpdate() {
            if (_vehicleFuel.Value > 0)
            {
                ApplyCarMovement();
                DrainFuel();
            }
            ChangeEngineSoundPitch();
        }


        // ------------------------ INPUT METHODS ------------------------
        private void OnMove(InputValue inputValue) {
            _moveInput = inputValue.Get<Vector2>();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _vehicleRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void ApplyCarMovement() {
            _frontTireRigidbody2D.AddTorque(-_moveInput.y * _vehicleMovementSpeed * Time.fixedDeltaTime);
            _backTireRigidbody2D.AddTorque(-_moveInput.y * _vehicleMovementSpeed * Time.fixedDeltaTime);
            _vehicleRigidbody2D.AddTorque(_moveInput.y * _vehicleRotationSpeed * Time.fixedDeltaTime);
        }

        private void DrainFuel() {
            if (_moveInput != Vector2.zero)
            {
                _vehicleFuel.ApplyChange(_fuelDrainAmount);
            }
        }

        private void ChangeEngineSoundPitch() {
            if (_vehicleFuel.Value <= 0)
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, 0, 2f * Time.deltaTime);
                return;
            }

            if (_moveInput.y == 0)
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, _engineSoundMinimumPitch, 0.6f * Time.deltaTime);
                return;
            }

            if (Mathf.Abs(_moveInput.y) * 2 > _engineSoundMaximumPitch)
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, _engineSoundMaximumPitch, 0.6f * Time.deltaTime);
            }
            else if (Mathf.Abs(_moveInput.y) * 2 < _engineSoundMinimumPitch)
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, _engineSoundMinimumPitch, 0.6f * Time.deltaTime);
            }
            else
            {
                _vehicleEngineSound.pitch = Mathf.Lerp(_vehicleEngineSound.pitch, Mathf.Abs(_moveInput.y) * 2, 0.6f * Time.deltaTime);
            }
        }
    }
}