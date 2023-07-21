using UnityEngine;

namespace Nojumpo
{
    public class HowToPlayButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            CanvasGroup tutorialPanelCanvasGroup = GameObject.FindWithTag("UI/Tutorial Panel").GetComponent<CanvasGroup>();
            TutorialHUD tutorialHUD = GameObject.FindWithTag("UI/Tutorial HUD").GetComponent<TutorialHUD>();
            tutorialHUD.SetupTutorialHUD();
            tutorialPanelCanvasGroup.alpha = 1;
            tutorialPanelCanvasGroup.interactable = true;
            tutorialPanelCanvasGroup.blocksRaycasts = true;
        }
    }
}