using UnityEngine;

namespace Nojumpo
{
    public class SettingsButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            CanvasGroup mainMenuPanelCanvasGroup = GameObject.FindWithTag("UI/Main Menu Panel").GetComponent<CanvasGroup>();
            mainMenuPanelCanvasGroup.alpha = 0;
            mainMenuPanelCanvasGroup.interactable = false;
            mainMenuPanelCanvasGroup.blocksRaycasts = false;
            
            CanvasGroup settingsPanelCanvasGroup = GameObject.FindWithTag("UI/Settings Panel").GetComponent<CanvasGroup>();
            settingsPanelCanvasGroup.alpha = 1;
            settingsPanelCanvasGroup.interactable = true;
            settingsPanelCanvasGroup.blocksRaycasts = true;
        }
    }
}