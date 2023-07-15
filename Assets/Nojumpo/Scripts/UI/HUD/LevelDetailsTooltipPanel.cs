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
            levelText.text = $"Level {_timeScoresSO.LevelCount.ToString()}";
            oneStarText.text = $"More Than {_timeScoresSO.BadTime.ToString()} Seconds";
            twoStarText.text = $"Between {_timeScoresSO.GoodTime.ToString()} and {_timeScoresSO.BadTime.ToString()} Seconds";
            threeStarText.text = $"Less Than {_timeScoresSO.GoodTime.ToString()} Seconds";
            personalBestText.text = $"Personal Best: {(int)PlayerPrefs.GetFloat($"Level {_timeScoresSO.LevelCount.ToString()} Personal Best")} Seconds";

            base.UpdateTooltip(pointerEventData, data);
        }
    }
}
