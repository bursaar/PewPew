using UnityEngine;
using System.Collections;

public class Baddie : Character {

	public float verticalThreshold;
	
	public float speed = 1.0f;
	public float sensitivity = 3.0f;
	
	void OnCollisionEnter2D (Collision2D other)
	{
		
		if(other.gameObject.tag=="Projectile")
		{
			Debug.Log ("...a bullet! " + myName + " will now terminate.");
			StopProjectile(other);
		}
			  
	}
	
	
	void Start()
	{
		
	}
	
	void FixedUpdate()
	{
		Cleanup();
	}
	
	void Cleanup()
	{
		if (this.transform.position.y < verticalThreshold)
		{
			Debug.Log (myName + " fell off the world and was destroyed!");
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

		this.rigidbody2D.velocity = directionOfMovement * speed;
	}
}
