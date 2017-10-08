using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public KeyCode upButton = KeyCode.UpArrow;
	public KeyCode downButton = KeyCode.DownArrow;
	public KeyCode leftButton = KeyCode.LeftArrow;
	public KeyCode rightButton = KeyCode.RightArrow;
	public float smoothTime = 0.6f;
	private Vector3 moveVector = Vector3.zero;
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
	public bool isPickupToggleable = false; //Default is false
	public bool absoluteMovement = true; //Default is false
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

		rb.angularVelocity = Vector3.zero;

		if(!absoluteMovement){ //Relative movement
			if(Input.GetAxis ("Vertical") > 0.0f){
				rb.velocity = transform.forward * moveSpeed * Input.GetAxis ("Vertical");
			} else {
				rb.velocity = Vector3.zero;
			}

			//Steer right
			if(Input.GetKey (KeyCode.RightArrow)){
				if(Input.GetKey (KeyCode.UpArrow)){
					transform.RotateAround (transform.position, transform.up, rotationSpeedPerSecond * Time.deltaTime);
				} else {
					transform.RotateAround (transform.position, transform.up, rotationSpeedPerSecondStandingStill * Time.deltaTime);	
				}
			}

			//Steer left
			if(Input.GetKey (KeyCode.LeftArrow)){
				if(Input.GetKey (KeyCode.UpArrow)){
					transform.RotateAround (transform.position, transform.up, -rotationSpeedPerSecond * Time.deltaTime);
				} else {
					transform.RotateAround (transform.position, transform.up, -rotationSpeedPerSecondStandingStill * Time.deltaTime);	
				}
			}	
		} else { //Absolute movement

			moveVector = Vector3.zero;

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

			Vector3.SmoothDamp (vel, moveVector, ref vel, smoothTime);
			rb.velocity = vel;

            movedAmount += vel.magnitude;
            if(movedAmount > 100)
            {
                movedAmount = 0f;
                AudioSource.PlayClipAtPoint(stepClips[Random.Range(0, stepClips.Length)], transform.position);
            }
		}
			
		//Picking up crates. Two modes, toggle-carry or hold-to-carry. Options are toggled by isPickupToggleable.
		Debug.DrawRay (transform.position, transform.forward * pickupLength, Color.red);
		if(isPickupToggleable){ //Toggle carrying
			if (Input.GetKeyDown (KeyCode.Space)) {
				if(pickUpTransform != null)
                {
                    AudioSource.PlayClipAtPoint(dropdownClip, transform.position);
                    pickUpTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    pickUpTransform = null;
                } else {
					if (Physics.Raycast (transform.position, transform.forward, out rayHit, pickupLength, pickUpObjectsMask)) {
						if (rayHit.collider.CompareTag (pickUpObjectsTag))
                        {
                            AudioSource.PlayClipAtPoint(pickupClip, transform.position);
                            pickUpTransform = rayHit.transform;
						}
					}		
				}
			}
		} else { //Hold down to carry
			if (Input.GetKey (KeyCode.Space)) {
				if (Physics.Raycast (transform.position, transform.forward, out rayHit, pickupLength, pickUpObjectsMask)) {
					if (rayHit.collider.CompareTag (pickUpObjectsTag)) {
						pickUpTransform = rayHit.transform;
					}
				}	
			} else{
				pickUpTransform = null;
			}
		}

		//While we are carrying an object
		if(pickUpTransform != null){
			
			pickUpTransform.position = (transform.position + transform.forward * pickupLength + new Vector3(0,liftAmount,0));
		}        
	}
    void OnGUI()
    {
    }
}
