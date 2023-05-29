using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph : EditorWindow
{
    private GraphView graphView;
    private string nombreArchivo= "New Dialog";

    [MenuItem("Graphs/Conversation Graph")]
    public static void OpenConversationGraphWindow()
    {
        var window = GetWindow<Graph>();
        window.titleContent = new GUIContent(text: "Conversation Graph");
    }
    private void OnEnable()
    {
        graphView = new GraphView
        {
            name = "Conversation Graph"
        };

        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);

        GenerateToolBar();
        GenerateMinimap();

    }
    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    //barra de herramientas para crear más nodos
    private void GenerateToolBar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField(label: "File Name:");
        fileNameTextField.SetValueWithoutNotify(nombreArchivo);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => nombreArchivo = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(true)) { text = "Save" });
        toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(false)) { text = "Load" });

        var nodeCreateButton = new Button(clickEvent: () =>
        {
            graphView.CreateNode("Dialog Node");
        });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }
    private void RequestDataOperation(bool save)//para cargar y guardar
    {
        if (string.IsNullOrEmpty(nombreArchivo))
        {
            EditorUtility.DisplayDialog(title: "File Name Invalid", message: "Please introduce a valid name", ok:"Ok");
            return;
        }
        var saveUtility = GraphSaveUtility.GetInstance(graphView);
        if (save)
            saveUtility.SaveGraph(nombreArchivo);
        else
        {
            saveUtility.LoadGraph(nombreArchivo);
        }
    }
    private void GenerateMinimap()
    {
        var miniMap = new MiniMap { anchored = true };
        miniMap.SetPosition(newPos: new Rect(x: 10, y: 30, width: 200, height: 140));
        graphView.Add(miniMap);
    }
}
