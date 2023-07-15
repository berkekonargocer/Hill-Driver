using Nojumpo.ScriptableObjects;
using Nojumpo.Scripts.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class TimeScores : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] TimeScoresSO timeScoresSO;
        public TimeScoresSO TimeScoresSO { get { return timeScoresSO; } }
        
        [SerializeField] bool activateTimerOnSceneLoad;

        string LevelPBPlayerPrefsKey { get; set; }


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            TimerManager.Instance.SetTimeScores(this);
            TimerManager.Instance.SetTimerActive(activateTimerOnSceneLoad);

            LevelPBPlayerPrefsKey = $"Level {timeScoresSO.LevelCount.ToString()} Personal Best";
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public bool IsPersonalBest() {
            return (int)TimerManager.Instance.CurrentTime < (int)PlayerPrefs.GetFloat(LevelPBPlayerPrefsKey) || PlayerPrefs.GetFloat(LevelPBPlayerPrefsKey) <= 0;
        }

        public void SetPersonalBest() {
            PlayerPrefs.SetFloat(LevelPBPlayerPrefsKey, TimerManager.Instance.CurrentTime);
        }
    }
}
