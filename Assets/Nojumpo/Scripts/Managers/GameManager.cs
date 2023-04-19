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

        [Header("GAME STATE VARIABLES")]
        public static bool _isFailed = false;
        public static bool _isReachedToEnd = false;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void OnEnable() {
            SceneManager.sceneLoaded += ResetVariables;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= ResetVariables;
        }

        private void Awake() {
            InitializeSingleton();
        }

        private void Update() {
            if (_isFailed && Input.GetKeyDown(KeyCode.Return))
            {
                LevelManager.Instance.RestartGame();
            }

            if (!_isFailed && _isReachedToEnd && Input.GetKeyDown(KeyCode.Return))
            {
                LevelManager.Instance.CallLoadNextLevelCoroutine();
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
            _isFailed = false;
            _isReachedToEnd = false;
            _vehicleFuel.Value = 1.0f;
        }

        private void FailedToReachToEnd() {
            _isFailed = true;
            // UI state
        }

        private IEnumerator LevelCompleted() {
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