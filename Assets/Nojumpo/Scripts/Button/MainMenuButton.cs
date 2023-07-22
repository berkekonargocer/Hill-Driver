using Nojumpo.Managers;
using Nojumpo.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class MainMenuButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            AudioManager.Instance.StopBGM();
            TimerManager.Instance.StopTimer();
            GameManager.Instance.SetIsPlaying(false);
            SceneManager.LoadScene(0);
        }
    }
}
