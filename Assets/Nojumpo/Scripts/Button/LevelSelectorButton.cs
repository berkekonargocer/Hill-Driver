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
        [SerializeField] TextMeshProUGUI levelCountText;
        [SerializeField] TimeScoresSO timeScoresSO;

        [SerializeField] int levelBuildIndex;

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
            string levelPbPlayerPrefsKey = $"Level {timeScoresSO.LevelCount.ToString()} Personal Best";

            if (PlayerPrefs.GetFloat(levelPbPlayerPrefsKey) <= 0)
                return;
            
            if (PlayerPrefs.GetFloat(levelPbPlayerPrefsKey) >= timeScoresSO.BadTime)
            {
                stars[0].color = Color.white;
            }
            else if (PlayerPrefs.GetFloat(levelPbPlayerPrefsKey) <= timeScoresSO.GoodTime)
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
            levelCountText.text = timeScoresSO.LevelCount.ToString();
        }
        
        public void OnClick() {
            GameObject.FindWithTag("UI/Menu Canvas").SetActive(false);
            GameObject.FindWithTag("UI/Tooltip Canvas").SetActive(false);
            LevelManager.Instance.CurrentLevel = timeScoresSO.LevelCount;
            LevelManager.Instance.StartGame(levelBuildIndex);
        }
    }
}