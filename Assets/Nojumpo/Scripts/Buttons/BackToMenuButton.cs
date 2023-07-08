using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class BackToMenuButton : MonoBehaviour
    {
        [SerializeField] GameObject tutorialPanel;
        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void BackToMenu() {
            tutorialPanel.SetActive(false);
        }

    }
}