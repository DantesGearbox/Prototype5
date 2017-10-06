using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalQuad : MonoBehaviour {

	public CrateType type;
	public string pickUpObjectsTag = "PickUpObject";

	void OnTriggerEnter(Collider other){
		if(other.CompareTag (pickUpObjectsTag)){

			Crate crate = other.GetComponent<Crate> ();

			if (crate == null)
				return;

			if(crate.type == type){
				//Add 100 points
				GameManager.instance.AddScore (100);
			} else {
				//Subtract 50 points
				GameManager.instance.AddScore (-50);
			}

			Debug.Log (GameManager.instance.GetScore ());

			//Delete the crate
			Destroy (crate.gameObject);
		}
	}
}
