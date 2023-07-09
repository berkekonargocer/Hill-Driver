using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class ClickToRestartButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}