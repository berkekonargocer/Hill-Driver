using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class FinishFlag : MonoBehaviour
    {
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.InvokeOnLevelCompleted();
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}