using System.Collections;
using DG.Tweening;
using Nojumpo.Interfaces;
using Nojumpo.ScriptableObjects;
using Nojumpo.Scripts;
using UnityEngine;

namespace Nojumpo
{
    public class FuelCanister : MonoBehaviour, ICollectable
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] RectTransform fuelCanvasElement;
        [SerializeField] AudioSource collectSFX;
        
        [SerializeField] FloatVariableSO vehicleFuel;
        
        [SerializeField] FloatVariableSO fuelAddAmount;
        [SerializeField] float animationTime;


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void AddFuelToVehicle() {
            vehicleFuel.ApplyChange(fuelAddAmount);
        }

        void ShrinkAnimation() {
            transform.DOScale(0, animationTime / 1.75f).SetUpdate(true);
        }

        IEnumerator GoToFuelUIElement() {
            float elapsedTime = 0.0f;

            while (elapsedTime < animationTime)
            {
                Vector3 uIElementPosition = Helper.GetWorldPositionOfCanvasElement(fuelCanvasElement);
                transform.position = Vector3.Lerp(transform.position, uIElementPosition, elapsedTime / (animationTime * 6));
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        void DisableCollider() {
            BoxCollider2D fuelCanisterBoxCollider2D = GetComponent<BoxCollider2D>();
            fuelCanisterBoxCollider2D.enabled = false;
        }

        void DestroySelf(float delayAmount) {
            Destroy(gameObject, delayAmount);
        }

        // ------------------------- CUSTOM PUBLIC METHODS ------------------------
        public void Collect() {
            DisableCollider();
            collectSFX.Play();
            AddFuelToVehicle();
            ShrinkAnimation();
            StartCoroutine(nameof(GoToFuelUIElement));
            DestroySelf(animationTime);
        }
    }
}
