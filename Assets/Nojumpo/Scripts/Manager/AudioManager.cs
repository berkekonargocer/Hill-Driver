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
        [SerializeField] AudioClip[] bgmAudios;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onLevelCompleted += levelCompletedAudioSource.Play;
        }

        void OnDisable() {
            GameManager.onLevelCompleted -= levelCompletedAudioSource.Play;
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
        
        public void SelectBGMAudioClipAndPlay(int clipNo) {
            bgmAudioSource.clip = bgmAudios[clipNo];
            bgmAudioSource.volume = clipNo == 1 ? 0.4f : 0.2f;
            bgmAudioSource.Play();
        }

    }
}