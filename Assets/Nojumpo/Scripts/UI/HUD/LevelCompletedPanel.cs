using System.Collections;
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
        [SerializeField] GameObject backgroundPanel;
        [SerializeField] GameObject personalBestTextObject;
        [SerializeField] TextMeshProUGUI congratulationsText;
        [SerializeField] RectTransform mainPanelRectTransform;
        [SerializeField] RectTransform[] buttonRectTransforms;
        [SerializeField] RectTransform[] brightStarIconRectTransforms;
        [SerializeField] AudioClip starAnimationAudio;
        [SerializeField] AudioClip personalBestCelebrationAudio;
        
        [SerializeField] float buttonScaleAnimationDuration;
        [SerializeField] float eachStarAnimationDuration;
        [SerializeField] float starEnlargeAmount;

        AudioSource levelCompletedPanelAudioSource;
        LevelDetailsSO _levelDetailsSO;
        
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.onLevelCompleted += LevelCompletedPanel_OnLevelCompleted;
        }

        void OnDisable() {
            GameManager.onLevelCompleted -= LevelCompletedPanel_OnLevelCompleted;
        }

        void Start() {
            Invoke(nameof(SetComponents), 1.0f);
        }

        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _levelDetailsSO = TimerManager.Instance.LevelDetailsSo;
            levelCompletedPanelAudioSource = GetComponent<AudioSource>();
        }
        
        void EnableBackgroundPanel() {
            backgroundPanel.SetActive(true);
        }

        void ScaleMainPanel() {
            mainPanelRectTransform.DOScale(1, 1).onComplete = SetStarScore;
        }

        void SetCongratulationsText() {
            congratulationsText.text = $"Congratulations! \n You reached to the flag in: \n {(int)TimerManager.Instance.CurrentTime} Seconds";
        }

        void SetStarScore() {
            if ((int)TimerManager.Instance.CurrentTime <= _levelDetailsSO.GoodTime)
            {
                StartCoroutine(StarAnimationRoutine(3));
            }
            else if ((int)TimerManager.Instance.CurrentTime >= _levelDetailsSO.BadTime)
            {
                StartCoroutine(StarAnimationRoutine(1));
            }
            else
            {
                StartCoroutine(StarAnimationRoutine(2));
            }
        }

        void PlayPersonalBestSFX() {
            levelCompletedPanelAudioSource.pitch = 1;
            levelCompletedPanelAudioSource.clip = personalBestCelebrationAudio;
            levelCompletedPanelAudioSource.Play();
        }

        void ActivateButtons() {
            for (int i = 0; i < buttonRectTransforms.Length; i++)
            {
                buttonRectTransforms[i].DOScale(1, buttonScaleAnimationDuration);
            }
        }

        void LevelCompletedPanel_OnLevelCompleted() {
            SetCongratulationsText();
            EnableBackgroundPanel();
            ScaleMainPanel();
        }
        
        IEnumerator PersonalBestRoutine() {
            if (_levelDetailsSO.IsPersonalBest())
            {
                PlayPersonalBestSFX();
                personalBestTextObject.SetActive(true);
                _levelDetailsSO.SetPersonalBest();
            }
            else
            {
                ActivateButtons();
                yield break;
            }

            yield return new WaitForSeconds(0.75f);
            
            ActivateButtons();
        }

        IEnumerator StarAnimationRoutine(int numberOfStars) {
            for (int i = 0; i < brightStarIconRectTransforms.Length; i++)
            {
                brightStarIconRectTransforms[i].localScale = Vector3.zero;
            }
            
            levelCompletedPanelAudioSource.clip = starAnimationAudio;
            levelCompletedPanelAudioSource.pitch = 1f;
            
            for (int i = 0; i < numberOfStars; i++)
            {
                levelCompletedPanelAudioSource.Play();
                yield return StartCoroutine(EnlargeAndShrinkStars(brightStarIconRectTransforms[i]));
                levelCompletedPanelAudioSource.pitch += 0.1f;
            }

            yield return new WaitForSeconds(0.6f);

            StartCoroutine(PersonalBestRoutine());
        }

        IEnumerator EnlargeAndShrinkStars(RectTransform rectTransform) {
            yield return ChangeStarScale(rectTransform, starEnlargeAmount, eachStarAnimationDuration);
            StartCoroutine(ChangeStarScale(rectTransform, 1.0f, eachStarAnimationDuration / 2));
        }

        IEnumerator ChangeStarScale(RectTransform rectTransform, float targetScale, float duration) {
            Vector3 initialScale = rectTransform.localScale;
            Vector3 finalScale = new Vector3(targetScale, targetScale, targetScale);

            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                rectTransform.localScale = Vector3.Lerp(initialScale, finalScale, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = finalScale;
        }
    }
}