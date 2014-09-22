using UnityEngine;
using System.Collections;

public class AISeeker : MonoBehaviour {

	public enum SpatialMode {	MODE_3D,
								MODE_2D	}
								
	SpatialMode myMode;
	
	
	public Vector2 Seek(Vector2 target)
	{
		Debug.Log ("The seek methods of the AI Seeker class have not yet been fully implemented are just returning unmodified arguments.");
		return target;
	}
	
	public Vector3 Seek(Vector3 target)
	{
		Debug.Log ("The seek methods of the AI Seeker class have not yet been fully implemented are just returning unmodified arguments.");
		return target;
	}
	
	
	
}
