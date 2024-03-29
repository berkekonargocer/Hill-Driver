using Nojumpo.ScriptableObjects;
using Nojumpo.Scripts.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class LevelDetails : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] LevelDetailsSO levelDetailsSo;
        public LevelDetailsSO LevelDetailsSo { get { return levelDetailsSo; } }
        
        [SerializeField] bool activateTimerOnSceneLoad;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Start() {
            TimerManager.Instance.SetTimerActive(activateTimerOnSceneLoad);
        }
    }
}
