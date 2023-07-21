using UnityEngine;

namespace Nojumpo
{
    public class BackToMenuButton : MonoBehaviour
    {

        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void BackToMenuFromTutorialPanel() {
            CanvasGroup tutorialPanelCanvasGroup = GameObject.FindWithTag("UI/Tutorial Panel").GetComponent<CanvasGroup>();
            TutorialHUD tutorialHUD = GameObject.FindWithTag("UI/Tutorial HUD").GetComponent<TutorialHUD>();
            tutorialHUD.DisableAnimators();
            tutorialPanelCanvasGroup.alpha = 0;
            tutorialPanelCanvasGroup.interactable = false;
            tutorialPanelCanvasGroup.blocksRaycasts = false;
        }

        public void BackToMenuFromSettings() {
            CanvasGroup settingsPanelCanvasGroup = GameObject.FindWithTag("UI/Settings Panel").GetComponent<CanvasGroup>();
            settingsPanelCanvasGroup.alpha = 0;
            settingsPanelCanvasGroup.interactable = false;
            settingsPanelCanvasGroup.blocksRaycasts = false;

            CanvasGroup mainMenuPanelCanvasGroup = GameObject.FindWithTag("UI/Main Menu Panel").GetComponent<CanvasGroup>();
            mainMenuPanelCanvasGroup.alpha = 1;
            mainMenuPanelCanvasGroup.interactable = true;
            mainMenuPanelCanvasGroup.blocksRaycasts = true;
        }

        public void BackToMenuFromLevelSelectPanel() {
            CanvasGroup mainMenuPanelCanvasGroup = GameObject.FindWithTag("UI/Main Menu Panel").GetComponent<CanvasGroup>();
            mainMenuPanelCanvasGroup.alpha = 1;
            mainMenuPanelCanvasGroup.interactable = true;
            mainMenuPanelCanvasGroup.blocksRaycasts = true;

            CanvasGroup levelSelectPanelCanvasGroup = GameObject.FindWithTag("UI/Level Select Panel").GetComponent<CanvasGroup>();
            levelSelectPanelCanvasGroup.alpha = 0;
            levelSelectPanelCanvasGroup.interactable = false;
            levelSelectPanelCanvasGroup.blocksRaycasts = false;
        }

    }
}