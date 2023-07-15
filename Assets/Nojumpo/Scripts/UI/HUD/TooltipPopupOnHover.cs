using Nojumpo.ScriptableObjects.Datas;
using Nojumpo.Scripts.Interfaces;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo.Systems.TooltipSystem
{
    [ExecuteInEditMode]
    public class TooltipPopupOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Tooltip Settings")]
        bool _isCurrentlyHovering;

        [SerializeField] Data _data;
        [SerializeField] CanvasGroup _canvasGroup;

        [SerializeField] bool _showDelayed = true;
        [SerializeField] int _delayAmount = 5;


        public async void OnPointerEnter(PointerEventData eventData) {
            _isCurrentlyHovering = true;

            if (_showDelayed)
            {
                int delayAmountInSeconds = _delayAmount * 100;
                await Task.Delay(delayAmountInSeconds);
            }

            _canvasGroup.GetComponent<ITooltipPanel>().DisplayTooltip(eventData, _data);
        }

        public void OnPointerExit(PointerEventData eventData) {
            _canvasGroup.GetComponent<ITooltipPanel>().CloseTooltip();
            _isCurrentlyHovering = false;
        }
    }
}
