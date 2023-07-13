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
        [SerializeField] int goodTime;
        public int GoodTime { get { return goodTime; } }

        [SerializeField] int badTime;
        public int BadTime { get { return badTime; } }

    }
}
