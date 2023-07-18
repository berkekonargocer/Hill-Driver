using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class PauseStateChangeButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            GameManager.Instance.PauseOrUnpauseGame();
        }
    }
}