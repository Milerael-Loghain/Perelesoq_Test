using System;
using System.Collections.Generic;
using Devices;
using Devices.Data;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public CircuitTree CircuitTree => _circuitTree;
    public List<CircuitNode> Nodes
    {
        get => _nodes;
        set => _nodes = value;
    }

    [SerializeField] private CircuitTree _circuitTree;
    [SerializeField] private List<CircuitNode> _nodes;
    [SerializeField] private DevicesConfig _devicesConfig;

    private void Awake()
    {
        foreach (var node in _nodes)
        {
            var deviceConfig = _devicesConfig.GetDeviceConfigByType(node.DeviceType);

            node.Initialize(deviceConfig);
        }
    }

    private void OnDestroy()
    {
        foreach (var node in _nodes)
        {
            node.Dispose();
        }
    }

    public void SetActiveState(int nodeId, bool isActive)
    {
        if (nodeId >= 0 && nodeId < _nodes.Count)
        {
            var node = _nodes[nodeId];
            node.CircuitNodeLogic.SetActiveState(isActive);
        }
    }
}