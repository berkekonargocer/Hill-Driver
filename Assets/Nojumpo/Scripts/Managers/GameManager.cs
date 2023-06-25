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
                LevelManager.Instance.CallLoadNextLevelCoroutine();
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
            vehicleFuel.Value = 1.0f;
        }

        void LevelCompleted() {
            IsLevelCompleted = true;
        }

        void StartedToNewLevel() {
            IsLevelCompleted = false;
        }
        
        
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void InvokeOnLevelCompleted() {
            onLevelCompleted?.Invoke();
        }
    }
}
