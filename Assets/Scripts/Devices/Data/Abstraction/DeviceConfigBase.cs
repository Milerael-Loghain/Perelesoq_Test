using System;
using UnityEngine;

namespace Data
{
    public class DeviceConfigBase : ScriptableObject
    {
        public Type DeviceLogicType { get; }
        public float ActiveEnergyConsumedByHour { get; }
        public float EnergyConsumedByActivation { get; }

        public bool InitialActiveState { get; }
    }
}