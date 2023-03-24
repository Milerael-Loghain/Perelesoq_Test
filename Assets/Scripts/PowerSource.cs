using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CircuitNode
{
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

    [SerializeField] private int _index;
    [SerializeField] private List<CircuitNode> _inputNodes = new();
    [SerializeField] private GameObject _nodeGameObject;
    [SerializeField] private List<CircuitNode> _outputNodes = new();
}

[Serializable]
public class CircuitTree
{
    public CircuitNode RootNode => _rootNode;

    [SerializeField] private CircuitNode _rootNode;
}

public class PowerSource : MonoBehaviour
{
    public CircuitTree CircuitTree => _circuitTree;

    [SerializeField] private CircuitTree _circuitTree;
}