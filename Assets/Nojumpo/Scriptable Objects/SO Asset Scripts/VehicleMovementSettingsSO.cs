using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewVehicleMovementSettingsSO", menuName = "Nojumpo/Scriptable Objects/Vehicle Data/New Vehicle Movement Settings SO")]
    public class VehicleMovementSettingsSO : ScriptableObject
    {
        
#if UNITY_EDITOR

        [TextArea]
        [SerializeField] string _developerDescription;

#endif

        [SerializeField] float movementSpeed = 50.0f;
        public float MovementSpeed { get { return movementSpeed; } }

    }
}