using System.Threading.Tasks;
using Nojumpo.ScriptableObjects.Datas;
using Nojumpo.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo.Systems.TooltipSystem
{
    [ExecuteInEditMode]
    public class TooltipPopupOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Data _data;
        [SerializeField] CanvasGroup _canvasGroup;

        [SerializeField] bool _showDelayed = true;
        [SerializeField] int _delayAmount = 5;
        
        [SerializeField] TooltipPanelAnimationSettings tooltipPanelAnimationSettings;


        async Task DisplayTooltip(PointerEventData eventData) {
            if (_showDelayed)
            {
                int delayAmountInSeconds = _delayAmount * 100;
                await Task.Delay(delayAmountInSeconds);
            }

            _canvasGroup.GetComponent<ITooltipPanel>().DisplayTooltip(eventData, _data, tooltipPanelAnimationSettings);
        }
        
        void CloseTooltip() {
            _canvasGroup.GetComponent<ITooltipPanel>().CloseTooltip(tooltipPanelAnimationSettings);
        }
        
        public async void OnPointerEnter(PointerEventData eventData) {
            await DisplayTooltip(eventData);
        }

        public void OnPointerExit(PointerEventData eventData) {
            CloseTooltip();
        }
    }
}
