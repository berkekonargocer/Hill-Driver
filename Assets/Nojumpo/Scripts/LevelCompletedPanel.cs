using System;
using DG.Tweening;
using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using Nojumpo.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Nojumpo
{
    public class LevelCompletedPanel : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] TimeScoresSO timeScoresSO;
        [SerializeField] RectTransform mainPanelRectTransform;
        [SerializeField] GameObject backgroundPanel;
        [SerializeField] TextMeshProUGUI congratulationsText;
        [SerializeField] GameObject personalBestTextObject;
        
        
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onLevelCompleted += SetCongratulationsText;
            GameManager.onLevelCompleted += timeScoresSO.SetPersonalBest;
            GameManager.onLevelCompleted += ActivatePersonalBestPanel;
            GameManager.onLevelCompleted += EnableBackgroundPanel;
            GameManager.onLevelCompleted += ScaleMainPanel;
        }

        void OnDisable() {
            GameManager.onLevelCompleted -= SetCongratulationsText;
            GameManager.onLevelCompleted -= timeScoresSO.SetPersonalBest;
            GameManager.onLevelCompleted -= ActivatePersonalBestPanel;
            GameManager.onLevelCompleted -= EnableBackgroundPanel;
            GameManager.onLevelCompleted -= ScaleMainPanel;
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

        void SetStarScore() {
            if (TimerManager.Instance.CurrentTime <= timeScoresSO.GoodTime)
            {
                //three star
            }
            else if (TimerManager.Instance.CurrentTime >= timeScoresSO.BadTime)
            {
                //one star
            }
            else
            {
                //two star
            }
        }
        
        void ActivatePersonalBestPanel() {
            if (timeScoresSO.IsPersonalBest())
            {
                personalBestTextObject.SetActive(true);
            }
        }
    }
}