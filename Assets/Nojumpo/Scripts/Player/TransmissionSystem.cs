using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class TransmissionSystem : MonoBehaviour, IPointerDownHandler
    {
        // -------------------------------- FIELDS ---------------------------------
        public static bool DRIVE_FORWARD { get; private set; } = true;

        [SerializeField] AudioClip transmissionStateChangeSFX;
        AudioSource _transmissionSystemAudioSource;
        Animator _transmissionSystemAnimator;
        static readonly int DRIVE_FORWARD_ANIM_PARAM = Animator.StringToHash("driveForward");

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            SceneManager.sceneLoaded += TransmissionSystem_OnSceneLoaded;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= TransmissionSystem_OnSceneLoaded;
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _transmissionSystemAudioSource = GetComponent<AudioSource>();
            _transmissionSystemAnimator = GetComponent<Animator>();
            _transmissionSystemAudioSource.clip = transmissionStateChangeSFX;
            DRIVE_FORWARD = true;
        }

        void ShiftGear() {
            _transmissionSystemAudioSource.Play();
            DRIVE_FORWARD = !DRIVE_FORWARD;
            _transmissionSystemAnimator.SetBool(DRIVE_FORWARD_ANIM_PARAM, DRIVE_FORWARD);
        }

        void TransmissionSystem_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            SetComponents();
        }
        
        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnPointerDown(PointerEventData eventData) {
            ShiftGear();
        }
    }
}