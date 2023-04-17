using Nojumpo.ScriptableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Managers
{
    public class GameManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        [Header("VEHICLE VARIABLES")]
        [SerializeField] private FloatVariableSO _vehicleFuel;

        [Header("GAME LEVEL VARIABLES")]
        private int _levelCount;

        [Header("GAME STATE VARIABLES")]
        public static bool _isDead = false;
        public static bool _isLevelCompleted = false;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void OnEnable() {
            SceneManager.sceneLoaded += ResetVariables;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= ResetVariables;
        }

        private void Awake() {
            InitializeSingleton();
            _levelCount = SceneManager.sceneCountInBuildSettings;
            Time.timeScale = 0.0f;
        }

        private void Update() {
            if (_isDead && Input.GetKeyDown(KeyCode.Return))
            {
                RestartGame();
            }

            if (!_isDead && _isLevelCompleted && Input.GetKeyDown(KeyCode.Return))
            {
                // Next Level
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

        private void ResetVariables(Scene scene, LoadSceneMode loadSceneMode) {
            _isDead = false;
            _isLevelCompleted = false;
            _vehicleFuel.Value = 1.0f;
        }

        public void CheckIfReachedToEnd() {
            if (!_isLevelCompleted)
            {
                _isDead = true;
            }
        }

        private IEnumerator LevelCompleted() {
            yield return new WaitForSeconds(3.5f);
            _isLevelCompleted = true;
        }

        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void StartGame() {
            Time.timeScale = 1;
            AudioManager.Instance.StartBGM();
        }

        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoToNextLevel() {
            if (SceneManager.GetActiveScene().buildIndex + 1 > _levelCount)
                return;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void CallLevelCompletedCoroutine() {
            StartCoroutine(LevelCompleted());
        }
    }
}