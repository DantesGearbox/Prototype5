using UnityEngine;



[RequireComponent(typeof(BoxCollider))]
public class Conveyor : MonoBehaviour {
	[SerializeField] protected float speed = 2.0f;
	[Tooltip("Whether the conveyor is on (true) or not (false)")]
	public bool isActive = true;	 

	protected Vector3 _direction = new Vector3(1, 0, 0);
	protected BoxCollider _collider;
	protected Transform _transform;



	void Start(){
		_collider = GetComponent<BoxCollider>();
		_collider.isTrigger = true;

		_transform = transform;

		_direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * _transform.rotation.eulerAngles.y), Mathf.Sin(Mathf.Deg2Rad * _transform.localRotation.eulerAngles.z), -Mathf.Sin(Mathf.Deg2Rad * _transform.rotation.eulerAngles.y));
	}


	public virtual Vector3 GetForce(Vector3 position){
		if(isActive)
			return _direction * speed * Time.deltaTime;
		else
			return Vector3.zero;
	}


	public void RotateRight(){
		Rotate(-90);
	}


	public void RotateLeft(){
		Rotate(90);
	}

	
	// #TODO Z rotation is also working!!
	public virtual void Rotate(float angle) {
		_transform.Rotate(Vector3.up, angle);

		float degToRad = Mathf.Deg2Rad;
		_direction = new Vector3(Mathf.Cos(degToRad * _transform.rotation.eulerAngles.y), Mathf.Sin(degToRad * _transform.localRotation.eulerAngles.z), -Mathf.Sin(degToRad * _transform.rotation.eulerAngles.y));
	}


	private void OnTriggerStay(Collider other) {
		ConveyorObject conveyorObject = other.GetComponent<ConveyorObject>();

		if(conveyorObject)
			conveyorObject.SetConveyor(this);
	}


	private void OnTriggerExit(Collider other) {
		ConveyorObject conveyorObject = other.GetComponent<ConveyorObject>();

		if(conveyorObject)
			conveyorObject.LeaveConveyor(this);
	}


	private void OnDrawGizmos() {
		_direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y), Mathf.Sin(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z), -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y));
		Debug.DrawLine(transform.position, transform.position + _direction * 1, Color.cyan);
	}
}
