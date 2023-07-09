using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour
    {
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void OnClick(int level) {
            GameObject.Find("Menu Canvas").SetActive(false);
            LevelManager.Instance.StartGame(level);
        }
    }
}