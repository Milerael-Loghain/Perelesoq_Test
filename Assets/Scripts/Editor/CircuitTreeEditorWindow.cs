using System.Collections.Generic;
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
            var powerSource = FindObjectOfType<PowerSource>();

            if (powerSource == null)
            {
                EditorGUILayout.LabelField("Power Source Doesn't Exits");
                return;
            }


            if (GUILayout.Button("Set Indexes"))
            {
                int index = 0;
                Dictionary<GameObject, int> indexByGameObject = new();
                SetIndexesRecursive(powerSource.CircuitTree.RootNode, ref index, ref indexByGameObject);
            }

            DrawNode(powerSource.CircuitTree.RootNode);
            // DrawNode(property.FindPropertyRelative("_rootNode"));

            // // Calculate rects
            // var amountRect = new Rect(position.x, position.y, 30, position.height);
            // var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
            // var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);
            //
            // // Draw fields - pass GUIContent.none to each so they are drawn without labels
            // EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            // EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
            // EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
            //
            // // Set indent back to what it was
            // EditorGUI.indentLevel = indent;
        }

        private void DrawNode(CircuitNode circuitNode)
        {
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(300f));
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

        private void SetIndexesRecursive(CircuitNode node, ref int index, ref Dictionary<GameObject, int> indexByGameObject)
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

            foreach (var childNode in node.OutputNodes)
            {
                SetIndexesRecursive(childNode, ref index, ref indexByGameObject);
            }
        }
    }
}