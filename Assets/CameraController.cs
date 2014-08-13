using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float verticalActiveBorder = 0.1f;
	public float horizontalActiveBorder = 0.01f;
	
	public float verticalStep = 6.5f;
	public float horizontalStep = 11.6f;
	
	enum Directions {CAM_STAY, CAM_UP, CAM_DOWN, CAM_LEFT, CAM_RIGHT};
	
	Camera mainCamera;

	CharacterMovement playerController;
	Transform currentPlayerPos;

	// Use this for initialization
	void Start () {
	
		mainCamera = Camera.main;
		currentPlayerPos = FindObjectOfType<Player>().GetComponent<Transform>();
		playerController = FindObjectOfType<CharacterMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		MoveCamera(PlayerGoingOffScreen());
	}
	
	Directions PlayerGoingOffScreen()
	{
		Vector2 flattenedPlayerPosition = Vector3toVector2(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
		
		if (flattenedPlayerPosition.x > 1 - horizontalActiveBorder)
		{
			return Directions.CAM_RIGHT;
		}
		
		if (flattenedPlayerPosition.x < horizontalActiveBorder)
		{
			return Directions.CAM_LEFT;
		}
		
		if (flattenedPlayerPosition.y < verticalActiveBorder)
		{
			return Directions.CAM_UP;
		}
		
		if (flattenedPlayerPosition.y > 1 - verticalActiveBorder)
		{
			return Directions.CAM_DOWN;
		}
		
		return Directions.CAM_STAY;
		
	}
	
	void MoveCamera(Directions pMovement)
	{
		Vector3 currentPos = mainCamera.transform.position;
	
		switch(pMovement)
		{
		case Directions.CAM_STAY:
		break;
		case Directions.CAM_RIGHT:
			currentPos.x += horizontalStep;
			mainCamera.transform.position = currentPos;
			Debug.Log("Moving the camera to the right.");
		break;
		case Directions.CAM_LEFT:
			currentPos.x -= horizontalStep;
			mainCamera.transform.position = currentPos;
			Debug.Log("Moving the camera to the left.");
		break;
		case Directions.CAM_UP:
			currentPos.y -= verticalStep;
			mainCamera.transform.position = currentPos;
			Debug.Log("Moving the camera up.");
		break;
		case Directions.CAM_DOWN:
			currentPos.y += verticalStep;
			mainCamera.transform.position = currentPos;
			Debug.Log("Moving the camera down.");
		break;
		}
	}
	
	Vector2 Vector3toVector2(Vector3 pInputVector)
	{
		Vector2 outputVector; 
		
		outputVector.x = pInputVector.x;
		outputVector.y = pInputVector.y;
		
		return outputVector;
	}
}