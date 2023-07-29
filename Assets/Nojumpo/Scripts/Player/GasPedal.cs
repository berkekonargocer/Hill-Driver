using DG.Tweening;
using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo
{
    public class GasPedal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // -------------------------------- FIELDS ---------------------------------
        VehicleController _vehicleController;
        Transform _gasPedalTransform;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }
        
        
        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _vehicleController = GameObject.FindWithTag("Player").GetComponent<VehicleController>();
            _gasPedalTransform = GetComponent<Transform>();
        }
        
        void StepOnGasPedal() {
            if (TransmissionSystem.DriveForward)
            {
                _vehicleController.SetMoveInputY(1);
            }
            else
            {
                _vehicleController.SetMoveInputY(-1);
            }
        }

        void ReleaseGasPedal() {
            _vehicleController.SetMoveInputY(0);
        }

        void OnPointerDownAnimation() {
            _gasPedalTransform.DOScale(0.8f, 0.15f);
        }

        void OnPointerUpAnimation() {
            _gasPedalTransform.DOScale(1f, 0.15f);
        }

        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnPointerDown(PointerEventData eventData) {
            OnPointerDownAnimation();
            StepOnGasPedal();
        }
        
        public void OnPointerUp(PointerEventData eventData) {
            OnPointerUpAnimation();
            ReleaseGasPedal();
        }
    }
}