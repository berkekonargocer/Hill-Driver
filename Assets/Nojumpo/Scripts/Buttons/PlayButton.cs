using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour
    {
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void Play() {
            GameObject.Find("Vehicle").GetComponent<AudioSource>().Play();
            GameObject.Find("Start Game Panel").SetActive(false);
            GameManager.Instance.StartGame();
        }
    }
}