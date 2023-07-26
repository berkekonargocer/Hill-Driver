using System.Collections;
using DG.Tweening;
using Nojumpo.ScriptableObjects;
using Nojumpo.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.PlayerPrefs;

namespace Nojumpo.Managers
{
    public class LevelManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        static LevelManager _instance;
        public static LevelManager Instance { get { return _instance; } }

        [Header("GAME LEVEL VARIABLES")]
        int _totalLevelCount;
        public int CurrentLevel { get; set; }

        CanvasGroup _loadingScreen;

        [SerializeField] float holdDownToRestartTime = 2.0f;
        float _currentHoldDownTime;
        bool _isHoldingDown;
        Image _restartButtonFillImage;
        Transform _restartButtonTransform;

        LevelDetailsSO _levelDetailsSO;
        [SerializeField] LevelDetailsSO[] levelDetailsExceptLvlOne;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            InitializeSingleton();
            SetInitialLockStates();
            _totalLevelCount = SceneManager.sceneCountInBuildSettings;
            SceneManager.sceneLoaded += LevelManager_OnSceneLoaded;
            GameManager.onLevelCompleted += LevelManager_OnLevelCompleted;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= LevelManager_OnSceneLoaded;
            GameManager.onLevelCompleted -= LevelManager_OnLevelCompleted;
        }

        void Update() {
            if (GameManager.Instance.IsLevelCompleted || !GameManager.Instance.IsPlaying)
                return;

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(HoldDownToRestartLevelCoroutine(holdDownToRestartTime));
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                StopHoldDownToRestartLevelCoroutine(holdDownToRestartTime);
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

        void SetComponents() {
            _levelDetailsSO = GameObject.FindWithTag("Level Details")?.GetComponent<LevelDetails>().LevelDetailsSo;
            _restartButtonFillImage = GameObject.FindWithTag("UI/Restart Button Fill Image")?.GetComponent<Image>();
            _restartButtonTransform = GameObject.FindWithTag("UI/Restart Button")?.GetComponent<Transform>();
            _loadingScreen = GameObject.FindWithTag("UI/Loading Screen Canvas").GetComponent<CanvasGroup>();
        }

        void SetInitialLockStates() {
            if (HasKey("Not First Launch"))
                return;

            // Debug.Log("Setting up initial lock states");
            for (int i = 0; i < levelDetailsExceptLvlOne.Length; i++)
            {
                SetInt(levelDetailsExceptLvlOne[i].CurrentLevelLockStatePlayerPrefsKey(), 0);
            }

            SetInt("Not First Launch", 1);
        }

        void UnlockNextLevel() {
            if (GetInt(_levelDetailsSO.NextLevelLockStatePlayerPrefsKey()) == 1 || _levelDetailsSO.LevelNumber == _totalLevelCount)
                return;

            SetInt(_levelDetailsSO.NextLevelLockStatePlayerPrefsKey(), 1);
        }

        void SetLevelLockStates() {
            // Debug.Log("Setting up level lock states");
            for (int i = 0; i < levelDetailsExceptLvlOne.Length; i++)
            {
                levelDetailsExceptLvlOne[i].SetLockState();
            }
        }

        void LevelManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            SetLevelLockStates();
            SetComponents();
        }

        void LevelManager_OnLevelCompleted() {
            UnlockNextLevel();
        }

        IEnumerator LoadLevelCoroutine(int levelToLoad) {
            if (levelToLoad > _totalLevelCount)
                StopCoroutine(LoadLevelCoroutine(levelToLoad));

            _loadingScreen.alpha = 1;
            _loadingScreen.interactable = true;
            _loadingScreen.blocksRaycasts = true;

            AsyncOperation loadScene = SceneManager.LoadSceneAsync(levelToLoad);
            loadScene.allowSceneActivation = false;

            while (loadScene.isDone == false)
            {
                if (loadScene.progress >= 0.9f)
                {
                    loadScene.allowSceneActivation = true;
                }

                yield return null;
            }
        }

        IEnumerator HoldDownToRestartLevelCoroutine(float holdDownTime) {
            _isHoldingDown = true;
            _restartButtonTransform.DOLocalRotate(new Vector3(0, 0, 360), holdDownTime, RotateMode.LocalAxisAdd);
            _restartButtonTransform.DOScale(0.8f, 0.25f);

            while (_isHoldingDown)
            {
                _currentHoldDownTime += Time.deltaTime;

                _restartButtonFillImage.color = new Color(_restartButtonFillImage.color.r, _restartButtonFillImage.color.g, _restartButtonFillImage.color.b, _currentHoldDownTime);

                yield return null;

                if (_currentHoldDownTime >= holdDownTime)
                {
                    StopCoroutine(nameof(HoldDownToRestartLevelCoroutine));
                    _isHoldingDown = false;
                    _currentHoldDownTime = 0.0f;
                    RestartLevel();
                }
            }
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void RestartLevel() {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(LoadLevelCoroutine(currentLevel));
            AudioManager.Instance.SelectBGMAudioClipAndPlay(currentLevel);
            GameManager.Instance.SetIsPlaying(true);
        }

        public void GoToNextLevel() {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(LoadLevelCoroutine(currentLevel + 1));
            AudioManager.Instance.SelectBGMAudioClipAndPlay(currentLevel + 1);
            GameManager.Instance.SetIsPlaying(true);
        }

        public void GoToMainMenu() {
            StartCoroutine(LoadLevelCoroutine(0));
            TimerManager.Instance.StopTimer();
            GameManager.Instance.SetIsPlaying(false);
            AudioManager.Instance.SelectBGMAudioClipAndPlay(0);
        }

        public void StartHoldDownToRestartLevelCoroutine(float holdDownTime) {
            StartCoroutine(HoldDownToRestartLevelCoroutine(holdDownTime));
        }

        public void StopHoldDownToRestartLevelCoroutine(float holdDownTime) {
            StopCoroutine(HoldDownToRestartLevelCoroutine(holdDownTime));
            _isHoldingDown = false;
            _currentHoldDownTime = 0.0f;
            _restartButtonFillImage.color = new Color(_restartButtonFillImage.color.r, _restartButtonFillImage.color.g, _restartButtonFillImage.color.b, 0);
            _restartButtonTransform.DOLocalRotate(new Vector3(0, 0, 0), holdDownTime);
            _restartButtonTransform.DOScale(1f, 0.25f);
        }

        public void CallLoadLevelCoroutine(int levelToLoad) {
            StartCoroutine(LoadLevelCoroutine(levelToLoad));
        }

    }
}
