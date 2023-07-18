using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class ClickToRestartButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            AudioManager.Instance.RestartBGM();
        }
    }
}