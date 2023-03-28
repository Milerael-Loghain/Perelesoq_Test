using System;
using Data;

public interface ICircuitNodeLogic
{
    public event Action<bool> OnActiveStateChanged;

    public bool IsActive { get; }
    public bool HasCurrent { get; }
    public float ActiveEnergyConsumedBySecond { get; }

    public void Initialize(CircuitNode circuitNode, DeviceConfigBase deviceConfigBase);
    public float SetActiveState(bool isActive);
    public void RefreshCurrentState();
}