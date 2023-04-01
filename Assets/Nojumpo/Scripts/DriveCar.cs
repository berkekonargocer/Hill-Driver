using UnityEngine;
using UnityEngine.InputSystem;

namespace Nojumpo
{
    public class DriveCar : MonoBehaviour
    {
        #region Fields

        #region Components

        [SerializeField] private Rigidbody2D _frontTireRigidbody2D;
        [SerializeField] private Rigidbody2D _backTireRigidbody2D;

        #endregion

        #region Car Movement Settings

        [Header("Car Movement Settings")]
        [SerializeField] private float _carSpeed = 150.0f;

        private Vector2 _moveInput = Vector2.zero;
        #endregion

        #endregion



        #region Unity Methods

        #region Awake and Start

        private void Awake() {

        }

        private void Start() {

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

        private void ApplyCarMovement() {
            _frontTireRigidbody2D.AddTorque(-_moveInput.x * _carSpeed * Time.fixedDeltaTime);
            _backTireRigidbody2D.AddTorque(-_moveInput.x * _carSpeed * Time.fixedDeltaTime);
        }

        #endregion

        #region Custom Public Methods



        #endregion
    }
}