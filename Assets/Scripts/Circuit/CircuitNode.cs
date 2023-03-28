using System;
using System.Collections.Generic;
using Data;
using Devices;
using UnityEngine;

[Serializable]
public class CircuitNode
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

    public void Initialize(DeviceConfigBase deviceConfigBase)
    {
        if (deviceConfigBase.DeviceLogicType is not ICircuitNodeLogic) return;

        _circuitNodeLogic = (ICircuitNodeLogic) Activator.CreateInstance(deviceConfigBase.DeviceLogicType);
        _circuitNodeLogic.Initialize(this, deviceConfigBase);
    }
}