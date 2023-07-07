using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class NextLevelButton : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------



        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        

        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void LoadNextLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}