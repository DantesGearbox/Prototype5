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
                EditorGUILayout.LabelField("Truck " + i);
                if (GUILayout.Button("Delete"))
                {
                    currentTimes.RemoveAt(i);
                    break;
                }
            }
            EditorGUILayout.EndHorizontal();
            { 
                t.truck = (GameObject)EditorGUILayout.ObjectField("Truck Object: ", t.truck, typeof(GameObject), true);
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
                t.departure = 50;
                t.truck = null;
                currentTimes.Add(t);
            }
        }
        EditorGUILayout.EndHorizontal();

        manager.truckTimings = currentTimes;
    }
}