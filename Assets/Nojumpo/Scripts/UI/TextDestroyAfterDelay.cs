using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;

namespace Nojumpo
{
    public class TextDestroyAfterDelay : MonoBehaviour
    {
        enum DestroyAnimation
        {
            FADE,
            SCALE_DOWN
        }
        
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] float delaySecondToFade;
        [SerializeField] float animationDuration;
        [SerializeField] bool beginCountdownOnStart;
        [SerializeField] DestroyAnimation destroyAnimation;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Start() {
            if (beginCountdownOnStart)
            {
                StartCoroutine(nameof(Destroy));
            }
        }

        
        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        IEnumerator Destroy() {
            yield return new WaitForSeconds(delaySecondToFade);

            switch (destroyAnimation)
            {
                case DestroyAnimation.FADE:
                    TextMeshProUGUI textMeshProUGUI = GetComponent<TextMeshProUGUI>();
                    textMeshProUGUI.DOFade(0, animationDuration).onComplete = () => Destroy(gameObject);
                    break;
                case DestroyAnimation.SCALE_DOWN:
                    transform.DOScale(0, animationDuration).onComplete = () => Destroy(gameObject);
                    break;
            }

        }
        
        
        // ------------------------- CUSTOM PUBLIC METHODS ------------------------
        public void StartFadeAndDestroyCoroutine() {
            StartCoroutine(nameof(Destroy));
        }
    }
}
