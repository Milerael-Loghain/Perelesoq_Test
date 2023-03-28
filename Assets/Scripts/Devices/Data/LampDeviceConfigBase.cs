using System;
using Circuit;
using Data;
using UnityEngine;

namespace Devices.Data
{
    [CreateAssetMenu]
    public class LampDeviceConfigBase : DeviceConfigBase
    {
        public Type DeviceLogicType => typeof(AlwaysActiveCircuitNodeLogic);
        public float ActiveEnergyConsumedBySecond => _activeEnergyConsumedBySecond;
        public float EnergyConsumedByActivation => 0;
        public bool InitialActiveState => false;

        [SerializeField]
        private float _activeEnergyConsumedBySecond;
    }
}