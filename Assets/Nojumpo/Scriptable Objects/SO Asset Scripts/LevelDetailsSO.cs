using Nojumpo.ScriptableObjects.Datas;
using Nojumpo.Scripts.Managers;
using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevelDetailsSO", menuName = "Nojumpo/Scriptable Objects/Data/Level/New Level Details SO")]
    public class LevelDetailsSO : Data
    {
        
        [SerializeField] int levelNumber;
        public int LevelNumber { get { return levelNumber; } }

        [SerializeField] bool isLocked;
        public bool IsLocked { get { return isLocked; } }

        [SerializeField] int goodTime;
        public int GoodTime { get { return goodTime; } }

        [SerializeField] int badTime;
        public int BadTime { get { return badTime; } }
        

        public string LevelPBPlayerPrefsKey() {
            return $"Level {levelNumber.ToString()} Personal Best";
        }

        public string CurrentLevelLockStatePlayerPrefsKey() {
            return $"Level {levelNumber.ToString()} Lock State";
        }

        public string NextLevelLockStatePlayerPrefsKey() {
            return $"Level {levelNumber + 1} Lock State";
        }

        public bool IsPersonalBest() {
            return (int)TimerManager.Instance.CurrentTime < PlayerPrefs.GetInt(LevelPBPlayerPrefsKey()) || 
                PlayerPrefs.GetInt(LevelPBPlayerPrefsKey()) <= 0;
        }
        
        public void SetLockState() {
            isLocked = PlayerPrefs.GetInt(CurrentLevelLockStatePlayerPrefsKey()) == 0 ? true : false;
        }

        public void SetPersonalBest() {
            PlayerPrefs.SetInt(LevelPBPlayerPrefsKey(), (int)TimerManager.Instance.CurrentTime);
        }
    }
}
