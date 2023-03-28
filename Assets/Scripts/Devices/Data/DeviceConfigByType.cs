using System;
using Data;

namespace Devices.Data
{
    [Serializable]
    public struct DeviceConfigByType
    {
        public CircuitDeviceType DeviceType;
        public DeviceConfigBase DeviceConfigBase;
    }
}