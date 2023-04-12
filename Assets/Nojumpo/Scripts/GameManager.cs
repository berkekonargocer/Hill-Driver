using UnityEngine;

namespace Nojumpo.Managers
{
    public class GameManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            InitializeSingleton();
            PauseGame();
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
        public void PauseGame() {
            Time.timeScale = 0;
        }

        public void ResumeGame() {
            Time.timeScale = 1;
        }
    }
}