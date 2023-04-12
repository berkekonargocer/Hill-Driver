using UnityEngine;

namespace Nojumpo.Managers
{
    public class AudioManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        private static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }

        [Header("AUDIO VARIABLES")]
        [SerializeField] private AudioSource _bgmAudioSource;

        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            InitializeSingleton();
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

        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void StartBGM() {
            _bgmAudioSource.Play();
        }
    }
}