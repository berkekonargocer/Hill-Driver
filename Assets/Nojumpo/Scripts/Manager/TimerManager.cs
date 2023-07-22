using System.Collections.Generic;
using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Scripts.Managers
{
    public class TimerManager : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [Header("SINGLETON")]
        static TimerManager _instance;
        public static TimerManager Instance { get { return _instance; } }

        [Header("COMPONENTS")]
        [SerializeField] TextMeshProUGUI _timerText;

        [Header("TIMER SETTINGS")]
        [SerializeField] bool _isTimerActive;
        [SerializeField] bool _isCountdown;
        [SerializeField] float _startingTime;

        float _currentTime; // Make this a float variable if you want to use this data to do something 
        public float CurrentTime { get { return _currentTime; } }
        public LevelDetailsSO LevelDetailsSo { get; private set; }

        [Header("LIMIT SETTINGS")]
        [SerializeField] bool _hasLimit;
        [SerializeField] float _timerLimit;

        [Header("TIME FORMAT SETTINGS")]
        [SerializeField] bool _minutesAndSeconds;
        [SerializeField] TimerFormats _timerFormat;

        readonly Dictionary<TimerFormats, string> _timeFormatsDictionary = new Dictionary<TimerFormats, string>();

        enum TimerFormats { Whole, TenthDecimal }

        [Header("Timer Visualization Settings")]
        [Tooltip("GREEN = When you have more time than half of the starting time   " +
            "YELLOW = When you have same or less time than half of the starting time   " +
            "RED = When you have same or less time than LAST TIMES that you set")]
        [SerializeField] bool _changeTimerColor;
        [SerializeField] int _lastTimes = 10;
        [SerializeField] int _timerTextFontSize;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            SceneManager.sceneLoaded += TimerManager_OnSceneLoaded;
            GameManager.onGamePaused += TimerManager_OnGamePaused;
            GameManager.onGameResumed += TimerManager_OnGameResumed;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= TimerManager_OnSceneLoaded;
            GameManager.onGamePaused -= TimerManager_OnGamePaused;
            GameManager.onGameResumed -= TimerManager_OnGameResumed;
        }

        void Awake() {
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

        void Start() {
            SetDictionaryValues();

            if (_timerText != null)
            {
                SetTimerFontSize();
            }
        }

        void Update() {
            if (!_isTimerActive)
                return;

            TimerCountdownOrUp();

            if (_changeTimerColor)
            {
                TimerTextColorChanger();
            }

            if (_hasLimit)
            {
                TimerLimit();
            }
        }

        
        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _timerText = GameObject.FindWithTag("UI/Timer Text")?.GetComponent<TextMeshProUGUI>();
            LevelDetailsSo = GameObject.FindWithTag("Level Details")?.GetComponent<LevelDetails>().LevelDetailsSo;
        }

        void TimerCountdownOrUp() {
            _currentTime = _isCountdown ? _currentTime -= Time.deltaTime : _currentTime += Time.deltaTime;

            if (_isCountdown && _currentTime <= 0)
            {
                _currentTime = 0;
                _isTimerActive = false;
            }

            SetTimerText();
        }

        void TimerLimit() {
            if ((_isCountdown && _currentTime <= _timerLimit) || (!_isCountdown && _currentTime >= _timerLimit))
            {
                _isTimerActive = false;
                _currentTime = _timerLimit;
                SetTimerText();
            }
        }

        void SetTimerText() {
            _timerText.text = GetCurrentTimeText(_minutesAndSeconds);
        }


        void TimerTextColorChanger() {
            if (_isCountdown)
            {
                if (_currentTime > _startingTime / 2)
                {
                    _timerText.color = Color.green;
                }
                else if (_currentTime <= _startingTime / 2 && _currentTime > _lastTimes)
                {
                    _timerText.color = Color.yellow;
                }
                else if (_currentTime <= _lastTimes)
                {
                    _timerText.color = Color.red;
                }
            }
            else
            {
                if (_currentTime <= LevelDetailsSo.GoodTime)
                {
                    _timerText.color = Color.green;
                }
                else if (_currentTime >= LevelDetailsSo.BadTime)
                {
                    _timerText.color = Color.red;
                }
                else
                {
                    _timerText.color = Color.yellow;
                }
            }
        }

        void ResetTimer() {
            _currentTime = _startingTime;
        }

        void SetTimerFontSize() {
            _timerText.fontSize = _timerTextFontSize;
        }

        void SetDictionaryValues() {
            _timeFormatsDictionary.Add(TimerFormats.Whole, "0");
            _timeFormatsDictionary.Add(TimerFormats.TenthDecimal, "0.0");
        }

        void TimerManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            SetComponents();
            ResetTimer();
        }

        void TimerManager_OnGamePaused(int numberToDivide) {
            StopTimer(numberToDivide);
        }
        
        void TimerManager_OnGameResumed(int numberToMultiply) {
            StartTimer(numberToMultiply);
        }
        
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public string GetCurrentTimeText(bool minutesAndSeconds) {
            if (minutesAndSeconds)
            {
                float minutes = Mathf.FloorToInt(_currentTime / 60);
                return $"{minutes.ToString()}:{_currentTime % 60:00}";
            }

            return _currentTime.ToString(_timeFormatsDictionary[_timerFormat]);
        }

        public void SetCurrentTime(bool startingTime, float timeToSet = 0) {
            _currentTime = startingTime ? _startingTime : timeToSet;
        }
        
        public void SetTimerActive(bool setActive) {
            _isTimerActive = setActive;
        }

        public void StopTimer() {
            _isTimerActive = false;
        }
        
        public void StartTimer(int numberToMultiply) {
            _isTimerActive = true;
        }

        public void StopTimer(int numberToDivide) {
            _isTimerActive = false;
        }
    }
}