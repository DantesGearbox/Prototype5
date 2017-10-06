using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TruckManager))]
public class TruckManagerEditor : Editor
{
    private List<TruckTiming> newTimes;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TruckManager manager = (TruckManager)target;

        newTimes = new List<TruckTiming>();
        for(int i = 0; i < manager.trucks.Count;i++)
        {
            TruckTiming t = new TruckTiming();
            EditorGUILayout.LabelField("Truck " + i);
            t.obj = (GameObject)EditorGUILayout.ObjectField("Truck Object: ", manager.trucks[i].obj, typeof(GameObject));
            EditorGUILayout.BeginHorizontal();
            t.arrival = EditorGUILayout.FloatField("Arrival (sec): ", manager.trucks[i].arrival);
            t.departure = EditorGUILayout.FloatField("Departure (sec): ", manager.trucks[i].departure);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();
            newTimes.Add(t);
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New"))
        {
            TruckTiming t = new TruckTiming();
            t.arrival = 0;
            t.departure = 50;
            t.obj = null;
            newTimes.Add(t);
        }

        if(GUILayout.Button("Delete Last"))
        {
            newTimes.RemoveAt(newTimes.Count - 1);
        }
        EditorGUILayout.EndHorizontal();

        manager.trucks = newTimes;
    }
}