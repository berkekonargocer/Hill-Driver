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
        static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        [Header("VEHICLE VARIABLES")]
        [SerializeField] FloatVariableSO vehicleFuel;

        [Header("GAME STATE VARIABLES")]
        public static bool _isFailed;
        public static bool _isReachedToEnd;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            SceneManager.sceneLoaded += ResetVariables;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= ResetVariables;
        }

        void Awake() {
            InitializeSingleton();
        }

        void Update() {
            if (_isFailed && Input.GetKeyDown(KeyCode.Return))
            {
                LevelManager.Instance.RestartLevel();
            }

            if (!_isFailed && _isReachedToEnd && Input.GetKeyDown(KeyCode.Return))
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
            _isFailed = false;
            _isReachedToEnd = false;
            vehicleFuel.Value = 1.0f;
        }

        void FailedToReachToEnd() {
            _isFailed = true;
            // UI state
        }

        IEnumerator LevelCompleted() {
            yield return new WaitForSeconds(3.5f);
            _isReachedToEnd = true;
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void CheckIfReachedToEnd() {
            if (!_isReachedToEnd)
            {
                FailedToReachToEnd();
            }
        }

        public void CallLevelCompletedCoroutine() {
            StartCoroutine(LevelCompleted());
        }
    }
}
