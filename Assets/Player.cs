using UnityEngine;
using System.Collections;

public class Player : CharacterMovement {

	public Rigidbody2D bullet;
	CharacterMovement thisCharacter;
	InputHandler thisInput;
	public Transform gun;
	public Transform bulletParent;
	public AudioClip[] firing;
	
	public float bulletSpeed;

	// Use this for initialization
	void Start () {
		thisCharacter = GetComponent<CharacterMovement>();
		thisInput = FindObjectOfType<InputHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (thisInput.GetInput(InputHandler.InputCommands.ACTION_FIRE))
		{
			FireWeapon();
			audio.PlayOneShot(NextClipFiring());
		}
			
	}
	
	public void FireWeapon()
	{
		Rigidbody2D newBullet = (Rigidbody2D)Instantiate(bullet, gun.transform.position, Quaternion.identity);
		newBullet.transform.SetParent(bulletParent.transform);
		
		if (thisCharacter.GetFacingRight())
		{
			Vector3 oldScale = newBullet.transform.localScale;
			oldScale.x *= -1;
			newBullet.transform.localScale = oldScale;
			Vector2 lateralForceRight;
			lateralForceRight.x = -1;
			lateralForceRight.y = 0;
			newBullet.velocity = lateralForceRight * bulletSpeed;
		} else {
			Vector2 lateralForceLeft;
			lateralForceLeft.x = 1;
			lateralForceLeft.y = 0;
			newBullet.velocity = lateralForceLeft * bulletSpeed;
		}
	}
	
	AudioClip NextClipFiring()
	{
		return firing[Random.Range(0,14)];
	}
	
}
