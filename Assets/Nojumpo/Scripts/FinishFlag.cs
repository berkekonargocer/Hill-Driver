using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class FinishFlag : MonoBehaviour
    {
        ParticleSystem levelCompleteParticleEffect;
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            levelCompleteParticleEffect = GetComponentInChildren<ParticleSystem>();
        }
        
        void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player"))
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GameManager.Instance.InvokeOnLevelCompleted();
                levelCompleteParticleEffect.Play();
            }
        }
    }
}