using UnityEngine;

namespace Nojumpo.Managers
{
    public class AudioManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }

        [Header("AUDIO VARIABLES")]
        [SerializeField] AudioSource bgmAudioSource;
        [SerializeField] AudioSource levelCompletedAudioSource;
        [SerializeField] AudioSource gamePauseAudioSource;
        [SerializeField] AudioClip[] bgmAudios;
        [SerializeField] AudioClip[] pauseSFXClips;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onLevelCompleted += AudioManager_OnLevelCompleted;
            GameManager.onGamePaused += AudioManager_OnGamePaused;
            GameManager.onGameResumed += AudioManager_OnGameResumed;
        }

        void OnDisable() {
            GameManager.onLevelCompleted -= AudioManager_OnLevelCompleted;
            GameManager.onGamePaused -= AudioManager_OnGamePaused;
            GameManager.onGameResumed -= AudioManager_OnGameResumed;
        }

        void Awake() {
            InitializeSingleton();
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

        void AudioManager_OnLevelCompleted() {
            DecreaseBGMVolumeTo25Percent();
            levelCompletedAudioSource.Play();
        }

        void AudioManager_OnGamePaused(int numberToDivide) {
            DecreaseBGMVolumeByDivision(numberToDivide);
        }

        void AudioManager_OnGameResumed(int numberToMultiply) {
            IncreaseBGMVolumeByMultiplication(numberToMultiply);
        }
        
        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void StartBGM() {
            bgmAudioSource.Play();
        }

        public void StopBGM() {
            bgmAudioSource.Stop();
        }

        public void RestartBGM() {
            bgmAudioSource.Stop();
            bgmAudioSource.Play();
        }

        public void DecreaseBGMVolumeTo25Percent() {
            bgmAudioSource.volume /= 4;
        }
        
        public void DecreaseBGMVolumeByDivision(int numberToDivide) {
            bgmAudioSource.volume /= numberToDivide;
        }

        public void IncreaseBGMVolumeByMultiplication(int numberToMultiply) {
            bgmAudioSource.volume *= numberToMultiply;
        }

        public void PlayGamePauseAudioSource(bool isPaused) {
            if (isPaused)
            {
                gamePauseAudioSource.clip = pauseSFXClips[1];
                gamePauseAudioSource.Play();
            }
            else
            {
                gamePauseAudioSource.clip = pauseSFXClips[0];
                gamePauseAudioSource.Play();
            }
        }
        
        public void SelectBGMAudioClipAndPlay(int clipNo) {
            bgmAudioSource.clip = bgmAudios[clipNo];
            bgmAudioSource.volume = clipNo == 2 ? 0.4f : 0.2f;
            bgmAudioSource.Play();
        }

    }
}