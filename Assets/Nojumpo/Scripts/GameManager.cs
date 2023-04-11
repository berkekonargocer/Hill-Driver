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
    }
}