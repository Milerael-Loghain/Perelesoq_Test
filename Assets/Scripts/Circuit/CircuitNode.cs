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

    public bool DefaultActiveState
    {
        get => _defaultActiveState;
        set => _defaultActiveState = value;
    }

    public string GeneratedGuid => _generatedGuid;

    public List<CircuitNode> OutputNodes => _outputNodes;
    public ICircuitNodeLogic CircuitNodeLogic => _circuitNodeLogic;

    [SerializeField] private string _generatedGuid = Guid.NewGuid().ToString();
    [SerializeField] private int _index;
    [SerializeField] private CircuitDeviceType _deviceType;
    [SerializeField] private bool _defaultActiveState;
    [SerializeField] private List<CircuitNode> _inputNodes = new();
    [SerializeField] private GameObject _nodeGameObject;
    [SerializeField] private List<CircuitNode> _outputNodes = new();

    private ICircuitNodeLogic _circuitNodeLogic;
    private IDeviceView _deviceView;
    private IDeviceUIView _deviceUIView;

    public void Initialize(DeviceConfigBase deviceConfigBase)
    {
        _generatedGuid = Guid.NewGuid().ToString();

        if (!typeof(ICircuitNodeLogic).IsAssignableFrom(deviceConfigBase.DeviceLogicType)) return;

        _deviceView = _nodeGameObject.GetComponent<IDeviceView>();

        _circuitNodeLogic = (ICircuitNodeLogic) Activator.CreateInstance(deviceConfigBase.DeviceLogicType);

        _circuitNodeLogic.OnStateChanged += OnSetStateFromLogic;
        _circuitNodeLogic.Initialize(this, deviceConfigBase);
    }

    public void BindUIView(IDeviceUIView deviceUIView)
    {
        _deviceUIView = deviceUIView;
        _deviceUIView.OnSetState += OnSetStateFromUI;
    }

    public void Dispose()
    {
        _circuitNodeLogic.OnStateChanged -= OnSetStateFromLogic;
        _deviceUIView.OnSetState -= OnSetStateFromUI;
    }

    private void OnSetStateFromUI(bool isActive)
    {
        _circuitNodeLogic.SetActiveState(isActive);
    }

    private void OnSetStateFromLogic(bool isActive, bool hasCurrent)
    {
        _deviceView?.SetVisualState(isActive, hasCurrent);
        _deviceUIView?.UpdateStateInfo(isActive, hasCurrent);
    }
}