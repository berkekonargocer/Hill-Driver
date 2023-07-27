using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class NextLevelButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            LevelManager.Instance.GoToNextLevel();
        }
    }
}