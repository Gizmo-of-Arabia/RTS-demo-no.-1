// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace RyanHipplesArchitecture.SO_Events
{
    [CustomEditor(typeof(GameEvent))]
    public class RaiseGameEventButton : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GUILayout.BeginHorizontal();
            EditorGUILayout.Space(20);

            GameEvent gameEvent = target as GameEvent;
            if (GUILayout.Button("Raise"))
            {
                gameEvent.Raise();
            }
            EditorGUILayout.Space(20);
            GUILayout.EndHorizontal();
        }


    }
}
