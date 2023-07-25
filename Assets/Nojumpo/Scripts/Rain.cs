using System;
using Nojumpo.ScriptableObjects;
using UnityEngine;

namespace Nojumpo
{
    public class Rain : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        ParticleSystem _rainParticleSystem;


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

        void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player"))
            {
                StopRain();
            }
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _rainParticleSystem = GetComponent<ParticleSystem>();
        }

        void StartRain() {
            _rainParticleSystem.Play();
        }

        void StopRain() {
            _rainParticleSystem.Stop();
        }
    }
}
