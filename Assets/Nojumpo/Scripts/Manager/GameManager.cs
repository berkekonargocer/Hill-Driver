using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Managers
{
    public class GameManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }
        
        public delegate void OnLevelCompleted();
        public static event OnLevelCompleted onLevelCompleted;
        public bool IsLevelCompleted { get; private set; }

        public delegate void OnGamePaused(int volumeDivision);
        public static event OnGamePaused onGamePaused;

        static bool IS_PAUSED;

        public delegate void OnGameResumed(int volumeMultiply);
        public static event OnGameResumed onGameResumed;
        
        [Header("VEHICLE VARIABLES")]
        [SerializeField] FloatVariableSO vehicleFuel;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            SceneManager.sceneLoaded += GameManager_OnSceneLoaded;
            onLevelCompleted += GameManager_OnLevelCompleted;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= GameManager_OnSceneLoaded;
            onLevelCompleted -= GameManager_OnLevelCompleted;
        }

        void Awake() {
            InitializeSingleton();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseOrUnpauseGame();
            }
            
            if (!IsLevelCompleted)
                return;
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LevelManager.Instance.CallLoadLevelCoroutine(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void InitializeSingleton() {
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

        void ResetVariables() {
            Time.timeScale = 1;
            IsLevelCompleted = false;
            IS_PAUSED = false;
            vehicleFuel.Value = 1.0f;
        }

        void GameManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            ResetVariables();
        }
        
        void GameManager_OnLevelCompleted() {
            IsLevelCompleted = true;
        }
        
        public void PauseOrUnpauseGame() {
            AudioManager.Instance.PlayGamePauseAudioSource(IS_PAUSED);
            
            if (IS_PAUSED)
            {
                onGameResumed?.Invoke(4);
                Time.timeScale = 1;
                IS_PAUSED = false;
            }
            else
            {
                onGamePaused?.Invoke(4);
                Time.timeScale = 0;
                IS_PAUSED = true;
            }
        }

        
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void InvokeOnLevelCompleted() {
            onLevelCompleted?.Invoke();
        }

    }
}
