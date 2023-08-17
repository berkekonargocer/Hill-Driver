using System;
using Nojumpo.ScriptableObjects;
using UnityEngine;

namespace Nojumpo
{
    public class Rain : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        ParticleSystem _rainParticleSystem;
        ParticleSystem.EmissionModule _rainParticleEmission;

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player"))
            {
                StartRain();
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player"))
            {
                StopRain();
            }
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _rainParticleSystem = GetComponent<ParticleSystem>();
            _rainParticleEmission = _rainParticleSystem.emission;
        }

        void StartRain() {
            _rainParticleSystem.Play();
            _rainParticleEmission.enabled = true;
        }

        void StopRain() {
            _rainParticleEmission.enabled = false;
            _rainParticleSystem.Stop();
        }
    }
}
