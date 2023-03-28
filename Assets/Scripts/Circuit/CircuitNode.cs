using System;
using System.Collections.Generic;
using Data;
using Devices;
using Devices.UI;
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
    [SerializeReference] private List<CircuitNode> _inputNodes = new();
    [SerializeField] private GameObject _nodeGameObject;
    [SerializeReference] private List<CircuitNode> _outputNodes = new();

    private ICircuitNodeLogic _circuitNodeLogic;
    private IDeviceView _deviceView;
    private IDeviceUIView _deviceUIView;

    public void Initialize(DeviceConfigBase deviceConfigBase)
    {
        if (deviceConfigBase.DeviceLogicType is not ICircuitNodeLogic) return;

        _deviceView = _nodeGameObject.GetComponent<IDeviceView>();

        _circuitNodeLogic = (ICircuitNodeLogic) Activator.CreateInstance(deviceConfigBase.DeviceLogicType);

        _circuitNodeLogic.OnActiveStateChanged += OnSetStateFromLogic;
        _circuitNodeLogic.Initialize(this, deviceConfigBase);
    }

    public void BindUIView(IDeviceUIView deviceUIView)
    {
        _deviceUIView = deviceUIView;
        _deviceUIView.OnSetState += OnSetStateFromUI;
    }

    public void Dispose()
    {
        _circuitNodeLogic.OnActiveStateChanged -= OnSetStateFromLogic;
        _deviceUIView.OnSetState -= OnSetStateFromUI;
    }

    private void OnSetStateFromUI(bool isActive)
    {
        _circuitNodeLogic.SetActiveState(isActive);
    }

    private void OnSetStateFromLogic(bool isActive)
    {
        _deviceView.SetVisualState(isActive);
        _deviceUIView.UpdateStateInfo(isActive);
    }
}