#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

[InitializeOnLoad]
public static class FullscreenGameView
{
    static bool isFullscreen = false;
    static EditorWindow gameView;
    static Rect prevPosition;

    static FullscreenGameView()
    {
        EditorApplication.playModeStateChanged += OnPlaymodeChanged;
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    static void OnPlaymodeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
            EnterFullscreen();

        if (state == PlayModeStateChange.ExitingPlayMode)
            ExitFullscreen();
    }

    static void DuringSceneGUI(SceneView view)
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.F12)
        {
            ToggleFullscreen();
            e.Use();
        }
    }

    static EditorWindow GetGameView()
    {
        var type = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        var windows = Resources.FindObjectsOfTypeAll(type);
        return windows.Length > 0 ? (EditorWindow)windows[0] : null;
    }

    static void ToggleFullscreen()
    {
        if (isFullscreen)
            ExitFullscreen();
        else
            EnterFullscreen();
    }

    static void EnterFullscreen()
    {
        if (isFullscreen) return;

        gameView = GetGameView();
        if (gameView == null) return;

        prevPosition = gameView.position;

        gameView.position = new Rect(
            0,
            0,
            Screen.currentResolution.width,
            Screen.currentResolution.height
        );

        isFullscreen = true;
    }

    static void ExitFullscreen()
    {
        if (!isFullscreen || gameView == null) return;

        gameView.position = prevPosition;
        isFullscreen = false;
    }
}
#endif
