using Nojumpo.ScriptableObjects;
using Nojumpo.Scripts;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using Nojumpo.Interfaces;
using UnityEngine.Serialization;

namespace Nojumpo
{
    public class FuelCannister : MonoBehaviour, ICollectable
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] RectTransform fuelCanvasElement;
        
        [SerializeField] FloatVariableSO vehicleFuel;
        
        [SerializeField] float animationTime;
        [SerializeField] float fuelAddAmount;


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void AddFuelToVehicle() {
            vehicleFuel.ApplyChange(fuelAddAmount);
        }

        void ShrinkAnimation() {
            transform.DOScale(0, animationTime / 2);
        }

        IEnumerator GoToFuelUIElement() {
            Vector3 uIElementPosition;
            float elapsedTime = 0.0f;

            while (elapsedTime < animationTime)
            {
                uIElementPosition = Helper.GetWorldPositionOfCanvasElement(fuelCanvasElement);
                transform.position = Vector3.Lerp(transform.position, uIElementPosition, elapsedTime / (animationTime * 6));
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }


        // ------------------------- CUSTOM PUBLIC METHODS ------------------------
        public void Collect() {
            AddFuelToVehicle();
            ShrinkAnimation();
            StartCoroutine(nameof(GoToFuelUIElement));
        }
    }
}
