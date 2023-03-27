using System;
using UnityEngine;

[Serializable]
public class CircuitTree
{
    public CircuitNode RootNode => _rootNode;

    [SerializeField] private CircuitNode _rootNode;
}