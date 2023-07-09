using UnityEngine;

namespace Nojumpo.Managers
{
    public class UIManager : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("SINGLETON")]
        static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
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
    }
}
