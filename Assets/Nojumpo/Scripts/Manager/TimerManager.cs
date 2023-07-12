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
        static TimerManager _instance;
        public static TimerManager Instance { get { return _instance; } }

        [Header("Components")]
        [SerializeField] TextMeshProUGUI _timerText;

        [Header("Timer Settings")]
        [SerializeField] bool _isTimerActive;
        [SerializeField] bool _isCountdown;
        [SerializeField] float _startingTime;

        float _currentTime; // Make this a float variable if you want to use this data to do something 
        public float CurrentTime { get { return _currentTime; } }
        public TimeScoresSO TimeScores { get; private set; }

        [Header("Limit  Settings")]
        [SerializeField] bool _hasLimit;
        [SerializeField] float _timerLimit;

        [Header("Time Format Settings")]
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


        void OnEnable() {
            SceneManager.sceneLoaded += SetComponents;
            SceneManager.sceneLoaded += ResetTimer;
            GameManager.onLevelCompleted += StopTimer;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= SetComponents;
            SceneManager.sceneLoaded -= ResetTimer;
            GameManager.onLevelCompleted -= StopTimer;
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

        void SetComponents(Scene scene, LoadSceneMode loadSceneMode) {
            _timerText = GameObject.FindWithTag("UI/Timer Text")?.GetComponent<TextMeshProUGUI>();
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
                if (_currentTime <= TimeScores.GoodTime)
                {
                    _timerText.color = Color.green;
                }
                else if (_currentTime >= TimeScores.BadTime)
                {
                    _timerText.color = Color.red;
                }
                else
                {
                    _timerText.color = Color.yellow;
                }
            }
        }

        void ResetTimer(Scene scene, LoadSceneMode loadSceneMode) {
            _currentTime = _startingTime;
        }


        void SetTimerFontSize() {
            _timerText.fontSize = _timerTextFontSize;
        }

        void SetDictionaryValues() {
            _timeFormatsDictionary.Add(TimerFormats.Whole, "0");
            _timeFormatsDictionary.Add(TimerFormats.TenthDecimal, "0.0");
        }

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

        public void SetTimeScores(TimeScoresSO timeScoresSO) {
            TimeScores = timeScoresSO;
        }

    }
}