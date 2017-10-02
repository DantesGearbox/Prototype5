using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexUV : MonoBehaviour {
    public Direction moveDirection;
    public float moveSpeed;

    private MeshRenderer mesh;

    private Vector2 offset;

	// Use this for initialization
	void Start ()
    {
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 vec = Vector2.zero;

        if (moveDirection == Direction.Up)
            vec = Vector2.up * moveSpeed * Time.deltaTime;
        else if(moveDirection == Direction.Down)
            vec = Vector2.down * moveSpeed * Time.deltaTime;
        else if(moveDirection == Direction.Left)
            vec = Vector2.left * moveSpeed * Time.deltaTime;
        else if(moveDirection == Direction.Right)
            vec = Vector2.right * moveSpeed * Time.deltaTime;
        else
            Debug.Log("Invalid Move Direction");

        offset += vec;

        if (offset.x > 1 || offset.x < -1)
            offset.x = 0;
        if (offset.y > 1 || offset.y < -1)
            offset.y = 0;

        mesh.material.mainTextureOffset = offset;
	}
}

public enum Direction
{
    Up,
    Left,
    Right,
    Down
}
