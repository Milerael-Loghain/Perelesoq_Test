using System;
using Circuit;
using Data;
using UnityEngine;

namespace Devices.Data
{
    [CreateAssetMenu]
    public class SwitchDeviceConfigBase : DeviceConfigBase
    {
        public Type DeviceLogicType => typeof(AlwaysActiveCircuitNodeLogic);
        public float ActiveEnergyConsumedBySecond => 0;
        public float EnergyConsumedByActivation => 0;
        public bool InitialActiveState => true;
    }
}