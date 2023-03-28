using System;
using Circuit;
using Data;
using UnityEngine;

namespace Devices.Data
{
    [CreateAssetMenu]
    public class DoorDriverDeviceConfigBase : DeviceConfigBase
    {
        public Type DeviceLogicType => typeof(AlwaysActiveCircuitNodeLogic);
        public float ActiveEnergyConsumedBySecond => 0;
        public float EnergyConsumedByActivation => _energyConsumedByActivation;
        public bool InitialActiveState => false;

        [SerializeField]
        private float _energyConsumedByActivation;
    }
}