using Nojumpo.ScriptableObjects;
using Nojumpo.ScriptableObjects.Datas;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nojumpo.Systems.TooltipSystem.Panel
{
    public class LevelDetailsTooltipPanel : TooltipPanelBase
    {
        TimeScoresSO _timeScoresSO;

        [Header("UI Texts")]
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] TextMeshProUGUI oneStarText;
        [SerializeField] TextMeshProUGUI twoStarText;
        [SerializeField] TextMeshProUGUI threeStarText;
        [SerializeField] TextMeshProUGUI personalBestText;

        public override void CalculatePreferredWidth() {
            _tooltipLayoutElement.enabled = Mathf.Max(levelText.preferredWidth, oneStarText.preferredWidth, twoStarText.preferredWidth,
                threeStarText.preferredWidth, personalBestText.preferredWidth) >= _tooltipLayoutElement.preferredWidth;
        }

        public override void UpdateTooltip(PointerEventData pointerEventData, Data data) {
            _timeScoresSO = data as TimeScoresSO;
            levelText.text = $"<color=yellow>Level {_timeScoresSO.LevelCount.ToString()}</color>";
            oneStarText.text = $"More Than <color=red>{_timeScoresSO.BadTime.ToString()}</color> Seconds";
            twoStarText.text = $"Between <color=green>{_timeScoresSO.GoodTime.ToString()}</color> and <color=red>{_timeScoresSO.BadTime.ToString()}</color> Seconds";
            threeStarText.text = $"Less Than <color=green>{_timeScoresSO.GoodTime.ToString()}</color> Seconds";
            personalBestText.text = $"<color=orange>Personal Best: {(int)PlayerPrefs.GetFloat($"Level {_timeScoresSO.LevelCount.ToString()} Personal Best")} Seconds</color>";

            base.UpdateTooltip(pointerEventData, data);
        }
    }
}
