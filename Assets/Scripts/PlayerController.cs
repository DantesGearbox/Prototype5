using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 throwVector = new Vector3 (0,5,0);
	public float timeHeldKey = 0.0f;
	private float throwRangeCap = 2.0f;
	private float throwChargeUpTime = 0.2f;
	public bool secondPickup = false;
	public bool secondLetgo = false;

	public KeyCode upButton = KeyCode.UpArrow;
	public KeyCode downButton = KeyCode.DownArrow;
	public KeyCode leftButton = KeyCode.LeftArrow;
	public KeyCode rightButton = KeyCode.RightArrow;
	public float smoothTime = 0.6f;
	private Vector3 vel = Vector3.zero;
	private float absoluteMovementRotationSpeed = 10.0f;
    private Vector3 rotationVector = Vector3.forward;

	Rigidbody rb;
	public LayerMask pickUpObjectsMask;
	public string pickUpObjectsTag = "PickUpObject";
	public float liftAmount = 1.7f;
	public int moveSpeed = 10;
	public int rotationSpeedPerSecond = 200;
	public int rotationSpeedPerSecondStandingStill = 250;
	public float pickupLength = 1.5f;
	private RaycastHit rayHit;
	private Transform pickUpTransform;
    private float movedAmount = 0;

    [Header("Sound")]
    public AudioClip pickupClip;
    public AudioClip dropdownClip;
    public AudioClip[] stepClips;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		//Character movement
		moveCharacter (getMovementVector ());

		//Picking up, carrying and throwing crates
		pickupOrThrowCrates ();
		carryCrateAround ();   
	}

	Vector3 getMovementVector(){

		//Make sure the character doesn't go spinning randomly
		rb.angularVelocity = Vector3.zero;

		Vector3 moveVector = Vector3.zero;

		if(Input.GetKey(upButton)){
			moveVector += Vector3.forward * moveSpeed;
		}
		if(Input.GetKey(downButton)){
			moveVector += -Vector3.forward * moveSpeed;
		}
		if(Input.GetKey(leftButton)){
			moveVector += -Vector3.right * moveSpeed;
		}
		if(Input.GetKey(rightButton)){
			moveVector += Vector3.right * moveSpeed;
		}

		float vectorLength = moveVector.magnitude;
		if(vectorLength != 0){
			rotationVector = new Vector3 (moveVector.x / vectorLength, moveVector.y / vectorLength, moveVector.z / vectorLength) * moveSpeed;
			moveVector = rotationVector;

			float step = absoluteMovementRotationSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, moveVector, step, 0.0f);
			transform.rotation = Quaternion.LookRotation (newDir);		
		}

		return moveVector;
	}

	void moveCharacter(Vector3 moveVector){
		Vector3.SmoothDamp (vel, moveVector, ref vel, smoothTime);
		rb.velocity = vel;

		movedAmount += vel.magnitude;
		if(movedAmount > 100)
		{
			movedAmount = 0f;
			AudioSource.PlayClipAtPoint(stepClips[Random.Range(0, stepClips.Length)], transform.position);
		}
	}

	RaycastHit isACratePickupAble(){
		for(float i = -0.5f; i < 1.0f ; i+= 0.5f){
			Debug.DrawRay (transform.position + transform.right * i + transform.up * 0.5f, transform.forward * pickupLength, Color.red);
			if (Physics.Raycast (transform.position + transform.right * i + transform.up * 0.5f, transform.forward, out rayHit, pickupLength, pickUpObjectsMask)) {

				return rayHit;
			}	
		}
		return rayHit;
	}

	void pickupOrThrowCrates(){
		RaycastHit hit = isACratePickupAble ();

		timeHeldKey += Time.deltaTime;
		if(timeHeldKey > throwRangeCap){
			timeHeldKey = throwRangeCap;
		}

		if(Input.GetKeyUp (KeyCode.Space)){

			if(secondLetgo){	//Throw or drop
				secondLetgo = false;
				if(timeHeldKey > throwChargeUpTime){
					AudioSource.PlayClipAtPoint(dropdownClip, transform.position);
					pickUpTransform.GetComponent<Rigidbody> ().velocity = (transform.forward * timeHeldKey * 5) + throwVector;
					pickUpTransform = null;
				} else {
					AudioSource.PlayClipAtPoint (dropdownClip, transform.position);
					pickUpTransform.GetComponent<Rigidbody> ().velocity = Vector3.zero;
					pickUpTransform = null;
				}
			} else {			//Throw or do nothing
				if(timeHeldKey > throwChargeUpTime){ //Throw or pickup
					AudioSource.PlayClipAtPoint(dropdownClip, transform.position);
					pickUpTransform.GetComponent<Rigidbody> ().velocity = (transform.forward * timeHeldKey * 5) + throwVector;
					pickUpTransform = null;
					secondPickup = false;
				} else {
					secondLetgo = true;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) { //Pickup crate
			if(secondPickup){
				timeHeldKey = 0.0f;
				secondPickup = false;
			} else { 
				if (hit.collider != null) {
					if (hit.collider.CompareTag (pickUpObjectsTag)) {
						AudioSource.PlayClipAtPoint(pickupClip, transform.position);
						pickUpTransform = hit.transform;

						secondPickup = true;
						timeHeldKey = 0.0f;
					}
				} 
			}
		}
	}

	void carryCrateAround(){
		if(pickUpTransform != null){
			pickUpTransform.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			pickUpTransform.position = (transform.position + transform.forward * pickupLength + new Vector3(0,liftAmount,0));
		}
	}
}
