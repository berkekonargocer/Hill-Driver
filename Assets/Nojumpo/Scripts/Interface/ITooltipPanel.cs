using Nojumpo.ScriptableObjects.Datas;
using TMPro;
using UnityEngine.EventSystems;

namespace Nojumpo.Scripts.Interfaces
{
    public interface ITooltipPanel
    {
        public TextMeshProUGUI TooltipText { get; set;}
        public void IsHovering(PointerEventData pointerEventData, Data data);
        public void UpdateTooltip(PointerEventData pointerEventData, Data data);
        public void ShowTooltip(PointerEventData pointerEventData, Data data);
        public void DisplayTooltip(PointerEventData pointerEventData, Data data);
        public void IsNotHovering();
        public void CloseTooltip();
        public void HideTooltip();
    }
}
