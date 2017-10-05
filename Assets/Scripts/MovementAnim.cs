using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnim : MonoBehaviour
{
    Animator anims;
    Rigidbody physics;

	// Use this for initialization
	void Start () {
        physics = GetComponent<Rigidbody>();
        anims = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        anims.SetFloat("Movement", physics.velocity.magnitude / 3);
	}
}
