using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float normalSpeed = 10f;
	public float runningSpeed = 15f;
	public float walkingSpeed = 5f;
	
	float currentSpeed;
	
	public float jumpHeight = 800f;
	
	bool isGrounded = true;
	bool againstWall = false;

	bool facingRight = true;
	
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	public LayerMask whatIsWall;
	
	InputHandler inputHandler;
	
	public Transform groundCheck;

	void Start () {
	
	inputHandler = FindObjectOfType<InputHandler>();
	currentSpeed = normalSpeed;
	
	}
	
	void FixedUpdate () {
	
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		
		againstWall = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsWall);
		
	
		float move = Input.GetAxis("Horizontal");
		
		rigidbody2D.velocity = new Vector2(move * currentSpeed, rigidbody2D.velocity.y);
	
		if(inputHandler.GetInput(InputHandler.InputCommands.MOVE_RIGHT) && !facingRight)
			Flip();
		else if (inputHandler.GetInput(InputHandler.InputCommands.MOVE_LEFT) && facingRight)
			Flip ();		
	}
	
	void Update()
	{
		// This should really be handled by a proper input handler.
		if (isGrounded && inputHandler.GetInput(InputHandler.InputCommands.MOVE_JUMP))
		{
			rigidbody2D.AddForce(new Vector2(0, jumpHeight));
		}
		
		if(againstWall)
			Debug.Log ("Splat! Against a wall.");
			
		if(inputHandler.GetInput(InputHandler.InputCommands.MODE_RUN))
		{
			currentSpeed = runningSpeed;
		}
		
		if(inputHandler.GetInput(InputHandler.InputCommands.MODE_WALK))
		{
			currentSpeed = normalSpeed;
		}
		
		if (inputHandler.GetInput(InputHandler.InputCommands.MODE_SNEAK))
		{
			currentSpeed = walkingSpeed;
		}
		
	}
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public bool GetFacingRight()
	{
		if (facingRight)
			Debug.Log("Player is facing right.");
		if (!facingRight)
			Debug.Log("Player is facing left.");
		return facingRight;
	}
	
	public void SetFacing(bool pIsFacingRight)
	{
		facingRight = pIsFacingRight;
	}
}
