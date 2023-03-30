using System;
using System.Collections.Generic;
using System.Linq;
using Devices;
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
    private List<ICircuitNodeLogic> _cameraLogicNodes;

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
            var nodeIndex = parentNode.OutputNodes.IndexOf(node);
            node = nodeByIndex[node.Index];
            parentNode.OutputNodes[nodeIndex] = node;
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

        for (var i = node.OutputNodes.Count - 1; i >= 0; i--)
        {
            var childNode = node.OutputNodes[i];
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

        _cameraLogicNodes = new();
        foreach (var circuitNode in _nodes)
        {
            if (circuitNode.DeviceType == CircuitDeviceType.Camera)
            {
                var circuitNodeLogic = circuitNode.CircuitNodeLogic;
                _cameraLogicNodes.Add(circuitNodeLogic);
                circuitNodeLogic.OnStateChanged += (b, b1, f) => OnCameraNodeActivated(_cameraLogicNodes.IndexOf(circuitNodeLogic));
            }
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

    private void OnCameraNodeActivated(int cameraIndex)
    {
        for (int i = 0; i < _cameraLogicNodes.Count; i++)
        {
            if (i == cameraIndex) continue;
            var cameraLogic = _cameraLogicNodes[i];

            cameraLogic.SetActiveState(false);
        }
    }
}