using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour {

	public List<TruckTiming> trucks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}

public struct TruckTiming{

	float arrival;
	float departure;
	GameObject obj;

}