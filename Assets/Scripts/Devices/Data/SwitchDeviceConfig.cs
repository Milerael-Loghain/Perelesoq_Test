using System;
using Circuit;
using Data;
using UnityEngine;

namespace Devices.Data
{
    [CreateAssetMenu]
    public class SwitchDeviceConfig : DeviceConfigBase
    {
        public override Type DeviceLogicType => typeof(CircuitNodeLogicBase);
        public override float ActiveEnergyConsumedByHour => 0;
        public override float EnergyConsumedByActivation => 0;
        public override GameObject DeviceUiView => _deviceUIView;
        public override bool CanBeActivatedWithoutCurrent => true;


        [SerializeField] private GameObject _deviceUIView;
    }
}