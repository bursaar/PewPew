using UnityEngine;
using System.Collections;

public class Baddie : MonoBehaviour {

	public string baddieName;
	public float verticalThreshold;
	public float speed = 1.0f;
	public float sensitivity = 3.0f;
	public bool baddieFacingRight = true;
	public enum Directions {DIRECTION_UP, 
						DIRECTION_FORWARD, 
						DIRECTION_DOWN, 
						DIRECTION_BACK, 
						DIRECTION_FORWARD_AND_DOWN};
	public Directions directionsToCheck = Directions.DIRECTION_FORWARD;
	public bool flipOnDetection = true;
	public enum BaddieMode {MODE_PATROL};
	public BaddieMode myMode;
	public int initialDetectionFilter = 3;
	int detectionFilter;
	Ray ray;
	
	public LayerMask layersToCollideWith;
	
	void OnCollisionEnter2D (Collision2D other)
	{
		
		if(other.gameObject.tag=="Projectile")
		{
			Debug.Log ("...a bullet! " + baddieName + " will now terminate.");
			StopProjectile(other);
		}
			  
	}
	
	void Start()
	{
		detectionFilter = initialDetectionFilter;
	}
	
	void FixedUpdate()
	{
		Cleanup();
		switch(myMode)
		{
			case BaddieMode.MODE_PATROL:
			Move (baddieFacingRight);
			break;
		}
		
	}
	
	void Cleanup()
	{
		if (this.transform.position.y < verticalThreshold)
		{
			Debug.Log (baddieName + " fell off the world and was destroyed!");
			Die ();
		}
	}
	
	void Die()
	{
		Destroy(this.gameObject);
	}
	
	void StopProjectile(Collision2D projectile)
	{
		Vector2 stopped;
		stopped.x = 0f;
		stopped.y = 0f;
		projectile.gameObject.rigidbody2D.velocity = (stopped);
		projectile.gameObject.GetComponent<Bullet>().SetCountdown(5);
		Die ();
	}
	
	bool CheckForCollision(Directions directionToCheck)
	{
		// Set up a default value
		Vector2 direction2D = this.transform.position;
		
		// Assign value based on argument
		switch(directionToCheck){
		case Directions.DIRECTION_UP:
		direction2D = Vector2.up;
		break;
		
		case Directions.DIRECTION_FORWARD:
		direction2D = Vector2.right;
		break;
		
		case Directions.DIRECTION_DOWN:
		direction2D = -Vector2.up;
		break;
		
		case Directions.DIRECTION_BACK:
		direction2D = -Vector2.right;
		break;
		
		case Directions.DIRECTION_FORWARD_AND_DOWN:
		direction2D = Vector2.right - Vector2.up;	// Combine the right vector (1, 0) with the down vector (0, -1) with addition (1, -1)
		break;
		}
		
		// Establish a scale for the raycasting the adjusts with velocity.
		float horizontalDistance = this.rigidbody2D.velocity.x;
		float verticalDistance = this.rigidbody2D.velocity.y;
		
		// Set up a useful default value for the RaycastHit2D variable based on vertical distance.
		RaycastHit2D hitInformation2D = Physics2D.Raycast(transform.position, direction2D, verticalDistance * sensitivity, layersToCollideWith);
		
		// Reassign IF the direction is forward or backwards (or forwards & down) instead.
		if (directionToCheck == Directions.DIRECTION_FORWARD
		|| directionToCheck == Directions.DIRECTION_BACK
		|| directionToCheck == Directions.DIRECTION_FORWARD_AND_DOWN)
		{// the variable     =   the raycast of this position, the direction we're looking in, at a distance of the speed by the sensitivity, only checking the layers in the list.
			hitInformation2D = Physics2D.Raycast(transform.position, direction2D, horizontalDistance * sensitivity, layersToCollideWith);
		}		
		
		// If we get a result
		if(hitInformation2D.collider != null)
		{
			// AND if the detection filter is greater than zero
			if (detectionFilter > 0)
			{
				// Decrement the filter and return No Dice
				detectionFilter--;
				return false;
			} else {
				// Reset the filter
				detectionFilter = initialDetectionFilter;
				
				// Report a hit
				Debug.Log (baddieName + " hit " + layersToCollideWith.ToString());
				return true;
			}
		}
		
		return false;
		
	}
	
	void Flip()
	{
		Vector3 thisScale = this.transform.localScale;
		thisScale.x *= -1;
		this.transform.localScale = thisScale;
	}
	
	void Move(bool facingRight)
	{
		Vector2 directionOfMovement;
		
		Vector2 leftVector;
		Vector2 rightVector;
		leftVector.x = -1f;
		leftVector.y = 0.0f;
		rightVector = leftVector * -1f;
		
		if (facingRight)
		{
			directionOfMovement = rightVector;
		} else {
			directionOfMovement = leftVector;
		}
		
		if (flipOnDetection)
		{
			if (CheckForCollision(directionsToCheck))
			{
				Flip();
				baddieFacingRight = !baddieFacingRight;
			}
		} else {
			if (!CheckForCollision(directionsToCheck))
			{
				Flip();
				baddieFacingRight = !baddieFacingRight;
			}
		}

		
		this.rigidbody2D.velocity = directionOfMovement * speed;
	}
}
