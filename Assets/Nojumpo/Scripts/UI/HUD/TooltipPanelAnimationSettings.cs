using System;
using UnityEngine;

namespace Nojumpo
{
    public enum TooltipPanelAnimations
    {
        NONE,
        SCALE,
        FADE
    }
    
    [Serializable]
    public class TooltipPanelAnimationSettings
    {
        // -------------------------------- FIELDS --------------------------------
        [SerializeField] float animationDuration = 0.2f;
        public float AnimationDuration { get { return animationDuration; } }

        [SerializeField] TooltipPanelAnimations tooltipPanelAnimation;
        public TooltipPanelAnimations TooltipPanelAnimation { get { return tooltipPanelAnimation; } }

    }
}