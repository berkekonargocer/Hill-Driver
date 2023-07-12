using System;
using DG.Tweening;
using Nojumpo.Managers;
using Nojumpo.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Nojumpo
{
    public class LevelCompletedPanel : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] RectTransform mainPanelRectTransform;
        [SerializeField] GameObject backgroundPanel;
        [SerializeField] TextMeshProUGUI congratulationsText;
        [SerializeField] GameObject personalBestTextObject;
        
        
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onLevelCompleted += SetCongratulationsText;
            GameManager.onLevelCompleted += SetPersonalBest;
            GameManager.onLevelCompleted += EnableBackgroundPanel;
            GameManager.onLevelCompleted += ScaleMainPanel;
        }

        void OnDisable() {
            GameManager.onLevelCompleted -= SetCongratulationsText;
            GameManager.onLevelCompleted -= SetPersonalBest;
            GameManager.onLevelCompleted -= EnableBackgroundPanel;
            GameManager.onLevelCompleted -= ScaleMainPanel;
        }

        void Awake() {
            if (PlayerPrefs.GetFloat($"Level {LevelManager.Instance.CurrentLevel.ToString()} Personal Best") <= 0)
            {
                PlayerPrefs.SetFloat($"Level {LevelManager.Instance.CurrentLevel.ToString()} Personal Best", 900);
            }
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void EnableBackgroundPanel() {
            backgroundPanel.SetActive(true);
        }

        void ScaleMainPanel() {
            mainPanelRectTransform.DOScale(1, 1);
        }

        void SetCongratulationsText() {
            float minutes = Mathf.FloorToInt(TimerManager.Instance.CurrentTime / 60);
            congratulationsText.text = $"Congratulations! \n You reached to the flag in: \n {minutes.ToString()} Minutes {TimerManager.Instance.CurrentTime % 60:00} Seconds";
        }

        void SetPersonalBest() {
            if (TimerManager.Instance.CurrentTime < PlayerPrefs.GetFloat($"Level {LevelManager.Instance.CurrentLevel.ToString()} Personal Best"))
            {
                PlayerPrefs.SetFloat($"Level {LevelManager.Instance.CurrentLevel.ToString()} Personal Best", TimerManager.Instance.CurrentTime);
                personalBestTextObject.SetActive(true);
            }
        }
    }
}