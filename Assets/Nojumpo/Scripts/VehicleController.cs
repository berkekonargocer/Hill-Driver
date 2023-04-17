using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using System.Collections;
using System.Threading.Tasks;
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
        [SerializeField] private float _vehicleMovementSpeed = 40.0f;
        [SerializeField] private float _vehicleRotationSpeed = 300.0f;
        [SerializeField] private float _angularDragOnOutOfFuel = 20.0f;
        [SerializeField] private float _timeToChangeAngularDrag = 10.0f;
        private bool _isChangeAngularDragCoroutineCalled = false;
        public Vector2 MoveInput { get; private set; } = Vector2.zero;

        [Header("VEHICLE FUEL SETTINGS")]
        [SerializeField] private FloatVariableSO _vehicleFuel;
        [SerializeField] private float _fuelDrainAmount = -0.0018f;
        private bool _isOutOfFuelAsyncMethodCalled;


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

            if (_vehicleFuel.Value < 0 && !_isOutOfFuelAsyncMethodCalled)
            {
                OutOfFuel();
            }
        }


        // ---------------------------- INPUT METHODS -----------------------------
        private void OnMove(InputValue inputValue) {
            MoveInput = inputValue.Get<Vector2>();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _vehicleRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void ApplyCarMovement() {
            _frontTireRigidbody2D.AddTorque(-MoveInput.y * _vehicleMovementSpeed * Time.fixedDeltaTime);
            _backTireRigidbody2D.AddTorque(-MoveInput.y * _vehicleMovementSpeed * Time.fixedDeltaTime);
            _vehicleRigidbody2D.AddTorque(MoveInput.y * _vehicleRotationSpeed * Time.fixedDeltaTime);
        }

        private void DrainFuel() {
            if (MoveInput != Vector2.zero)
            {
                _vehicleFuel.ApplyChange(_fuelDrainAmount);
            }
        }

        private async void OutOfFuel() {
            _isOutOfFuelAsyncMethodCalled = true;

            await ChangeVehicleWheelsAngularDrag();

            GameManager.Instance.CheckIfReachedToEnd();
        }

        private async Task ChangeVehicleWheelsAngularDrag() {

            _isChangeAngularDragCoroutineCalled = true;

            float timeElapsed = 0;
            float angularDragChangeTimeElapsed = 0;

            while (timeElapsed < _timeToChangeAngularDrag)
            {
                _backTireRigidbody2D.angularDrag = Mathf.MoveTowards(_backTireRigidbody2D.angularDrag, _angularDragOnOutOfFuel, angularDragChangeTimeElapsed / _timeToChangeAngularDrag);
                _frontTireRigidbody2D.angularDrag = Mathf.MoveTowards(_frontTireRigidbody2D.angularDrag, _angularDragOnOutOfFuel, angularDragChangeTimeElapsed / _timeToChangeAngularDrag);
                angularDragChangeTimeElapsed += Time.deltaTime / 15;
                timeElapsed += Time.deltaTime;
                await Task.Yield();
            }
        }
    }
}