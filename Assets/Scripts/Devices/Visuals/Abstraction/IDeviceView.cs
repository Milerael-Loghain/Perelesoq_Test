namespace Devices.Visuals.Abstraction
{
    public interface IDeviceView
    {
        public void SetVisualState(bool isActive, bool hasCurrent);
    }
}