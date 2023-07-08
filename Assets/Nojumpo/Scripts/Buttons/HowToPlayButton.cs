using UnityEngine;

namespace Nojumpo
{
    public class HowToPlayButton : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] GameObject _tutorialPanel;
        
        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------

        public void OnClick() {
            _tutorialPanel.SetActive(true);
        }
    }
}