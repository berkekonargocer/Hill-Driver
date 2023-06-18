using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Managers
{
    public class LevelManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        static LevelManager _instance;
        public static LevelManager Instance { get { return _instance; } }

        [Header("GAME LEVEL VARIABLES")]
        int _levelCount;

        [Header("LOADING SCREEN SETTINGS")]
        [SerializeField] GameObject _loadingScreen;


        [SerializeField] float holdDownToRestartTime = 2.0f;
        float _currentHoldDownTime;
        bool _isHoldingDown;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            InitializeSingleton();
            _levelCount = SceneManager.sceneCountInBuildSettings;
            enabled = false;
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(HoldDownToRestartLevelCoroutine(holdDownToRestartTime));
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                StopCoroutine(HoldDownToRestartLevelCoroutine(holdDownToRestartTime));
                _isHoldingDown = false;
                _currentHoldDownTime = 0.0f;
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

        IEnumerator LoadNextLevelCoroutine() {
            if (SceneManager.GetActiveScene().buildIndex + 1 > _levelCount)
                StopCoroutine(LoadNextLevelCoroutine());

            if (_loadingScreen == null)
                _loadingScreen = GameObject.Find("Loading Screen Panel");

            _loadingScreen.SetActive(true);

            AsyncOperation loadScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            loadScene.allowSceneActivation = false;

            while (loadScene.isDone == false)
            {
                if (loadScene.progress >= 0.9f)
                {
                    loadScene.allowSceneActivation = true;
                }

                yield return null;
            }

            if (_loadingScreen != null)
                _loadingScreen.SetActive(false);
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void StartGame() {
            Time.timeScale = 1;
            AudioManager.Instance.StartBGM();
            CallLoadNextLevelCoroutine();
            enabled = true;
        }

        public void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void StartHoldDownToRestartLevelCoroutine(float holdDownTime) {
            StartCoroutine(HoldDownToRestartLevelCoroutine(holdDownTime));
        }

        public void StopHoldDownToRestartLevelCoroutine(float holdDownTime) {
            StopCoroutine(HoldDownToRestartLevelCoroutine(holdDownTime));
            _isHoldingDown = false;
            _currentHoldDownTime = 0.0f;
        }

        public void CallLoadNextLevelCoroutine() {
            StartCoroutine(LoadNextLevelCoroutine());
        }

        IEnumerator HoldDownToRestartLevelCoroutine(float holdDownTime) {
            _isHoldingDown = true;
            
            while (_isHoldingDown)
            {
                _currentHoldDownTime += Time.deltaTime;

                yield return null;

                if (_currentHoldDownTime >= holdDownTime)
                {
                    StopCoroutine(nameof(HoldDownToRestartLevelCoroutine));
                    _isHoldingDown = false;
                    _currentHoldDownTime = 0.0f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
        
    }
}
