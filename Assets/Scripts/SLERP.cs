using UnityEngine;



public class SLERP: MonoBehaviour {
	public Vector3 start;
	public Vector3 end;

	private float progress;

	private void Update() {
		progress += Time.deltaTime / 2;
		transform.position += Vector3.Slerp(start, end, progress);// Mathf.Cos(progress * Mathf.PI * 2) * start + Mathf.Sin(progress * Mathf.PI * 2) * end;
	}
}
