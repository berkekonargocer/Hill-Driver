using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class ClickToRestartButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            GameObject.FindWithTag("Player").SetActive(false);
            GameObject.FindWithTag("UI/Menu Canvas").SetActive(false);
            GameObject.FindWithTag("UI/HUD Canvas").SetActive(false);
            GameObject.FindWithTag("Environment").SetActive(false);
            LevelManager.Instance.CallLoadLevelCoroutine(currentLevel);
            AudioManager.Instance.SelectBGMAudioClipAndPlay(currentLevel - 1);
        }
    }
}