using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class NextLevelButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void LoadNextLevel() {
            GameObject.FindWithTag("UI/Menu Canvas").SetActive(false);
            GameObject.FindWithTag("UI/HUD Canvas").SetActive(false);
            GameObject.FindWithTag("Environment").SetActive(false);
            GameObject.FindWithTag("Player").SetActive(false);
            LevelManager.Instance.CallLoadNextLevelCoroutine();
        }
    }
}