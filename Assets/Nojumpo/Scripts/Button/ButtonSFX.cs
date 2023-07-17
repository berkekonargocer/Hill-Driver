using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        // -------------------------------- FIELDS ---------------------------------
        AudioSource _buttonAudioSource;

        [SerializeField] AudioClip hoverSFX;
        [SerializeField] AudioClip pointerDownSFX;
        [SerializeField] AudioClip pointerUpSFX;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _buttonAudioSource = GetComponent<AudioSource>();
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnPointerEnter(PointerEventData eventData) {
            _buttonAudioSource.PlayOneShot(hoverSFX);
        }
        public void OnPointerDown(PointerEventData eventData) {
            _buttonAudioSource.PlayOneShot(pointerDownSFX);
        }
        public void OnPointerUp(PointerEventData eventData) {
            _buttonAudioSource.PlayOneShot(pointerUpSFX);
        }
    }
}