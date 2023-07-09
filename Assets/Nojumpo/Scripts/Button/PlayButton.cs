using UnityEngine;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour
    {
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void OnClick() {
            CanvasGroup mainMenuPanelCanvasGroup = GameObject.FindWithTag("UI/Main Menu Panel").GetComponent<CanvasGroup>();
            mainMenuPanelCanvasGroup.alpha = 0;
            mainMenuPanelCanvasGroup.interactable = false;
            mainMenuPanelCanvasGroup.blocksRaycasts = false;

            CanvasGroup levelSelectPanelCanvasGroup = GameObject.FindWithTag("UI/Level Select Panel").GetComponent<CanvasGroup>();
            levelSelectPanelCanvasGroup.alpha = 1;
            levelSelectPanelCanvasGroup.interactable = true;
            levelSelectPanelCanvasGroup.blocksRaycasts = true;
        }
    }
}