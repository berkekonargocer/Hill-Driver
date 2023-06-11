using UnityEngine;

namespace Nojumpo.Scripts
{
    public static class Helper
    {
        // -------------------------------- FIELDS ---------------------------------
        static Camera _camera;

        public static Camera MainCamera {
            get {
                if (_camera == null)
                {
                    _camera = Camera.main;
                }
                return _camera;
            }
        }

        
        // -------------------------------- METHODS ---------------------------------
        public static Vector2 GetWorldPositionOfCanvasElement(RectTransform rectTransform) {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, rectTransform.position, MainCamera, out var result);
            return result;
        }

    }
}
