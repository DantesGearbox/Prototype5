using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour {

	public GameObject cratePrefab;
	public int startingCratesAmount;
	public int timeCounter = 0;
	public List<GameObject> objectsInTruck;
	public Bounds bounds;

	void OnTriggerEnter(Collider other){
		objectsInTruck.Add (other.gameObject);
	}

	void OnTriggerExit(Collider other){
		objectsInTruck.Remove (other.gameObject);
	}

	void OnDisable(){

		for (int i = objectsInTruck.Count-1; i >= 0; i--) {
			GameObject obj = objectsInTruck [i];

			if (obj.CompareTag ("Player")) {

				Vector3 newPosition = new Vector3 (transform.position.x, obj.transform.position.y, transform.position.z);
				obj.transform.position = (-transform.right * 5) + newPosition;

				//Teleport out


			} else if (obj.CompareTag ("PickUpObject")) {
				Destroy (obj);
				GameManager.instance.AddScore (-50);
			}
		}
	}

	void OnEnable(){
		bounds = GetComponent<BoxCollider> ().bounds;
		objectsInTruck = new List<GameObject> ();
		
		//Spawn new crates
		for(int i = 0; i < startingCratesAmount; i++){
			//1.4 is a magic number that makes the crates stand on the floor
			Vector3 pos = new Vector3 (Random.Range (bounds.min.x, bounds.max.x), bounds.center.y-1.4f, Random.Range (bounds.min.z, bounds.max.z)); 
			Debug.Log (bounds.extents);

			Instantiate (cratePrefab, pos, Quaternion.identity);	
		}
	}
}
