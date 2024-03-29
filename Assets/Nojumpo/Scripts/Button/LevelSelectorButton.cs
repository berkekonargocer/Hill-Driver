using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nojumpo
{
    [RequireComponent(typeof(CanvasGroup))]
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
            levelCountText.text = levelDetailsSo.LevelNumber.ToString();
            _buttonCanvasGroup = GetComponent<CanvasGroup>();
        }
        
        void UpdateButton(Scene scene, LoadSceneMode loadSceneMode) {
            if (levelDetailsSo.IsLocked)
            {
                LockLevel();
                return;
            }

            if (levelDetailsSo.LevelNumber != 1)
            {
                UnlockLevel();
            }

            UpdateStars();
        }
        
        void LockLevel() {
            lockedState.gameObject.SetActive(true);
            unlockedState.gameObject.SetActive(false);
            _buttonCanvasGroup.blocksRaycasts = false;
            _buttonCanvasGroup.interactable = false;
        }
        
        void UnlockLevel() {
            lockedState.gameObject.SetActive(false);
            unlockedState.gameObject.SetActive(true);
            _buttonCanvasGroup.blocksRaycasts = true;
            _buttonCanvasGroup.interactable = true;
        }

        void UpdateStars() {

            string levelPbPlayerPrefsKey = $"Level {levelDetailsSo.LevelNumber.ToString()} Personal Best";

            if (PlayerPrefs.GetInt(levelPbPlayerPrefsKey) <= 0)
                return;

            if (PlayerPrefs.GetInt(levelPbPlayerPrefsKey) >= levelDetailsSo.BadTime)
            {
                stars[0].color = Color.white;
            }
            else if (PlayerPrefs.GetInt(levelPbPlayerPrefsKey) <= levelDetailsSo.GoodTime)
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
        
        void PlayLevel() {
            GameObject.FindWithTag("UI/Tooltip Canvas").SetActive(false);
            GameManager.Instance.StartGame(levelBuildIndex);
        }
        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void OnClick() {
            PlayLevel();
        }
    }
}