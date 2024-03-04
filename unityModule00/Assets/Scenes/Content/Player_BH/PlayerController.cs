using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
	private CharacterController controller;
	public float speed = 1.0f;
	public float jumpSpeed = 2.0f; 
	public float gravity = 3.0f;
	private Vector3 moveDirection = Vector3.zero;

	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.isGrounded) {
			moveDirection.y = 0;
			moveDirection = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			//Jumping
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}
		else {
			moveDirection.y -= gravity * Time.deltaTime;
		}
		//Making the character move
		controller.Move(moveDirection * Time.deltaTime);
	}
}
