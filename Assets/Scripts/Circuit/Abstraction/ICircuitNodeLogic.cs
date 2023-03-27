using Data;

public interface ICircuitNodeLogic
{
    public bool IsActive { get; }
    public float ActiveEnergyConsumedBySecond { get; }
    public void Initialize(IDeviceConfig deviceConfig);
    public float SetActiveState(bool isActive);
}