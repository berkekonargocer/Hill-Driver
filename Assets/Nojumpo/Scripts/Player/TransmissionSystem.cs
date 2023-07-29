using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo
{
    public class TransmissionSystem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // -------------------------------- FIELDS ---------------------------------
        public static bool DriveForward { get; private set; } = true;

        [SerializeField] AudioClip transmissionStateChangeSFX;
        AudioSource transmissionSystemAudioSource;
        Animator transmissionSystemAnimator;
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {

        }

        void OnDisable() {

        }

        void Awake() {

        }

        void Start() {

        }

        void Update() {

        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            transmissionSystemAudioSource = GetComponent<AudioSource>();
            transmissionSystemAnimator = GetComponent<Animator>();
            transmissionSystemAudioSource.clip = transmissionStateChangeSFX;
        }

        void ChangeTransmissionState() {
            transmissionSystemAudioSource.Play();
            DriveForward = !DriveForward;
        }
        
        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnPointerDown(PointerEventData eventData) {
            ChangeTransmissionState();
        }
        
        public void OnPointerUp(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }
    }
}