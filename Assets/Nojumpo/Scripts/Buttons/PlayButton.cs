using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------



        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void Play() {
            GameObject.Find("Start Game Panel").SetActive(false);
            GameManager.Instance.StartGame();
        }
    }
}