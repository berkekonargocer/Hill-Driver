using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour
    {
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void Play() {
            GameObject.Find("Main Menu Panel").SetActive(false);
            LevelManager.Instance.StartGame();
        }
    }
}