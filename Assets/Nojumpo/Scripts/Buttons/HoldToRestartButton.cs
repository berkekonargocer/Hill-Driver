using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo
{
    public class HoldToRestartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // -------------------------------- FIELDS --------------------------------
        [SerializeField] float holdDownTime = 1.0f;

        // ------------------------ CUSTOM PUBLIC METHODS -------------------------

        public void OnPointerDown(PointerEventData pointerEventData) {
            LevelManager.Instance.StartHoldDownToRestartLevelCoroutine(holdDownTime);
        }

        public void OnPointerUp(PointerEventData pointerEventData) {
            LevelManager.Instance.StopHoldDownToRestartLevelCoroutine(holdDownTime);
        }
        
    }
}
