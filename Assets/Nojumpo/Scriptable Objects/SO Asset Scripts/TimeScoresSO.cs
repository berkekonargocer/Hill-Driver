using Nojumpo.Scripts.Managers;
using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewTimeScoresSo", menuName = "Nojumpo/Scriptable Objects/Datas/Timer Manager/New Time Scores SO")]
    public class TimeScoresSO : ScriptableObject
    {

#if UNITY_EDITOR

        [TextArea]
        [SerializeField] string _developerDescription;

#endif
        [SerializeField] int levelCount;

        [SerializeField] int goodTime;
        public int GoodTime { get { return goodTime; } }

        [SerializeField] int badTime;
        public int BadTime { get { return badTime; } }
        
        public string LevelPlayerPrefsKey { get; private set; }
        public float PersonalBest { get; private set; }


        void OnEnable() {
            LevelPlayerPrefsKey = $"Level {levelCount.ToString()} Personal Best";
            PersonalBest = PlayerPrefs.GetFloat(LevelPlayerPrefsKey);
            
            if (PlayerPrefs.GetFloat(LevelPlayerPrefsKey) <= 0)
            {
                PlayerPrefs.SetFloat(LevelPlayerPrefsKey, 900);
            }
        }

        public void SetPersonalBest() {
            if (IsPersonalBest())
            {
                PlayerPrefs.SetFloat(LevelPlayerPrefsKey, TimerManager.Instance.CurrentTime);
                PersonalBest = PlayerPrefs.GetFloat(LevelPlayerPrefsKey);
            }
        }

        public bool IsPersonalBest() {
            return TimerManager.Instance.CurrentTime < PlayerPrefs.GetFloat(LevelPlayerPrefsKey);
        }

    }
}
