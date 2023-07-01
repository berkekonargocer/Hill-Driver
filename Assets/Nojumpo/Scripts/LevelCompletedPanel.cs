using DG.Tweening;
using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class LevelCompletedPanel : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] RectTransform mainPanelRectTransform;
        [SerializeField] GameObject backgroundPanel;

        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onLevelCompleted += EnableBackgroundPanel;
            GameManager.onLevelCompleted += ScaleMainPanel;
        }

        void OnDisable() {
            GameManager.onLevelCompleted -= EnableBackgroundPanel;
            GameManager.onLevelCompleted -= ScaleMainPanel;
        }

        
        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void EnableBackgroundPanel() {
            backgroundPanel.SetActive(true);
        }

        void ScaleMainPanel() {
            mainPanelRectTransform.DOScale(1, 1);
        }
    }
}