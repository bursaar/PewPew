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
	
	public int runningAudioTimer = 60;
	public int sneakingAudioTimer = 80;
	public AudioClip[] jump;
	public AudioClip[] run;
	public AudioClip[] sneak;
	public AudioClip[] firing;
	
	public float distanceToCheckForGround;
	RaycastHit2D groundCheckRay;
	Vector2 groundDirection;
	
	void Start () {
	
	inputHandler = FindObjectOfType<InputHandler>();
	currentSpeed = normalSpeed;
	groundDirection.x = 0f;
	groundDirection.y = -1f;
	}
	
	void FixedUpdate () {
		groundCheckRay = Physics2D.Raycast(this.transform.position, -Vector2.up, distanceToCheckForGround, whatIsGround);
		
		isGrounded = StandingOnGround();
		
		againstWall = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsWall);
	
		float move = Input.GetAxis("Horizontal");
		
		rigidbody2D.velocity = new Vector2(move * currentSpeed, rigidbody2D.velocity.y);
		
		if(!facingRight && move < 0)
			Flip();
		else 
		if (facingRight && move > 0)
			Flip ();		
	}
	
	public bool StandingOnGround()
	{
		if (groundCheckRay.point != null)
		{
			return true;
		} else {
			return false;
		}
	}
	
	void Update()
	{
		// This should really be handled by a proper input handler.
		if (isGrounded && inputHandler.GetInput(InputHandler.InputCommands.MOVE_JUMP))
		{
			audio.PlayOneShot(NextClipJump());
			rigidbody2D.AddForce(new Vector2(0, jumpHeight));
		}
		
		if(againstWall)
			Debug.Log ("Splat! Against a wall.");
			
		if(inputHandler.GetInput(InputHandler.InputCommands.MODE_RUN))
		{
			audio.PlayOneShot(NextClipRun());
			currentSpeed = runningSpeed;
		}
		
		if(inputHandler.GetInput(InputHandler.InputCommands.MODE_WALK))
		{
			currentSpeed = normalSpeed;
		}
		
		if (inputHandler.GetInput(InputHandler.InputCommands.MODE_SNEAK))
		{
			audio.PlayOneShot(NextClipSneak());
			currentSpeed = walkingSpeed;
		}
	}
	
	public void Flip()
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
	
	public AudioClip NextClipJump()
	{
		return null; //jump[Random.Range(0,jump)];
	}
	
	public AudioClip NextClipRun()
	{
		if (runningAudioTimer <= 0)
		{
			runningAudioTimer = 60;
			return null; //run[Random.Range(0,run)];
		} else {
			runningAudioTimer--;
		}
		
		return null;
	}
	
	public AudioClip NextClipSneak()
	{	
		if (sneakingAudioTimer <= 0)
		{
			sneakingAudioTimer = 90;
			return null; // sneak[Random.Range(0,sneak)];
		} else {
			sneakingAudioTimer--;
		}
		
		return null;
	}
}
