using System;

namespace Devices.UI
{
    public interface IDeviceUIView
    {
        public event Action<bool> OnSetState;

        public void Initialize(string deviceName, bool isActive);
        public void UpdateStateInfo(bool isActive, bool hasCurrent);
    }
}