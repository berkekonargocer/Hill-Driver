using Nojumpo.Interfaces;
using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour, IButton
    {
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void OnClick() {
            GameObject.Find("Menu Canvas").SetActive(false);
            LevelManager.Instance.StartGame();
        }
    }
}