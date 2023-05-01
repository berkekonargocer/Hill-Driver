using Nojumpo.Interfaces;
using UnityEngine;
using UnityEngine.Rendering;

namespace Nojumpo
{
    public class HowToPlayButton : MonoBehaviour, IButton
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField]
        GameObject howToPlayPanel;

        // ------------------------- UNITY BUILT-IN METHODS ------------------------


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
        }
    }
}
