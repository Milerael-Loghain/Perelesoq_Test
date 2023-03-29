using System;
using UnityEngine;

namespace Data
{
    public class DeviceConfigBase : ScriptableObject
    {
        public virtual Type DeviceLogicType { get; }
        public virtual float ActiveEnergyConsumedByHour { get; }
        public virtual float EnergyConsumedByActivation { get; }
        public virtual GameObject DeviceUiView { get; }
    }
}