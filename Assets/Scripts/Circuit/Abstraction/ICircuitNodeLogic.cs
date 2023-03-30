using System;
using Data;

public interface ICircuitNodeLogic
{
    public event Action<bool, bool, float> OnStateChanged;

    public bool IsActive { get; }
    public bool HasCurrent { get; }
    public float ActiveEnergyConsumedByHour { get; }

    public void Initialize(CircuitNode circuitNode, DeviceConfigBase deviceConfigBase);
    public void SetActiveState(bool isActive);
    public void RefreshCurrentState();
}