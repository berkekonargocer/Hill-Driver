using UnityEngine;
using UnityEngine.U2D;

namespace Nojumpo
{
    [ExecuteInEditMode]
    public class ProceduralTerrainGenerator : MonoBehaviour
    {
        // -------------------------------- FIELDS --------------------------------
        [Header("GENERATION SETTINGS")]
        [SerializeField] SpriteShapeController _spriteShapeController;
        [SerializeField, Range(3.0f, 500.0f)] int _terrainLength = 100;
        [SerializeField, Range(1.0f, 50.0f)] float _xMultiplier = 2.0f;
        [SerializeField, Range(1.0f, 50.0f)] float _yMultiplier = 2.0f;
        [SerializeField, Range(0.0f, 1.0f)] float _curveSmoothness = 0.5f;
        [SerializeField] float _noiseStep = 0.5f;
        [SerializeField] float _bottom = 10.0f;
        Vector3 _lastPosition;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnValidate() {
            _spriteShapeController.spline.Clear();
            GenerateTerrain();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void GenerateTerrain() {
            for (int i = 0; i < _terrainLength; i++)
            {
                _lastPosition = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier);
                _spriteShapeController.spline.InsertPointAt(i, _lastPosition);

                if (i != 0 && i != _terrainLength - 1)
                {
                    _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                    _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                    _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);
                }
            }

            _spriteShapeController.spline.InsertPointAt(_terrainLength, new Vector3(_lastPosition.x, transform.position.y - _bottom));
            _spriteShapeController.spline.InsertPointAt(_terrainLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
        }
    }
}
