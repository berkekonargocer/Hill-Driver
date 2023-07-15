using UnityEngine;

namespace Nojumpo.ScriptableObjects.Datas
{
    [CreateAssetMenu(fileName = "NewData", menuName = "Nojumpo/Scriptable Objects/Datas/Description/New Description")]
    public class Data : ScriptableObject
    {
#if UNITY_EDITOR

        [TextArea] [SerializeField] string _developerDescription;

#endif

        [SerializeField] string _name;
        public string Name { get { return _name; } }

        [TextArea] [SerializeField] string _description;
        public string Description { get { return _description; } }
    }
}
