using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TruckManager))]
public class TruckManagerEditor : Editor
{
    List<TruckTiming> currentTimes;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TruckManager manager = (TruckManager)target;

        List<TruckTiming> currentTimes = manager.truckTimings;
        for(int i = 0; i < manager.truckTimings.Count;i++)
        {
            TruckTiming t = currentTimes[i];
            EditorGUILayout.BeginHorizontal();
            { 
                EditorGUILayout.LabelField("Timing " + i);
                if (GUILayout.Button("Delete"))
                {
                    currentTimes.RemoveAt(i);
                    break;
                }
            }
            EditorGUILayout.EndHorizontal();
            {
                t.truck = (GameObject)EditorGUILayout.ObjectField("Truck: ", t.truck, typeof(GameObject), true, GUILayout.ExpandWidth(true));
                t.door = (GameObject)EditorGUILayout.ObjectField("Door: ", t.door, typeof(GameObject), true, GUILayout.ExpandWidth(true));
                EditorGUILayout.BeginHorizontal();
                t.arrival = EditorGUILayout.FloatField("Arrival (sec): ", t.arrival);
                t.departure = EditorGUILayout.FloatField("Departure (sec): ", t.departure);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            currentTimes[i] = t;
        }

        EditorGUILayout.BeginHorizontal();
        { 
            if (GUILayout.Button("New"))
            {
                TruckTiming t = new TruckTiming();
                t.arrival = 0;
                t.departure = 180;
                t.truck = null;
                t.door = null;
                currentTimes.Add(t);
            }
        }
        EditorGUILayout.EndHorizontal();

        manager.truckTimings = currentTimes;
    }
}