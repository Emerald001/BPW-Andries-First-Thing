using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;

	private float speed;
	public float runSpeed = 25;
	public float walkSpeed = 15;
	Vector3 velocity;
	public float gravity = -9.81f;
	public float jumpHeight = 1f;
	public float slowDown = 0.1f;

	public Transform GroundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	public Animator walkPlayer;

	bool isGrounded;

    void Update()
    {
		//ground check
		isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

		if(isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		//get the movement from the buttons
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = transform.right * x + transform.forward * z; //set the movement into a vector3

		controller.Move(move * speed * Time.deltaTime); //move chracter

		//jump
		if(Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
		}

		velocity.y += gravity * Time.deltaTime; //gravity on chracter

		//move jump
		controller.Move(velocity * Time.deltaTime);

		//sprint
		if(Input.GetKey(KeyCode.LeftShift)) 
		{
			speed = runSpeed;
			walkPlayer.SetBool("Walking", true);
		}
		else
		{
			speed = walkSpeed;
			walkPlayer.SetBool("Walking", false);
		}
    }
} 