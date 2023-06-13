using Nojumpo.Interfaces;
using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour, IButton
    {
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void OnClick() {
            GameObject.Find("Main Menu Panel").SetActive(false);
            GameObject.Find("Menu Background").SetActive(false);
            LevelManager.Instance.StartGame();
        }
    }
}