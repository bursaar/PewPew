using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int startingLifetime;
	public bool countdownActive = true;
	int lifetime;
	
	void OnTriggerEnter()
	{
		countdownActive = true;
	}

	// Use this for initialization
	void Start () {
		
		ResetLifetime();
		// countdownActive = false;
	
	}
	
	public void SetCountdown(int newCountdown)
	{
		lifetime = newCountdown;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (this.transform.position.y < -1)
			DestroySelf();
			
		if (countdownActive)
		{
			if (Countdown())
			{
				DestroySelf();
			}
		}
		
		
	}
	
	void DestroySelf()
	{
		Destroy(this.gameObject);
	}
	
	bool Countdown()
	{	
		lifetime--;
		
		if (lifetime <= 0)
		{
			ResetLifetime();
			return true;
		}
		
		return false;
	}
	
	void ResetLifetime()
	{
		lifetime = startingLifetime;
	}
}
