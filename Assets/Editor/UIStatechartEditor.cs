using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

public class UIStatechartEditor : EditorWindow {
    UIStatechart EditedUIStatechart;

    Rect window1, window2;

    Rect canvasWindowSize;
    Rect canvasSize;
    Vector2 scrollPos = Vector2.zero;

    Color canvasColor = new Color(.35f, .35f, .35f);
    Color canvasDotColor = new Color(.7f, .7f, .7f);
    int canvasDotSpacing = 50;

    [MenuItem("Window/UI Statechart")]
    static void ShowEditor()
    {
        // Get existing open window or if none, make a new one:
        UIStatechartEditor window = (UIStatechartEditor)EditorWindow.GetWindow(typeof(UIStatechartEditor));
        window.Init();
        window.Show();
    }

    public void Init()
    {
        titleContent = new GUIContent("UI Statechart");

        window1 = new Rect(10, 10, 100, 100);
        window2 = new Rect(210, 210, 100, 100);
    }

    void OnGUI()
    {
        //if(Event.current.isScrollWheel)
        //{
        //}

        var e = Event.current;
        int border = 0;
        canvasWindowSize = new Rect(border, border, position.width - border * 2, position.height - border * 2);
        canvasSize = new Rect(0, 0, 1000, 1000);

        scrollPos = GUI.BeginScrollView(canvasWindowSize, scrollPos, canvasSize);

            EditorGUI.DrawRect(canvasSize, canvasColor);
            DrawCanvasDotGrid();

            DrawNodeCurve(window1, window2); // Here the curve is drawn under the windows

            BeginWindows();
                window1 = GUI.Window(1, window1, DrawNodeWindow, "Window 1");   // Updates the Rect's when these are dragged
                window2 = GUI.Window(2, window2, DrawNodeWindow, "Window 2");
            EndWindows();

        GUI.EndScrollView();

        if (e.isMouse && e.button == 1)
        {
            ContextMenu();
        }
    }

    void DrawCanvasDotGrid()
    {
        var _rect = new Rect(0, 0, 1, 1);

        for(int x=0; x < canvasSize.width; x+=canvasDotSpacing)
        {
            for(int y=0; y < canvasSize.height; y+=canvasDotSpacing)
            {
                _rect.x = x;
                _rect.y = y;

                EditorGUI.DrawRect(_rect, canvasDotColor);
            }
        }
    }

    void DrawNodeWindow(int id)
    {
        GUI.DragWindow();

        GUI.Button(new Rect(10, 20, 100, 20), "Can't drag me");
    }

    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + 8, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 2);
    }

    void ContextMenu()
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Add New Statechart"), false, CreateNewStatechartNode, Event.current.mousePosition);

        menu.ShowAsContext();
    }

    void CreateNewStatechartNode(object position)
    {
        window2.position = (Vector2)position;
    }
}
