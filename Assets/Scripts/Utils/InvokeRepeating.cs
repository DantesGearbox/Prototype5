using UnityEngine;
using UnityEngine.Events;



public class InvokeRepeating : MonoBehaviour {
	public UnityEvent invoke;
	public float interval = 1f;
	public float startDelay = 0f;



	private void Start() {
		if(invoke != null)
			InvokeRepeating("Repeat", startDelay, interval);
	}


	private void Repeat(){
		invoke.Invoke();
	}
}
