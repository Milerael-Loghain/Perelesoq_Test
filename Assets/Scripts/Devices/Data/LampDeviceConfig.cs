using System;
using Circuit;
using Data;
using UnityEngine;

namespace Devices.Data
{
    [CreateAssetMenu]
    public class LampDeviceConfig : DeviceConfigBase
    {
        public override Type DeviceLogicType => typeof(AlwaysActiveCircuitNodeLogic);
        public override float ActiveEnergyConsumedByHour => _activeEnergyConsumedByHour;
        public override float EnergyConsumedByActivation => 0;
        public override GameObject DeviceUiView => _deviceUIView;

        [SerializeField] private float _activeEnergyConsumedByHour;
        [SerializeField] private GameObject _deviceUIView;
    }
}