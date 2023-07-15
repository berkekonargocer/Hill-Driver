using Nojumpo.ScriptableObjects.Datas;
using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewTimeScoresSo", menuName = "Nojumpo/Scriptable Objects/Datas/Timer Manager/New Time Scores SO")]
    public class TimeScoresSO : Data
    {

        [SerializeField] int levelCount;
        public int LevelCount { get { return levelCount; } }
        
        [SerializeField] int goodTime;
        public int GoodTime { get { return goodTime; } }

        [SerializeField] int badTime;
        public int BadTime { get { return badTime; } }

    }
}
