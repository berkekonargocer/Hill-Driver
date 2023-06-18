using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class RestartButton : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [SerializeField] float holdDownTime = 2.0f;

        float _currentHoldDownTime = 0.0f;
        bool _isHoldingDown = false;

        // ------------------------ CUSTOM PUBLIC METHODS -------------------------

        public void OnPointerDown() {
            StartCoroutine(nameof(RestartLevel));
        }

        public void OnPointerUp() {
            _isHoldingDown = false;
            _currentHoldDownTime = 0.0f;
            StopCoroutine(nameof(RestartLevel));
        }

        IEnumerator RestartLevel() {
            _isHoldingDown = true;

            while (_isHoldingDown)
            {
                _currentHoldDownTime += Time.deltaTime;

                yield return null;

                if (_currentHoldDownTime >= holdDownTime)
                {
                    _currentHoldDownTime = 0.0f;
                    _isHoldingDown = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    StopCoroutine(nameof(RestartLevel));
                }
            }
        }
        
    }
}
