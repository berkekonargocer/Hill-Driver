using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo
{
    public class HowToPlayButton : MonoBehaviour, IButton
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] GameObject _tutorialPanel;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------

        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------

        public void OnClick() {
            _tutorialPanel.SetActive(true);
        }
    }
}