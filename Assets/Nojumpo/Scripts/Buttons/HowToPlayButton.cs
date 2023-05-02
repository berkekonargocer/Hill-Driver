using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo
{
    public class HowToPlayButton : MonoBehaviour, IButton
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField]
        GameObject howToPlayPanel;


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
        }
    }
}
