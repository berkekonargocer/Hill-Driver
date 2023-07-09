using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class NextLevelButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void LoadNextLevel() {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            GameObject.FindWithTag("UI/Menu Canvas").SetActive(false);
            GameObject.FindWithTag("UI/HUD Canvas").SetActive(false);
            GameObject.FindWithTag("Environment").SetActive(false);
            GameObject.FindWithTag("Player").SetActive(false);
            AudioManager.Instance.SelectBGMAudioClipAndPlay(currentLevel);
            LevelManager.Instance.CallLoadLevelCoroutine(currentLevel + 1);
        }
    }
}