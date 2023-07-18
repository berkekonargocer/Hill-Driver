using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo
{
    public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        enum AnimationType
        {
            MOVE_AND_SCALE,
            SCALE,
            SHRINK
        }
        
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] AnimationType animationType;
        [SerializeField] float targetScale;
        [SerializeField] float moveUpAmount;
        [SerializeField] float animationDuration;
        float initialScale;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------

        void Awake() {
            initialScale = gameObject.transform.localScale.x;
        }

        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void MoveAndScale(bool isBeginning) {
            if (isBeginning)
            {
                gameObject.transform.DOScale(targetScale, animationDuration);
                gameObject.transform.DOLocalMoveY(moveUpAmount, animationDuration);
                return;
            }
            
            gameObject.transform.DOScale(initialScale, animationDuration);
            gameObject.transform.DOLocalMoveY(0, animationDuration);
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------

        public void OnPointerEnter(PointerEventData eventData) {
            switch (animationType)
            {
                case AnimationType.MOVE_AND_SCALE:
                    MoveAndScale(true);
                    break;
                case AnimationType.SCALE:
                    break;
                case AnimationType.SHRINK:
                    break;
            }
        }
        public void OnPointerExit(PointerEventData eventData) {
            switch (animationType)
            {
                case AnimationType.MOVE_AND_SCALE:
                    MoveAndScale(false);
                    break;
                case AnimationType.SCALE:
                    break;
                case AnimationType.SHRINK:
                    break;
            }
        }
    }
}