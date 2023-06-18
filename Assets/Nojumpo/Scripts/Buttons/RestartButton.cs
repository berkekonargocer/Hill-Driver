using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo
{
    public class RestartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // -------------------------------- FIELDS --------------------------------
        [SerializeField] float holdDownTime = 2.0f;

        // ------------------------ CUSTOM PUBLIC METHODS -------------------------

        public void OnPointerDown(PointerEventData pointerEventData) {
            LevelManager.Instance.StartHoldDownToRestartLevelCoroutine(holdDownTime);
        }

        public void OnPointerUp(PointerEventData pointerEventData) {
            LevelManager.Instance.IsHoldingDown = false;
            LevelManager.Instance.CurrentHoldDownTime = 0.0f;
            LevelManager.Instance.StopHoldDownToRestartLevelCoroutine(holdDownTime);
        }
        
    }
}
