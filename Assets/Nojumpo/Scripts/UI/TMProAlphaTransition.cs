using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Nojumpo
{
    public class TMProAlphaTransition : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] float endValue;
        [SerializeField] float animationDuration;
        [SerializeField] bool repeat;

        float _initialValue;
        TextMeshProUGUI _textMeshProUGUI;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }

        void Start() {
            ApplyEffect();
        }

        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _initialValue = _textMeshProUGUI.alpha;
        }

        void ApplyEffect() {
            if (repeat)
            {
                Sequence alphaTransitionEffectSequence = DOTween.Sequence();
                
                Tween alphaTransitionTween1 = _textMeshProUGUI.DOFade(endValue, animationDuration / 2);
                Tween alphaTransitionTween2 = _textMeshProUGUI.DOFade(_initialValue, animationDuration / 2);

                alphaTransitionEffectSequence.Append(alphaTransitionTween1);
                alphaTransitionEffectSequence.Append(alphaTransitionTween2);

                alphaTransitionEffectSequence.SetLoops(-1);

                alphaTransitionEffectSequence.Play();
            }
            else
            {
                _textMeshProUGUI.DOFade(endValue, animationDuration);
            }
        }
    }
}
