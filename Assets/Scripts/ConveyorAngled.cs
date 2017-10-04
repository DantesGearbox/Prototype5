using UnityEngine;


public class ConveyorAngled : Conveyor {
	public const float MAX_DISTANCE_TO_INNER_CORNER = 2f;
	[SerializeField]private Transform innerCorner;
	private Vector3 _directionIn = new Vector3(0, 0, 1);




	public override Vector3 GetForce(Vector3 position){
		if(!innerCorner){
			Debug.LogError("Cannot find inner corner");
			return Vector3.zero;
		}

		if(!isActive)
			return Vector3.zero;

		float degToRad = Mathf.Deg2Rad;

		//float rotationalSpeed = Vector3.Distance(new Vector3(innerCorner.position.x, 0, innerCorner.position.z), new Vector3(position.x, 0, position.y)) / MAX_DISTANCE_TO_INNER_CORNER;
		//if(position.z <= innerCorner.position.z)
		float cos = Mathf.Cos(degToRad * _transform.rotation.eulerAngles.y);
		float sin = Mathf.Sin(degToRad * _transform.rotation.eulerAngles.y);

		if(_transform.rotation.eulerAngles.y < 0){
		 // PROPER
			if(cos * position.z + sin * position.x <= cos * innerCorner.position.z + sin * position.x)
				return _directionIn * speed * Time.deltaTime;

			if(cos * position.x + sin * position.z >= cos * innerCorner.position.x + sin * innerCorner.position.z)
				return _direction * speed * Time.deltaTime;
		} else{
			if(cos * position.z + sin * position.x >= cos * innerCorner.position.z + sin * position.x)
				return _directionIn * speed * Time.deltaTime;

			if(cos * position.x + sin * position.z <= cos * innerCorner.position.x + sin * innerCorner.position.z)
				return _direction * speed * Time.deltaTime;
		}

		Vector3 delta = position - innerCorner.position;
		delta.y = 0;
		float progress = Mathf.Atan2(Mathf.Abs(delta.x), Mathf.Abs(delta.z)) / (Mathf.PI * 0.5f);

		return Vector3.Slerp(_direction, _directionIn, progress) * speed * Time.deltaTime;
	}


	public override void Rotate(float angle) {
		base.Rotate(angle);
		
		float degToRad = Mathf.Deg2Rad;
		_directionIn = new Vector3(Mathf.Sin(degToRad * _transform.rotation.eulerAngles.y), Mathf.Sin(degToRad * _transform.localRotation.eulerAngles.z), -Mathf.Cos(degToRad * _transform.rotation.eulerAngles.y));
	}
}
