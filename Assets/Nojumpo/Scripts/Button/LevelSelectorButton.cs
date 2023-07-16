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
        [SerializeField] LevelDetailsSO levelDetailsSo;
        [SerializeField] GameObject unlockedState;
        [SerializeField] GameObject lockedState;

        [SerializeField] int levelBuildIndex;

        [SerializeField] Image[] stars;

        CanvasGroup _buttonCanvasGroup;
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            SceneManager.sceneLoaded += UpdateButton;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= UpdateButton;
        }

        void Awake() {
            SetComponents();
        }

        
        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            levelCountText.text = levelDetailsSo.LevelCount.ToString();
            _buttonCanvasGroup = GetComponent<CanvasGroup>();
        }
        
        void UpdateButton(Scene scene, LoadSceneMode loadSceneMode) {
            if (levelDetailsSo.IsLocked)
            {
                lockedState.gameObject.SetActive(true);
                unlockedState.gameObject.SetActive(false);
                _buttonCanvasGroup.blocksRaycasts = false;
                _buttonCanvasGroup.interactable = false;
                return;
            }

            if (levelDetailsSo.LevelCount != 1)
            {
                lockedState.gameObject.SetActive(false);
                unlockedState.gameObject.SetActive(true);
                _buttonCanvasGroup.blocksRaycasts = true;
                _buttonCanvasGroup.interactable = true;
            }

            UpdateStars();
        }
        
        void UpdateStars() {

            string levelPbPlayerPrefsKey = $"Level {levelDetailsSo.LevelCount.ToString()} Personal Best";

            if (PlayerPrefs.GetFloat(levelPbPlayerPrefsKey) <= 0)
                return;

            if (PlayerPrefs.GetFloat(levelPbPlayerPrefsKey) >= levelDetailsSo.BadTime)
            {
                stars[0].color = Color.white;
            }
            else if (PlayerPrefs.GetFloat(levelPbPlayerPrefsKey) <= levelDetailsSo.GoodTime)
            {
                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].color = Color.white;
                    ;
                }
            }
            else
            {
                for (int i = 0; i < stars.Length - 1; i++)
                {
                    stars[i].color = Color.white;
                    ;
                }
            }
        }
        
        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            GameObject.FindWithTag("UI/Menu Canvas").SetActive(false);
            GameObject.FindWithTag("UI/Tooltip Canvas").SetActive(false);
            LevelManager.Instance.CurrentLevel = levelDetailsSo.LevelCount;
            LevelManager.Instance.StartGame(levelBuildIndex);
        }
    }
}