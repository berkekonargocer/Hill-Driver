using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using Nojumpo.Scripts.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class LevelDetails : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] LevelDetailsSO levelDetailsSo;
        [SerializeField] bool activateTimerOnSceneLoad;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            LevelManager.Instance.SetLevelDetailsSO(levelDetailsSo);
            TimerManager.Instance.SetLevelDetailsSO(levelDetailsSo);
            TimerManager.Instance.SetTimerActive(activateTimerOnSceneLoad);
        }
        

    }
}
