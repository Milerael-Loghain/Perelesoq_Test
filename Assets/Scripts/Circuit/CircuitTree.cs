using System;
using UnityEngine;

[Serializable]
public class CircuitTree
{
    public CircuitNode RootNode
    {
        get => _rootNode;
        set => _rootNode = value;
    }

    [SerializeField] private CircuitNode _rootNode;
}