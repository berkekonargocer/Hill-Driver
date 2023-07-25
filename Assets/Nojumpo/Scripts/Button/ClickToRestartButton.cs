using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class ClickToRestartButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            LevelManager.Instance.RestartLevel();
        }
    }
}