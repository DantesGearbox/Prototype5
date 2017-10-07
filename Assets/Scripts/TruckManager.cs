using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour {

    [HideInInspector]
    public List<TruckTiming> truckTimings = new List<TruckTiming>();

    private float startTimestamp;

	// Use this for initialization
	void Start () {
        startTimestamp = Time.time;

        for(int i = 0; i < truckTimings.Count;i++)
        {
            truckTimings[i].truck.SetActive(false);
            truckTimings[i].door.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i < truckTimings.Count;i++)
        {
            if (HasArrived(truckTimings[i]) && truckTimings[i].truck.activeSelf != true)
            {
                truckTimings[i].truck.SetActive(true);
                truckTimings[i].door.SetActive(false);
            }
            else if (!truckTimings[i].hasDeparted && HasDepartured(truckTimings[i]) && truckTimings[i].truck.activeSelf == true)
            {
                truckTimings[i].hasDeparted = true;
                truckTimings[i].truck.SetActive(false);
                truckTimings[i].door.SetActive(true);
            }
        }
	}

    bool HasArrived(TruckTiming time)
    {
        return GetPassedTime() > time.arrival && GetPassedTime() < time.departure;
    }

    bool HasDepartured(TruckTiming time)
    {
        return GetPassedTime() > time.departure;
    }

    float GetPassedTime()
    {
        return Time.time - startTimestamp;
    }
		
}
[Serializable]
public class TruckTiming{

	public float arrival;
	public float departure;
    public bool hasDeparted;
	public GameObject truck;
    public GameObject door;
}