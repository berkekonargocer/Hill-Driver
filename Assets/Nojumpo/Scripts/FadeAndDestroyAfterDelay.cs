using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Nojumpo
{
    public class FadeAndDestroyAfterDelay : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] float delaySecondToFade;
        [SerializeField] float fadeDuration;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------

        void Start() {
            StartCoroutine(nameof(FadeAndDestroy));
        }

        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        IEnumerator FadeAndDestroy() {

            yield return new WaitForSeconds(delaySecondToFade);

            Material material = gameObject.GetComponent<Material>();
            material.DOFade(0, fadeDuration).onComplete = () => Destroy(gameObject);
        }
    }
}
