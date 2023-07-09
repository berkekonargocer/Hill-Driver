using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class MainMenuButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void GoToMainMenu() {
            AudioManager.Instance.StopBGM();
            SceneManager.LoadScene(0);
        }
    }
}
