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
        private Rigidbody2D _vehicleRigidbody2D;

        [Header("GAME LEVEL VARIABLES")]
        private int _levelCount;

        [Header("GAME STATE VARIABLES")]
        private static bool _isOutOfFuelCoroutineCalled = false;
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
            //if (!_isOutOfFuelCoroutineCalled && !_isDead && _vehicleFuel.Value <= 0)
            //{
            //    StartCoroutine(OutOfFuel());
            //}

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
            _vehicleRigidbody2D = GameObject.Find("Vehicle").GetComponent<Rigidbody2D>();
            _isDead = false;
            _isLevelCompleted = false;
            _isOutOfFuelCoroutineCalled = false;
            _vehicleFuel.Value = 1.0f;
        }

        private IEnumerator OutOfFuelCoroutine() {
            _isOutOfFuelCoroutineCalled = true;

            yield return new WaitForSeconds(3.0f);

            if (!_isLevelCompleted)
            {
                OutOfFuel();
            }
        }

        private void OutOfFuel() {
            _isDead = true;
            _vehicleRigidbody2D.bodyType = RigidbodyType2D.Static;
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

        public void StartLevelCompletedCoroutine() {
            StartCoroutine(LevelCompleted());
        }
    }
}