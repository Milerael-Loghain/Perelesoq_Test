using System;
using Circuit;
using Data;
using UnityEngine;

namespace Devices.Data
{
    [CreateAssetMenu]
    public class AndGateDeviceConfig : DeviceConfigBase
    {
        public override Type DeviceLogicType => typeof(AlwaysActiveCircuitNodeLogic);
        public override float ActiveEnergyConsumedByHour => 0;
        public override float EnergyConsumedByActivation => 0;
        public override bool CanBeActivatedWithoutCurrent => true;
        public override GameObject DeviceUiView => _deviceUIView;

        [SerializeField] private GameObject _deviceUIView;
    }
}