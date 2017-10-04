using UnityEngine;



public class ConveyorObject : MonoBehaviour {
	public enum MoveMode{ transform, rigidbody}
	public MoveMode moveMode = MoveMode.transform;

	private Conveyor _currentConveyor = null;
	private Transform _transform;
	private Rigidbody _rigidbody;



	private void Start() {
		_transform = transform;
		_rigidbody = GetComponent<Rigidbody>();
	}


	public void SetConveyor(Conveyor conveyor){
		if(_currentConveyor == null)
			_currentConveyor = conveyor;
	}


	public void LeaveConveyor(Conveyor conveyor){
		if(_currentConveyor == conveyor)
			_currentConveyor = null;
	}


	public bool HasConeyor(){
		return _currentConveyor != null;
	}


	public void Update() {
		if(!_currentConveyor){
			return;
		}

		if(moveMode == MoveMode.rigidbody)
			_rigidbody.AddForce(_currentConveyor.GetForce(transform.position) * 5, ForceMode.VelocityChange);
		else if(moveMode == MoveMode.transform)
			_transform.position += _currentConveyor.GetForce(transform.position);
	}
}
