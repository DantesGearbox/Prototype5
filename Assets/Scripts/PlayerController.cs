using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody rb;
	public LayerMask pickUpObjectsMask;
	public string pickUpObjectsTag = "PickUpObject";
	public Transform subManTransform;

	public int moveSpeed = 5;
	public int rotationSpeedPerSecond = 200;
	public int rotationSpeedPerSecondStandingStill = 200;
	public float pickupLength = 1.5f;
	private bool movingForward;
	private RaycastHit rayHit;
	private Transform pickUpTransform;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		rb.angularVelocity = Vector3.zero;

		if(Input.GetAxis ("Vertical") > 0.0f){
			rb.velocity = transform.forward * moveSpeed * Input.GetAxis ("Vertical");
		} else {
			rb.velocity = Vector3.zero;
		}

		if(Input.GetKey (KeyCode.RightArrow)){
			if(Input.GetKey (KeyCode.UpArrow)){
				transform.RotateAround (transform.position, transform.up, rotationSpeedPerSecond * Time.deltaTime);
			} else {
				transform.RotateAround (transform.position, transform.up, rotationSpeedPerSecondStandingStill * Time.deltaTime);	
			}
		}

		if(Input.GetKey (KeyCode.LeftArrow)){
			if(Input.GetKey (KeyCode.UpArrow)){
				transform.RotateAround (transform.position, transform.up, -rotationSpeedPerSecond * Time.deltaTime);
			} else {
				transform.RotateAround (transform.position, transform.up, -rotationSpeedPerSecondStandingStill * Time.deltaTime);	
			}
		}

		Debug.DrawRay (subManTransform.position, subManTransform.forward * pickupLength, Color.red);

		if(Input.GetKey (KeyCode.Space)){
			if (Physics.Raycast(subManTransform.position, subManTransform.forward, out rayHit, pickupLength, pickUpObjectsMask))
			{
				if(rayHit.collider.CompareTag (pickUpObjectsTag)){
					pickUpTransform = rayHit.transform;
				}
			}	
		} else {
			pickUpTransform = null;
		}

		if(pickUpTransform != null){
			
			pickUpTransform.position = (subManTransform.position + transform.forward * pickupLength);
		}
	}
}
