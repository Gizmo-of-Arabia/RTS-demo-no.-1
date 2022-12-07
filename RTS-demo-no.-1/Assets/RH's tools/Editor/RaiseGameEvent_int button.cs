using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


namespace RyanHipplesArchitecture.SO_Events
{
    [CustomEditor(typeof(GameEvent_int))]
    public class RaiseGameEventButton_intButton : Editor
    {
        int event_parameter = 0;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent_int gameEvent_int = target as GameEvent_int;


            GUILayout.BeginHorizontal();

            
            GUILayout.Label("Parameter for GameEvent_int:");
            event_parameter = EditorGUILayout.IntField(event_parameter);


            
            if (GUILayout.Button("Raise",GUILayout.Width(150f)))
            {
                gameEvent_int.Raise(event_parameter);
            }
            EditorGUILayout.Space(300);
            GUILayout.EndHorizontal();
        }


    }
}
