﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (this.transform.position.y < -1)
			DestroySelf();
		
	}
	
	void DestroySelf()
	{
		Destroy(this);
	}
}