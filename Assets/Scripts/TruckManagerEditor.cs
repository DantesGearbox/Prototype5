using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TruckManager))]
public class TruckManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TruckManager manager = (TruckManager)target;

        List<TruckTiming> currentTimes = manager.trucks;
        for(int i = 0; i < manager.trucks.Count;i++)
        {
            TruckTiming t = new TruckTiming();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Truck " + i);
            if (GUILayout.Button("Delete"))
            {
                currentTimes.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndHorizontal();
            t.obj = (GameObject)EditorGUILayout.ObjectField("Truck Object: ", manager.trucks[i].obj, typeof(GameObject));
            EditorGUILayout.BeginHorizontal();
            t.arrival = EditorGUILayout.FloatField("Arrival (sec): ", manager.trucks[i].arrival);
            t.departure = EditorGUILayout.FloatField("Departure (sec): ", manager.trucks[i].departure);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New"))
        {
            TruckTiming t = new TruckTiming();
            t.arrival = 0;
            t.departure = 50;
            t.obj = null;
            currentTimes.Add(t);
        }
        EditorGUILayout.EndHorizontal();

        manager.trucks = currentTimes;
    }
}