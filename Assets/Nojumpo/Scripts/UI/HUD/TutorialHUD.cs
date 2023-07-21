using UnityEngine;

namespace Nojumpo
{
    public class TutorialHUD : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] Animator[] tutorialAnimators;

        int _currentTipIndex = 0;
        static readonly int IsIdle = Animator.StringToHash("isIdle");
        
        
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void SetupTutorialHUD() {
            _currentTipIndex = 0;

            for (int i = 0; i < tutorialAnimators.Length; i++)
            {
                tutorialAnimators[i].enabled = true;
                tutorialAnimators[i].SetBool(IsIdle, true);
            }
            
            tutorialAnimators[_currentTipIndex].SetBool(IsIdle, false);
        }

        public void DisableAnimators() {
            for (int i = 0; i < tutorialAnimators.Length; i++)
            {
                tutorialAnimators[i].enabled = false;
            }
        }
        
        public void NextTip() {
            if (_currentTipIndex >= 0 && _currentTipIndex < tutorialAnimators.Length - 1)
            {
                tutorialAnimators[_currentTipIndex].SetBool(IsIdle, true);
                ++_currentTipIndex;
                tutorialAnimators[_currentTipIndex].SetBool(IsIdle, false);
            }
        }

        public void PreviousTip() {
            if (_currentTipIndex > 0 && _currentTipIndex <= tutorialAnimators.Length - 1)
            {
                tutorialAnimators[_currentTipIndex].SetBool(IsIdle, true);
                --_currentTipIndex;
                tutorialAnimators[_currentTipIndex].SetBool(IsIdle, false);
            }
        }

    }
}