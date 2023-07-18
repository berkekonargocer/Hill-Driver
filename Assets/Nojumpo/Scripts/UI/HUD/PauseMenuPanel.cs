using DG.Tweening;
using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class PauseMenuPanel : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        CanvasGroup _pauseMenuBackgroundCanvasGroup;
        [SerializeField] RectTransform panelRectTransform;
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onGamePaused += DisplayPanel;
            GameManager.onGameResumed += ClosePanel;
        }

        void OnDisable() {
            GameManager.onGamePaused -= DisplayPanel;
            GameManager.onGameResumed -= ClosePanel;
        }

        void Awake() {
            SetComponents();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _pauseMenuBackgroundCanvasGroup = GetComponentInChildren<CanvasGroup>();
        }
        
        void DisplayPanel(int numberToDivide) {
            _pauseMenuBackgroundCanvasGroup.alpha = 1;
            _pauseMenuBackgroundCanvasGroup.interactable = true;
            _pauseMenuBackgroundCanvasGroup.blocksRaycasts = true;
            panelRectTransform.DOScale(1, 0.15f).SetUpdate(true);
        }

        void ClosePanel(int numberToMultiply) {
            _pauseMenuBackgroundCanvasGroup.alpha = 0;
            _pauseMenuBackgroundCanvasGroup.interactable = false;
            _pauseMenuBackgroundCanvasGroup.blocksRaycasts = false;
            panelRectTransform.DOScale(0, 0.15f).SetUpdate(true);
        }
    }
}