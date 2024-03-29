using System.Collections;
using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class VehicleController : MonoBehaviour
    {
        public static VehicleController VEHICLE_CONTROLLER { get; private set; }
        
        [Header("COMPONENTS")]
        [SerializeField] Rigidbody2D frontTireRigidbody2D;
        [SerializeField] Rigidbody2D backTireRigidbody2D;
        Rigidbody2D _vehicleRigidbody2D;

        [Header("VEHICLE MOVEMENT SETTINGS")]
        [SerializeField] VehicleMovementSettingsSO vehicleMovementSettingsSO;
        const float VEHICLE_ROTATION_SPEED = 175.0f;
        const float ANGULAR_DRAG_ON_OUT_OF_FUEL = 20.0f;
        const float TIME_TO_CHANGE_ANGULAR_DRAG = 10.0f;
        public Vector2 MoveInput { get; private set; } = Vector2.zero;

        [Header("VEHICLE FUEL SETTINGS")]
        [SerializeField] FloatVariableSO vehicleFuel;
        [SerializeField] VehicleFuelSettingsSO vehicleFuelSettingsSO;
        bool _isOutOfFuelMethodCalled;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onLevelCompleted += VehicleController_OnLevelCompleted;
            SceneManager.sceneLoaded += VehicleController_OnSceneLoaded; 
        }

        void OnDisable() {
            GameManager.onLevelCompleted -= VehicleController_OnLevelCompleted;
            SceneManager.sceneLoaded -= VehicleController_OnSceneLoaded;
            VEHICLE_CONTROLLER = null;
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
#if !UNITY_ANDROID
        void OnMove(InputValue inputValue) {
            MoveInput = inputValue.Get<Vector2>();
        }
  #endif


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            VEHICLE_CONTROLLER = this;
            _vehicleRigidbody2D = GetComponent<Rigidbody2D>();
        }

        void ApplyCarMovement() {
            frontTireRigidbody2D.AddTorque(-MoveInput.y * vehicleMovementSettingsSO.MovementSpeed * Time.fixedDeltaTime);
            backTireRigidbody2D.AddTorque(-MoveInput.y * vehicleMovementSettingsSO.MovementSpeed * Time.fixedDeltaTime);
            _vehicleRigidbody2D.AddTorque(MoveInput.y * VEHICLE_ROTATION_SPEED * Time.fixedDeltaTime);
        }

        void DrainFuel() {
            if (MoveInput.y != 0)
            {
                vehicleFuel.ApplyChange(-vehicleFuelSettingsSO.FuelDrainAmount);
            }
        }

        void StopCarSlowly() {
            StartCoroutine(nameof(ChangeVehicleWheelsAngularDrag));
        }

        void DisableUpdate() {
            enabled = false;
        }

        void VehicleController_OnLevelCompleted() {
            StopCarSlowly();
            DisableUpdate();
        }

        void VehicleController_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            SetComponents();
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

        // ------------------------ CUSTOM PUBLIC METHODS -------------------------
        public void SetMoveInputY(float value) {
            MoveInput = new Vector2(MoveInput.x, value);
        }

    }
}
