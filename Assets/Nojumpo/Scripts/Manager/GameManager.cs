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
        
        [Header("SINGLETON")]
        static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        [Header("VEHICLE VARIABLES")]
        [SerializeField] FloatVariableSO vehicleFuel;

        public bool IsLevelCompleted { get; private set; }
        

        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            // SetPersonalBestPlayerPrefs();
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
            IsLevelCompleted = false;
            vehicleFuel.Value = 1.0f;
        }

        void LevelCompleted() {
            IsLevelCompleted = true;
        }

        // void SetPersonalBestPlayerPrefs() {
        //     int totalLevelAmount = SceneManager.sceneCountInBuildSettings - 1;
        //     
        //     for (int i = 1; i < totalLevelAmount; i++)
        //     {
        //         string levelPBPlayerPrefsKey = $"Level {i} Personal Best";
        //         
        //         if (PlayerPrefs.GetFloat(levelPBPlayerPrefsKey) <= 0)
        //         {
        //             PlayerPrefs.SetFloat(levelPBPlayerPrefsKey, 900);
        //         }
        //     }
        // }

        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void InvokeOnLevelCompleted() {
            onLevelCompleted?.Invoke();
        }
    }
}
