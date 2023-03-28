using System;
using System.Collections.Generic;
using Data;
using Devices;
using Devices.Visuals.Abstraction;
using UnityEngine;

[Serializable]
public class CircuitNode : IDisposable
{
    public CircuitDeviceType DeviceType
    {
        get => _deviceType;
        set => _deviceType = value;
    }

    public int Index
    {
        get => _index;
        set => _index = value;
    }

    public List<CircuitNode> InputNodes
    {
        get => _inputNodes;
        set => _inputNodes = value;
    }

    public GameObject NodeGameObject
    {
        get => _nodeGameObject;
        set => _nodeGameObject = value;
    }

    public List<CircuitNode> OutputNodes => _outputNodes;
    public ICircuitNodeLogic CircuitNodeLogic => _circuitNodeLogic;

    [SerializeField] private int _index;
    [SerializeField] private CircuitDeviceType _deviceType;
    [SerializeField] private List<CircuitNode> _inputNodes = new();
    [SerializeField] private GameObject _nodeGameObject;
    [SerializeField] private List<CircuitNode> _outputNodes = new();

    private ICircuitNodeLogic _circuitNodeLogic;
    private IDeviceVisuals _deviceVisuals;

    public void Initialize(DeviceConfigBase deviceConfigBase)
    {
        if (deviceConfigBase.DeviceLogicType is not ICircuitNodeLogic) return;

        _deviceVisuals = _nodeGameObject.GetComponent<IDeviceVisuals>();

        _circuitNodeLogic = (ICircuitNodeLogic) Activator.CreateInstance(deviceConfigBase.DeviceLogicType);

        _circuitNodeLogic.OnActiveStateChanged += _deviceVisuals.SetVisualState;
        _circuitNodeLogic.Initialize(this, deviceConfigBase);
    }

    public void Dispose()
    {
        _circuitNodeLogic.OnActiveStateChanged -= _deviceVisuals.SetVisualState;
    }
}