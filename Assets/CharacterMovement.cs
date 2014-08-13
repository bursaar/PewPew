using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float maxSpeed = 10f;
	
	public float jumpHeight = 800f;
	
	bool isGrounded = true;

	bool facingRight = true;
	
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	
	InputHandler inputHandler;
	
	public Transform groundCheck;

	void Start () {
	
	inputHandler = FindObjectOfType<InputHandler>();
	
	}
	
	void FixedUpdate () {
	
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		
		
		
	
		// float move = Input.GetAxis("Horizontal");
		
		// rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
	
		if(inputHandler.GetInput(InputHandler.InputCommands.MOVE_RIGHT) > 0 && !facingRight)
			Flip();
		else if (inputHandler.GetInput(InputHandler.InputCommands.MOVE_LEFT) < 0 && facingRight)
			Flip ();
		
	}
	
	void Update()
	{
		// This should really be handled by a proper input handler.
		if (isGrounded && inputHandler.GetInput(InputHandler.InputCommands.MOVE_JUMP))
		{
			rigidbody2D.AddForce(new Vector2(0, jumpHeight));
		}
		
		if (inputHandler.GetInput(InputHandler.InputCommands.MOVE_RIGHT))
			rigidbody2D.AddForce(new Vector2(CharacterMovement * maxSpeed, 0));
		
		if (inputHandler.GetInput(InputHandler.InputCommands.MOVE_LEFT))
			rigidbody2D.AddForce(new Vector2(CharacterMovement * maxSpeed * -1, 0));
	}
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
