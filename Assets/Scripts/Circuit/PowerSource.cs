using System;
using System.Collections.Generic;
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

    private void Awake()
    {
        foreach (var node in _nodes)
        {

        }
    }
}