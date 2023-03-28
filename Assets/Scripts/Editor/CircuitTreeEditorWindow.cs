using System.Collections.Generic;
using System.Linq;
using Devices;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CircuitTreeEditorWindow: EditorWindow
    {
        [MenuItem("Tools/CircuitTreeEditor")]
        public static void ShowWindow()
        {
            // Get existing open window or if none, make a new one:
            CircuitTreeEditorWindow window = (CircuitTreeEditorWindow)GetWindow(typeof(CircuitTreeEditorWindow));
            window.Show();
        }


        public void OnGUI()
        {
            var powerSource = FindObjectOfType<CircuitManager>();

            if (powerSource == null)
            {
                EditorGUILayout.LabelField("Power Source Doesn't Exits");
                return;
            }


            if (GUILayout.Button("Refresh Nodes List"))
            {
                int index = 0;
                Dictionary<GameObject, int> indexByGameObject = new();
                Dictionary<int, CircuitNode> nodeByIndex = new();
                SetIndexesAndParentsRecursive(null, powerSource.CircuitTree.RootNode, ref index, ref indexByGameObject, ref nodeByIndex);

                powerSource.Nodes = nodeByIndex.OrderBy(kp => kp.Key)
                    .Select(kp => kp.Value)
                    .ToList();
            }

            DrawNode(powerSource.CircuitTree.RootNode);
        }

        private void DrawNode(CircuitNode circuitNode)
        {
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(300f));
            circuitNode.DeviceType = (CircuitDeviceType) EditorGUILayout.EnumPopup(circuitNode.DeviceType);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(circuitNode.Index.ToString(), GUILayout.MaxWidth(15f));
            circuitNode.NodeGameObject = (GameObject) EditorGUILayout.ObjectField(circuitNode.NodeGameObject, typeof(GameObject));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(25f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(5f);

            var outputNodes = new List<CircuitNode>(circuitNode.OutputNodes);
            foreach (var outputNode in outputNodes)
            {
                DrawNode(outputNode);
                if (GUILayout.Button("-"))
                {
                    circuitNode.OutputNodes.Remove(outputNode);
                }

                GUILayout.Space(10f);
            }

            if (GUILayout.Button("+"))
            {
                circuitNode.OutputNodes.Add(new CircuitNode());
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
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
    }
}