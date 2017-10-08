using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalQuad : MonoBehaviour {

	/*
	 * Type1 = red
	 * Type2 = green
	 * Type3 = blue
	 *
	 */

	public CrateType type;
	public string pickUpObjectsTag = "PickUpObject";
    public AudioClip wrongTypeClip, correctTypeClip;

	void OnTriggerEnter(Collider other){
		if(other.CompareTag (pickUpObjectsTag)){

			Crate crate = other.GetComponent<Crate> ();

			if (crate == null)
				return;

			if(crate.type == type || type == CrateType.None){
                //Add 100 points
                AudioSource.PlayClipAtPoint(correctTypeClip, transform.position);
				GameManager.instance.AddScore (100);

			} else {
                //Subtract 50 points
                AudioSource.PlayClipAtPoint(wrongTypeClip, transform.position);
                GameManager.instance.AddScore (-50);
			}

			//Delete the crate
			Destroy (crate.gameObject);
		}
	}
}
