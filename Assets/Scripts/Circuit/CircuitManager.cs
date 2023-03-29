using System;
using System.Collections.Generic;
using Devices.Data;
using Framework;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
#if UNITY_EDITOR
    public CircuitTree CircuitTree => _circuitTree;
#endif

    public List<CircuitNode> Nodes
    {
        get => _nodes;
#if UNITY_EDITOR
        set => _nodes = value;
#endif
    }

    [SerializeField] [HideInInspector] private CircuitTree _circuitTree;
    [SerializeField] private List<CircuitNode> _nodes;

    private void Awake()
    {
        ServiceLocator.Instance.Register(this);
    }

    public void Initialize(DevicesConfig devicesConfig)
    {
        foreach (var node in _nodes)
        {
            var deviceConfig = devicesConfig.GetDeviceConfigByType(node.DeviceType);

            node.Initialize(deviceConfig);
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.Unregister(this);

        foreach (var node in _nodes)
        {
            node.Dispose();
        }
    }
}