using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Managers
{
    public class GameManager : MonoBehaviour {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        [Header("GAME LEVEL VARIABLES")]
        private int _levelCount = SceneManager.sceneCountInBuildSettings;

        [Header("GAME STATE VARIABLES")]
        public static bool _isDead;
        public static bool _isLevelCompleted;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            InitializeSingleton();
            PauseGame();
        }

        private void Update() {
            if (_isDead && Input.GetKeyDown(KeyCode.Return))
            {
                RestartGame();
            }
            
            if (!_isDead && _isLevelCompleted && Input.GetKeyDown(KeyCode.Return))
            {

            }
        }

        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void InitializeSingleton() {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void PauseGame() {
            Time.timeScale = 0;
        }

        public void ResumeGame() {
            Time.timeScale = 1;
        }

        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoToNextLevel() {
            if (SceneManager.GetActiveScene().buildIndex + 1 > _levelCount) 
                return;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}