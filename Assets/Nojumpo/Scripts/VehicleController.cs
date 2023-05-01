using System.Threading.Tasks;
using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Nojumpo
{
    public class VehicleController : MonoBehaviour
    {

        [Header("COMPONENTS")]
        [SerializeField] [FormerlySerializedAs("_frontTireRigidbody2D")] private Rigidbody2D frontTireRigidbody2D;
        [SerializeField] [FormerlySerializedAs("_backTireRigidbody2D")] private Rigidbody2D backTireRigidbody2D;
        private Rigidbody2D _vehicleRigidbody2D;

        [Header("VEHICLE MOVEMENT SETTINGS")]
        private const float VEHICLE_MOVEMENT_SPEED = 40.0f;
        private const float VEHICLE_ROTATION_SPEED = 300.0f;
        private const float ANGULAR_DRAG_ON_OUT_OF_FUEL = 20.0f;
        private const float TIME_TO_CHANGE_ANGULAR_DRAG = 10.0f;
        public Vector2 MoveInput { get; private set; } = Vector2.zero;
        
        [Header("VEHICLE FUEL SETTINGS")]
        [SerializeField] [FormerlySerializedAs("_vehicleFuel")] private FloatVariableSO vehicleFuel;
        [SerializeField] [FormerlySerializedAs("_fuelDrainAmount")] private float fuelDrainAmount = -0.0018f;
        private bool _isOutOfFuelAsyncMethodCalled;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            SetComponents();
        }

        private void FixedUpdate() {
            if (vehicleFuel.Value > 0)
            {
                ApplyCarMovement();
                DrainFuel();
            }

            if (vehicleFuel.Value < 0 && !_isOutOfFuelAsyncMethodCalled)
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
            frontTireRigidbody2D.AddTorque(-MoveInput.y * VEHICLE_MOVEMENT_SPEED * Time.fixedDeltaTime);
            backTireRigidbody2D.AddTorque(-MoveInput.y * VEHICLE_MOVEMENT_SPEED * Time.fixedDeltaTime);
            _vehicleRigidbody2D.AddTorque(MoveInput.y * VEHICLE_ROTATION_SPEED * Time.fixedDeltaTime);
        }

        private void DrainFuel() {
            if (MoveInput != Vector2.zero)
            {
                vehicleFuel.ApplyChange(fuelDrainAmount);
            }
        }

        private async void OutOfFuel() {
            _isOutOfFuelAsyncMethodCalled = true;

            await ChangeVehicleWheelsAngularDrag();

            GameManager.Instance.CheckIfReachedToEnd();
        }

        private async Task ChangeVehicleWheelsAngularDrag() {
            float timeElapsed = 0;
            float angularDragChangeTimeElapsed = 0;

            while (timeElapsed < TIME_TO_CHANGE_ANGULAR_DRAG)
            {
                backTireRigidbody2D.angularDrag = Mathf.MoveTowards(backTireRigidbody2D.angularDrag, ANGULAR_DRAG_ON_OUT_OF_FUEL, angularDragChangeTimeElapsed / TIME_TO_CHANGE_ANGULAR_DRAG);
                frontTireRigidbody2D.angularDrag = Mathf.MoveTowards(frontTireRigidbody2D.angularDrag, ANGULAR_DRAG_ON_OUT_OF_FUEL, angularDragChangeTimeElapsed / TIME_TO_CHANGE_ANGULAR_DRAG);
                angularDragChangeTimeElapsed += Time.deltaTime / 15;
                timeElapsed += Time.deltaTime * 5f;
                await Task.Yield();
            }
        }
    }
}
