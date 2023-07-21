using UnityEngine;

namespace Nojumpo
{
    public class ParallaxEffect : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        Transform _cameraTransform;
        Vector3 _lastCameraPosition;

        float _textureUnitSizeX;
        float _textureUnitSizeY;
        
        [SerializeField] Vector2 parallaxEffectMultiplier;
        
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }

        void LateUpdate() {
            ApplyParallax();
            RepeatXPosition();
        }

        
        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _cameraTransform = Camera.main.transform;
            _lastCameraPosition = _cameraTransform.position;
            
            Sprite objectSprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D objectTexture2D = objectSprite.texture;
            _textureUnitSizeX = objectTexture2D.width / objectSprite.pixelsPerUnit;
            _textureUnitSizeY = objectTexture2D.height / objectSprite.pixelsPerUnit;
        }

        void ApplyParallax() {
            Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
            _lastCameraPosition = _cameraTransform.position;
        }
        
        void RepeatXPosition() {
            if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX)
            {
                //float offsetPositionX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX; use this like (_cameraTransform.position.x + offsetPositionX, ...) if needed 
                transform.position = new Vector3(_cameraTransform.position.x, transform.position.y);
            }
        }

        /// <summary>
        /// Use this with a bool if you need a infinite background in y position
        /// </summary>
        void RepeatYPosition() {
            if (Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSizeY)
            {
                //float offsetPositionY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSizeY; use this like (..., _cameraTransform.position.y + offsetPositionY) if needed 
                transform.position = new Vector3(transform.position.x, _cameraTransform.position.y);
            }
        }
    }
}