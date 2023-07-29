using UnityEngine;
using UnityEngine.EventSystems;

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
        void Awake() {
            SetComponents();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _transmissionSystemAudioSource = GetComponent<AudioSource>();
            _transmissionSystemAnimator = GetComponent<Animator>();
            _transmissionSystemAudioSource.clip = transmissionStateChangeSFX;
        }

        void ShiftGear() {
            _transmissionSystemAudioSource.Play();
            DRIVE_FORWARD = !DRIVE_FORWARD;
            _transmissionSystemAnimator.SetBool(DRIVE_FORWARD_ANIM_PARAM, DRIVE_FORWARD);
        }
        
        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnPointerDown(PointerEventData eventData) {
            ShiftGear();
        }
    }
}