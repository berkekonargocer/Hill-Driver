using Nojumpo.Scripts.Interfaces;
using Nojumpo.ScriptableObjects.Datas;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nojumpo.Systems.TooltipSystem.Panel
{
    public class TooltipPanelBase : MonoBehaviour, ITooltipPanel
    {
        [Header("Components")]
        [SerializeField] RectTransform _tooltipBackgroundRectTransform;
        RectTransform _tooltipCanvasRectTransform;
        RectTransform _tooltipPanelRectTransform;

        CanvasGroup _tooltipCanvasGroup;
        protected LayoutElement _tooltipLayoutElement;
        
        [Header("Tooltip Panel Position Settings")]
        [SerializeField] bool _fixedPosition;
        readonly Vector2 _rightUpperPivot = new Vector2(1.0f, 1.2f);
        readonly Vector2 _rightLowerPivot = new Vector2(1.0f, 0.0f);
        readonly Vector2 _leftUpperPivot = new Vector2(-0.050f, 1.2f);
        readonly Vector2 _leftLowerPivot = new Vector2(-0.025f, 0.0f);

        Vector2 _mousePosition;
        Vector2 _anchoredPosition;

        const int _maxPositionOffset = 120;

        [Header("Tooltip Settings")]
        bool _isHovering = false;
        [SerializeField] protected TextMeshProUGUI _tooltipText;
        [SerializeField] protected bool _autoSize = true;

        public TextMeshProUGUI TooltipText { get { return _tooltipText; } set { _tooltipText = value; } }


        void OnEnable() {
            SetComponents();
        }

        void Awake() {
            if (_fixedPosition) { enabled = false; }
        }

        void Update() {
            if (!_isHovering) return;

            ChangePivotPositionOnMousePosition();
            FollowCursor();
        }


        void FollowCursor() {
            _mousePosition = Input.mousePosition;
            _anchoredPosition = _mousePosition / _tooltipCanvasRectTransform.localScale.x;

            if (_anchoredPosition.x + _tooltipBackgroundRectTransform.rect.width - _maxPositionOffset > _tooltipCanvasRectTransform.rect.width)
            {
                _anchoredPosition.x = _tooltipCanvasRectTransform.rect.width - _tooltipBackgroundRectTransform.rect.width + _maxPositionOffset;
            }

            if (_anchoredPosition.y + _tooltipBackgroundRectTransform.rect.height > _tooltipCanvasRectTransform.rect.height)
            {
                _anchoredPosition.y = _tooltipCanvasRectTransform.rect.height - _tooltipBackgroundRectTransform.rect.height;
            }

            if (_anchoredPosition.x - _tooltipBackgroundRectTransform.rect.width + _maxPositionOffset < 0)
            {
                _anchoredPosition.x = _tooltipBackgroundRectTransform.rect.width - _maxPositionOffset;
            }

            if (_anchoredPosition.y - _tooltipBackgroundRectTransform.rect.height < 0)
            {
                _anchoredPosition.y = _tooltipBackgroundRectTransform.rect.height;
            }

            _tooltipPanelRectTransform.anchoredPosition = _anchoredPosition;
        }

        void ChangePivotPositionOnMousePosition() {
            if (_mousePosition.y > Screen.height / 2.0f && _mousePosition.x < Screen.width / 2.0f)
            {
                _tooltipBackgroundRectTransform.pivot = _leftUpperPivot;
            }

            if (_mousePosition.y > Screen.height / 2.0f && _mousePosition.x > Screen.width / 2.0f)
            {
                _tooltipBackgroundRectTransform.pivot = _rightUpperPivot;
            }

            if (_mousePosition.y < Screen.height / 2.0f && _mousePosition.x > Screen.width / 2.0f)
            {
                _tooltipBackgroundRectTransform.pivot = _rightLowerPivot;
            }

            if (_mousePosition.y < Screen.height / 2.0f && _mousePosition.x < Screen.width / 2.0f)
            {
                _tooltipBackgroundRectTransform.pivot = _leftLowerPivot;
            }
        }

        void SetComponents() {
            _tooltipCanvasGroup = GetComponent<CanvasGroup>();
            _tooltipLayoutElement = GetComponentInChildren<LayoutElement>();
            _tooltipPanelRectTransform = GetComponent<RectTransform>();
            _tooltipCanvasRectTransform = GameObject.Find("Tooltip Canvas").GetComponent<RectTransform>();
        }
        
        public virtual void CalculatePreferredWidth() {
            _tooltipLayoutElement.enabled = Mathf.Max(_tooltipText.preferredWidth) >= _tooltipLayoutElement.preferredWidth;
        }

        public virtual void DisplayTooltip(PointerEventData pointerEventData, Data data) {
            IsHovering(pointerEventData, data);
            UpdateTooltip(pointerEventData, data);
            ShowTooltip(pointerEventData, data);
        }
        
        public virtual void UpdateTooltip(PointerEventData pointerEventData, Data data) {
            if (_tooltipText != null)
            {
                _tooltipText.text = data.Description;
            }

            if (_autoSize)
            {
                CalculatePreferredWidth();
            }
        }

        public virtual void ShowTooltip(PointerEventData pointerEventData, Data data) {
            _tooltipCanvasGroup.alpha = 1;
        }

        public virtual void IsHovering(PointerEventData pointerEventData, Data data) {
            _isHovering = true;
        }

        public virtual void CloseTooltip() {
            IsNotHovering();
            HideTooltip();
        }
        
        public virtual void IsNotHovering() {
            _isHovering = false;
        }

        public virtual void HideTooltip() {
            _tooltipCanvasGroup.alpha = 0;
        }
    }
}
