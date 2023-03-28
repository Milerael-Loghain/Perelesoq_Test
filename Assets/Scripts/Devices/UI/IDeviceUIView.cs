using System;

namespace Devices.UI
{
    public interface IDeviceUIView
    {
        public event Action<bool> OnSetState;

        public void UpdateStateInfo(bool isActive);
    }
}