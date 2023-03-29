using System;
using System.Collections.Generic;
using System.Linq;
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
    private List<CircuitNode> _nodes;

    private void Awake()
    {
        ServiceLocator.Instance.Register(this);
        FixReferences();
    }

    private void FixReferences()
    {
        int index = 0;
        Dictionary<GameObject, int> indexByGameObject = new();
        Dictionary<int, CircuitNode> nodeByIndex = new();
        SetIndexesAndParentsRecursive(null, CircuitTree.RootNode, ref index, ref indexByGameObject, ref nodeByIndex);

        _nodes = new List<CircuitNode>();

        for (var i = 0; i < nodeByIndex.Count; i++)
        {
            _nodes.Add(nodeByIndex[i]);
        }
    }

    private void SetIndexesAndParentsRecursive(CircuitNode parentNode, CircuitNode node, ref int index, ref Dictionary<GameObject, int> indexByGameObject, ref Dictionary<int, CircuitNode> nodeByIndex)
    {
        if (indexByGameObject.ContainsKey(node.NodeGameObject))
        {
            node.Index = indexByGameObject[node.NodeGameObject];
            Debug.Log($"Found Same GameObject in Other Node! Assigning Existing Index! {node.Index}");
        }
        else
        {
            node.Index = index;
            indexByGameObject[node.NodeGameObject] = index;
            index++;
        }

        if (nodeByIndex.ContainsKey(node.Index))
        {
            node = nodeByIndex[node.Index];
        }
        else
        {
            nodeByIndex[node.Index] = node;
            node.InputNodes = new List<CircuitNode>();
        }

        if (parentNode != null)
        {
            if (!node.InputNodes.Any(node => node.Index == parentNode.Index))
            {
                node.InputNodes.Add(parentNode);
            }
        }

        foreach (var childNode in node.OutputNodes)
        {
            SetIndexesAndParentsRecursive(node, childNode, ref index, ref indexByGameObject, ref nodeByIndex);
        }
    }

    public void Initialize(DevicesConfig devicesConfig)
    {
        foreach (var node in _nodes)
        {
            var deviceConfig = devicesConfig.GetDeviceConfigByType(node.DeviceType);

            node.Initialize(deviceConfig);
        }

        _nodes.FirstOrDefault()?.CircuitNodeLogic.RefreshCurrentState();
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