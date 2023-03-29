using System;
using Circuit;
using Data;
using UnityEngine;

namespace Devices.Data
{
    [CreateAssetMenu]
    public class DoorDriverDeviceConfig : DeviceConfigBase
    {
        public override Type DeviceLogicType => typeof(CircuitNodeLogicBase);
        public override float ActiveEnergyConsumedByHour => 0;
        public override float EnergyConsumedByActivation => _energyConsumedByActivation;
        public override GameObject DeviceUiView => _deviceUIView;

        [SerializeField] private float _energyConsumedByActivation;
        [SerializeField] private GameObject _deviceUIView;
    }
}