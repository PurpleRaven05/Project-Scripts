using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public class GraphSaveUtility
{
    private VistaGrafo targetGraphView;
    private DialogContainer containerCache;
    
    private List<Edge> Edges => targetGraphView.edges.ToList();
    private List<Nodo> Nodes => targetGraphView.nodes.ToList().Cast<Nodo>().ToList();

    public static GraphSaveUtility GetInstance(VistaGrafo targetGraphView)
    {
        return new GraphSaveUtility
        {
            targetGraphView = targetGraphView
        };
    }
    public void SaveGraph(string nombreArchivo)
    {
        if (!Edges.Any()) return;

        var dialogContainer = ScriptableObject.CreateInstance<DialogContainer>();
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (var i=0; i<connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as Nodo;
            var inputNode = connectedPorts[i].input.node as Nodo;

            dialogContainer.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }
        foreach (var nodo in Nodes.Where(nodo => !nodo.EntryPoint))
        {
            dialogContainer.DialogNodeDatas.Add(new DialogNodeData
            {
                Guid = nodo.GUID,
                DialogueText = nodo.DialogueText,
                Position = nodo.GetPosition().position
            });
        }

        //Crea una carpeta de recursos si no existia
        if (!AssetDatabase.IsValidFolder(path: "Assets/Resources"))
            AssetDatabase.CreateFolder(parentFolder: "Assets", newFolderName: "Resources");

        AssetDatabase.CreateAsset(dialogContainer, path: $"Assets/Resources/{nombreArchivo}.asset");
        AssetDatabase.SaveAssets();

    }
    public void LoadGraph(string nombreArchivo)
    {
        containerCache = Resources.Load<DialogContainer>(nombreArchivo);
        if (containerCache == null)//si lo que cargamos no existe mandamos error
        {
            EditorUtility.DisplayDialog(title: "File not found", message: "Requested file doesn't exist", ok: "Ok");
            return;
        }
        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }
    private void ClearGraph()
    {
        Nodes.Find(x => x.EntryPoint).GUID = containerCache.NodeLinks[0].BaseNodeGuid;

        foreach(var nodo in Nodes)
        {
            if (nodo.EntryPoint) continue;

            //elimina todas las conexiones del nodo
            Edges.Where(x => x.input.node == nodo).ToList()
                    .ForEach(edge => targetGraphView.RemoveElement(edge));
            //elimina el nodo
            targetGraphView.RemoveElement(nodo);
        }
    }
    private void CreateNodes()
    {
        foreach(var nodeData in containerCache.DialogNodeDatas)
        {
            var tempNode = targetGraphView.CreateConverNode(nodeData.DialogueText);
            tempNode.GUID = nodeData.Guid;
            targetGraphView.AddElement(tempNode);

            var nodePorts = containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => targetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }
    private void ConnectNodes()
    {
        for (var i = 0; i < Nodes.Count; i++)
        {
            var k = i; //Prevent access to modified closure
            var connections = containerCache.NodeLinks.Where(x => x.BaseNodeGuid == Nodes[k].GUID).ToList();
            for (var j = 0; j < connections.Count(); j++)
            {
                var targetNodeGUID = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
                LinkNodesTogether(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(
                    containerCache.DialogNodeDatas.First(x => x.Guid == targetNodeGUID).Position,
                    targetGraphView.defaultNodeSize));
            }
        }
    }
    private void LinkNodesTogether(Port outputPort, Port inputPort)
    {
        var tempEdge = new Edge()
        {
            output = outputPort,
            input = inputPort
        };
        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);
        targetGraphView.Add(tempEdge);
    }
}

