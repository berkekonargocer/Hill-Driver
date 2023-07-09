using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevelSO", menuName = "Nojumpo/Scriptable Objects/Datas/Level/New Level Data Scriptable Object")]
    public class LevelDataSO : ScriptableObject
    {
        
#if UNITY_EDITOR

        [TextArea]
        [SerializeField] string _developerDescription;

#endif
        [SerializeField] int levelBuildIndex;
        public int LevelBuildIndex { get { return levelBuildIndex; } }

        [SerializeField] int levelCount;
        public int LevelCount { get { return levelCount; } }

        [SerializeField] int oneStarTime;
        public int OneStarTime { get { return oneStarTime; } }
        
        [SerializeField] int twoStarTime;
        public int TwoStarTime { get { return twoStarTime; } }
        
        [SerializeField] int threeStarTime;
        public int ThreeStarTime { get { return threeStarTime; } }
        
        [SerializeField] int bestTime;
        public int BestTime { get { return bestTime; } set { bestTime = value; } }
    }
}