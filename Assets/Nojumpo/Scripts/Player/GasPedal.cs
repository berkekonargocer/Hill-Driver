using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class GasPedal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // -------------------------------- FIELDS ---------------------------------
        VehicleController _vehicleController;
        Transform _gasPedalTransform;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            SceneManager.sceneLoaded += GasPedal_OnSceneLoaded;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= GasPedal_OnSceneLoaded;
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _vehicleController = GameObject.FindWithTag("Vehicle").GetComponent<VehicleController>();
            _gasPedalTransform = GetComponent<Transform>();
        }
        
        void StepOnGasPedal() {
            if (TransmissionSystem.DRIVE_FORWARD)
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

        void GasPedal_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            SetComponents();
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