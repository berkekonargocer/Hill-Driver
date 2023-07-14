using System.Collections;
using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nojumpo
{
    public class VehicleController : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] Rigidbody2D frontTireRigidbody2D;
        [SerializeField] Rigidbody2D backTireRigidbody2D;
        Rigidbody2D _vehicleRigidbody2D;

        [Header("VEHICLE MOVEMENT SETTINGS")]
        const float VEHICLE_MOVEMENT_SPEED = 45.0f;
        const float VEHICLE_ROTATION_SPEED = 300.0f;
        const float ANGULAR_DRAG_ON_OUT_OF_FUEL = 20.0f;
        const float TIME_TO_CHANGE_ANGULAR_DRAG = 10.0f;
        public Vector2 MoveInput { get; private set; } = Vector2.zero;

        [Header("VEHICLE FUEL SETTINGS")]
        [SerializeField] FloatVariableSO vehicleFuel;
        [SerializeField] float fuelDrainAmount = -0.0018f;
        bool _isOutOfFuelMethodCalled;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onLevelCompleted += DisableUpdate;
            GameManager.onLevelCompleted += StopCarSlowly;

        }

        void OnDisable() {
            GameManager.onLevelCompleted -= DisableUpdate;
            GameManager.onLevelCompleted -= StopCarSlowly;
        }

        void Awake() {
            SetComponents();
        }

        void FixedUpdate() {
            if (vehicleFuel.Value > 0)
            {
                ApplyCarMovement();
                DrainFuel();
            }

            if (_isOutOfFuelMethodCalled)
                return;
            
            if (vehicleFuel.Value < 0)
            {
                StopCarSlowly();
            }
        }

        // ---------------------------- INPUT METHODS -----------------------------
        void OnMove(InputValue inputValue) {
            MoveInput = inputValue.Get<Vector2>();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _vehicleRigidbody2D = GetComponent<Rigidbody2D>();
        }

        void ApplyCarMovement() {
            frontTireRigidbody2D.AddTorque(-MoveInput.y * VEHICLE_MOVEMENT_SPEED * Time.fixedDeltaTime);
            backTireRigidbody2D.AddTorque(-MoveInput.y * VEHICLE_MOVEMENT_SPEED * Time.fixedDeltaTime);
            _vehicleRigidbody2D.AddTorque(MoveInput.y * VEHICLE_ROTATION_SPEED * Time.fixedDeltaTime);
        }

        void DrainFuel() {
            if (MoveInput != Vector2.zero)
            {
                vehicleFuel.ApplyChange(fuelDrainAmount);
            }
        }
        
        void StopCarSlowly() {
            StartCoroutine(nameof(ChangeVehicleWheelsAngularDrag));
        }

        void DisableUpdate() {
            enabled = false;
        }
        
        IEnumerator ChangeVehicleWheelsAngularDrag() {
            float timeElapsed = 0.0f;
            float angularDragChangeTimeElapsed = 0.0f;
            
            while (timeElapsed < TIME_TO_CHANGE_ANGULAR_DRAG)
            {
                backTireRigidbody2D.angularDrag = Mathf.MoveTowards(backTireRigidbody2D.angularDrag, ANGULAR_DRAG_ON_OUT_OF_FUEL, angularDragChangeTimeElapsed / TIME_TO_CHANGE_ANGULAR_DRAG);
                frontTireRigidbody2D.angularDrag = Mathf.MoveTowards(frontTireRigidbody2D.angularDrag, ANGULAR_DRAG_ON_OUT_OF_FUEL, angularDragChangeTimeElapsed / TIME_TO_CHANGE_ANGULAR_DRAG);
                angularDragChangeTimeElapsed += Time.deltaTime / 15;
                timeElapsed += Time.deltaTime * 5f;
                yield return null;
            }
        }
    }
}
