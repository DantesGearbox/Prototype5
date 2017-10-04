#if (UNITY_EDITOR)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ArrayBuilder : MonoBehaviour
{
    [Header("Builder")]
    [Range(1, 20)]
    public int length;

    [Header("Asset")]
    public GameObject prefab;

	public bool centerObjects = false;

    public GameObject[] objs;
    private Vector3 size = Vector3.one;
    private int lastLength;
    private GameObject lastPrefab;

    // Update is called once per frame
    void Update()
    {
        if (EditorApplication.isPlaying) return;    //(frans) Don't screw anything while I play m8

        if (length == lastLength && prefab == lastPrefab)
            return;

        if(length != lastLength)
            lastLength = length;

        if (prefab != lastPrefab) 
            lastPrefab = prefab;   

        if (objs.Length > 0)
            ClearObjs();

        objs = new GameObject[length];
        for (int i = 0; i < objs.Length; i++){
			if(centerObjects)
				objs[i] = Instantiate(prefab, transform.position + (transform.right * ((0.5f + i - (objs.Length / 2f)) * 2f)), transform.rotation, transform);   //(frans) This is sort of hardcoded but whatever
			else
				objs[i] = Instantiate(prefab, transform.position + (transform.right * (.5f + i * 2f)), transform.rotation, transform); 
		}
        
    }

    void ClearObjs()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
#endif
