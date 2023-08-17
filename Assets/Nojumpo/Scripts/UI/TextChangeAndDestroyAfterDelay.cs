using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Nojumpo
{
    public class TextChangeAndDestroyAfterDelay : MonoBehaviour
    {
        enum TextChangeAnimation
        {
            FADE,
            SCALE
        }

        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] float changeTextWaitSeconds;
        [SerializeField] float animationDuration;
        [SerializeField] TextChangeAnimation textChangeAnimation;
        [SerializeField] bool beginOnStart;
        [SerializeField] string[] texts;

        TextMeshProUGUI _textMeshProUGUI;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }

        void Start() {
            if (beginOnStart)
            {
                StartCoroutine(nameof(ChangeText));
            }
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        IEnumerator ChangeText() {
            switch (textChangeAnimation)
            {
                case TextChangeAnimation.SCALE:
                    transform.localScale = Vector3.zero;
                    break;
                case TextChangeAnimation.FADE:
                    _textMeshProUGUI.DOFade(0, 0);
                    break;
            }

            for (int i = 0; i < texts.Length; i++)
            {
                switch (textChangeAnimation)
                {
                    case TextChangeAnimation.SCALE:
                        transform.DOScale(0, animationDuration / 2).onComplete = () => _textMeshProUGUI.text = texts[i];
                        yield return new WaitForSeconds(animationDuration / 2);
                        transform.DOScale(1, animationDuration / 2);
                        break;
                    case TextChangeAnimation.FADE:
                        _textMeshProUGUI.DOFade(0, animationDuration / 2).onComplete = () => _textMeshProUGUI.text = texts[i];
                        yield return new WaitForSeconds(animationDuration / 2);
                        _textMeshProUGUI.DOFade(1, animationDuration / 2);
                        break;
                }

                yield return new WaitForSeconds(changeTextWaitSeconds);
            }

            switch (textChangeAnimation)
            {
                case TextChangeAnimation.SCALE:
                    transform.DOScale(0, animationDuration / 2).onComplete = () => Destroy(gameObject);
                    break;
                case TextChangeAnimation.FADE:
                    _textMeshProUGUI.DOFade(0, animationDuration / 2).onComplete = () => Destroy(gameObject);
                    break;
            }
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void StartEffect() {
            StartCoroutine(nameof(ChangeText));
        }
    }
}
