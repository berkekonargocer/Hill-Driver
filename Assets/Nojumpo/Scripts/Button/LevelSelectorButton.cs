using System;
using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nojumpo
{
    public class LevelSelectorButton : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] TimeScoresSO timeScoresSO;
        [SerializeField] TextMeshProUGUI levelCountText;
        
        [SerializeField] int levelBuildIndex;
        [SerializeField] int levelCount;

        [SerializeField] Image[] stars;
        

        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            SceneManager.sceneLoaded += UpdateStars;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= UpdateStars;
        }

        void Awake() {
            SetComponents();
        }

        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void UpdateStars(Scene scene, LoadSceneMode loadSceneMode) {
            if (timeScoresSO.PersonalBest == 0)
                return;
            
            if (timeScoresSO.PersonalBest >= timeScoresSO.BadTime)
            {
                stars[0].color = Color.white;
            }
            else if (timeScoresSO.PersonalBest <= timeScoresSO.GoodTime)
            {
                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].color = Color.white;;
                }
            }
            else
            {
                for (int i = 0; i < stars.Length - 1; i++)
                {
                    stars[i].color = Color.white;;
                }
            }
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        void SetComponents() {
            levelCountText.text = levelCount.ToString();
        }
        
        public void OnClick() {
            GameObject.FindWithTag("UI/Menu Canvas").SetActive(false);
            LevelManager.Instance.CurrentLevel = levelCount;
            LevelManager.Instance.StartGame(levelBuildIndex);
        }
    }
}