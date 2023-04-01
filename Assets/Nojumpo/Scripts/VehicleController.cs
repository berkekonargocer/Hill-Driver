using UnityEngine;
using UnityEngine.InputSystem;

namespace Nojumpo
{
    public class VehicleController : MonoBehaviour
    {
        #region Fields

        #region Components

        [SerializeField] private Rigidbody2D _frontTireRigidbody2D;
        [SerializeField] private Rigidbody2D _backTireRigidbody2D;
        private Rigidbody2D _vehicleRigidbody2D;

        #endregion

        #region Vehicle Movement Settings

        [Header("Vehicle Movement Settings")]
        [SerializeField] private float _vehicleMovementSpeed = 150.0f;
        [SerializeField] private float _vehicleRotationSpeed = 300.0f;

        private Vector2 _moveInput = Vector2.zero;
        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake() {
            SetComponents();
        }

        #endregion

        #region Fixed Update

        private void FixedUpdate() {
            ApplyCarMovement();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        #region Input Methods

        private void OnMove(InputValue inputValue) {
            _moveInput = inputValue.Get<Vector2>();
        }

        #endregion

        private void SetComponents() {
            _vehicleRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void ApplyCarMovement() {
            _frontTireRigidbody2D.AddTorque(-_moveInput.y * _vehicleMovementSpeed * Time.fixedDeltaTime);
            _backTireRigidbody2D.AddTorque(-_moveInput.y * _vehicleMovementSpeed * Time.fixedDeltaTime);
            _vehicleRigidbody2D.AddTorque(_moveInput.y * _vehicleRotationSpeed * Time.fixedDeltaTime);
        }

        #endregion
    }
}