using UnityEngine;

/*
 * SET THE OBJECT'S ANCHOR TO MIDDLE 
 */

namespace Nojumpo.Scripts.Animation
{
    public class WobbleAnimation : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        Quaternion _targetAngle;

        [Range(0.1f, 5.0f)] [SerializeField] float waitBetweenWobbles = 0.5f;
        [Range(1.0f, 50.0f)] [SerializeField] float intensity = 10.0f;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Start() {
            InvokeRepeating(nameof(ChangeTarget), 0, waitBetweenWobbles);
        }

        void Update() {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetAngle, Time.deltaTime);
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void ChangeTarget() {
            float randomIntensity = Random.Range(0.1f, intensity);
            float curve = Mathf.Sin(Random.Range(0, Mathf.PI * 2));
            _targetAngle = Quaternion.Euler(Vector3.forward * (curve * randomIntensity));
        }
    }
}
