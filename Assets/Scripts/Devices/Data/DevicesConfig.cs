using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Devices.Data
{
    [CreateAssetMenu]
    public class DevicesConfig : ScriptableObject
    {
        [SerializeField] private List<DeviceConfigByType> DeviceConfigByTypes;

        public DeviceConfigBase GetDeviceConfigByType(CircuitDeviceType circuitDeviceType)
        {
            foreach (var deviceConfigByType in DeviceConfigByTypes)
            {
                if (deviceConfigByType.DeviceType == circuitDeviceType) return deviceConfigByType.DeviceConfigBase;
            }

            Debug.LogError($"Config Not Found For Device: {circuitDeviceType}");
            return null;
        }
    }
}