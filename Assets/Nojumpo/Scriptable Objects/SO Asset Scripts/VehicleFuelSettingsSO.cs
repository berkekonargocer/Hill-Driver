using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewVehicleFuelSettingsSO", menuName = "Nojumpo/Scriptable Objects/Vehicle Data/New Vehicle Fuel Settings SO")]
    public class VehicleFuelSettingsSO : ScriptableObject
    {
        
#if UNITY_EDITOR

        [TextArea]
        [SerializeField] string _developerDescription;

#endif

        [SerializeField] float fuelDrainAmount = 0.0013f;
        public float FuelDrainAmount { get { return fuelDrainAmount; } }

    }
}