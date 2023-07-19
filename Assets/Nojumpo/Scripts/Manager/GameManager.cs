using System;
using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Managers
{
    public class GameManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        public static event Action onLevelCompleted;
        public static event Action<int> onGamePaused;
        public static event Action<int> onGameResumed;
        
        [Header("SINGLETON")]
        static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        [Header("VEHICLE VARIABLES")]
        [SerializeField] FloatVariableSO vehicleFuel;
        
        static bool IS_PAUSED;
        public bool IsLevelCompleted { get; private set; }


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            SceneManager.sceneLoaded += ResetVariables;
            onLevelCompleted += LevelCompleted;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= ResetVariables;
            onLevelCompleted -= LevelCompleted;
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

        void ResetVariables(Scene scene, LoadSceneMode loadSceneMode) {
            Time.timeScale = 1;
            IsLevelCompleted = false;
            IS_PAUSED = false;
            vehicleFuel.Value = 1.0f;
        }

        void LevelCompleted() {
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
