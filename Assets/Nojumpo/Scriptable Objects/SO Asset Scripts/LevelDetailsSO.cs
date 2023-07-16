using System;
using Nojumpo.ScriptableObjects.Datas;
using Nojumpo.Scripts.Managers;
using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevelDetailsSO", menuName = "Nojumpo/Scriptable Objects/Datas/Level/New Level Details SO")]
    public class LevelDetailsSO : Data
    {

        [SerializeField] int levelCount;
        public int LevelCount { get { return levelCount; } }

        [SerializeField] bool isLocked;
        public bool IsLocked { get { return isLocked; } }

        [SerializeField] int goodTime;
        public int GoodTime { get { return goodTime; } }

        [SerializeField] int badTime;
        public int BadTime { get { return badTime; } }
        

        public string LevelPBPlayerPrefsKey() {
            return $"Level {levelCount.ToString()} Personal Best";
        }

        public string CurrentLevelLockStatePlayerPrefsKey() {
            return $"Level {levelCount.ToString()} Lock State";
        }

        public string NextLevelLockStatePlayerPrefsKey() {
            return $"Level {levelCount + 1} Lock State";
        }

        public bool IsPersonalBest() {
            return (int)TimerManager.Instance.CurrentTime < (int)PlayerPrefs.GetFloat(LevelPBPlayerPrefsKey()) || 
                PlayerPrefs.GetFloat(LevelPBPlayerPrefsKey()) <= 0;
        }
        
        public void SetLockState() {
            isLocked = PlayerPrefs.GetInt(CurrentLevelLockStatePlayerPrefsKey()) == 1 ? true : false;
        }

        public void SetPersonalBest() {
            PlayerPrefs.SetFloat(LevelPBPlayerPrefsKey(), TimerManager.Instance.CurrentTime);
        }
    }
}
