using Nojumpo.ScriptableObjects;
using Nojumpo.Scripts.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class TimeScores : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] TimeScoresSO timeScoresSO;
        [SerializeField] bool activateTimerOnSceneLoad;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            TimerManager.Instance.SetTimeScores(timeScoresSO);
            TimerManager.Instance.SetTimerActive(activateTimerOnSceneLoad);
        }
    }
}