using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float verticalActiveBorder = 0.1f;
	public float horizontalActiveBorder = 0.01f;
	
	public float verticalStep = 6.5f;
	public float horizontalStep = 11.6f;
	
	public int numberOfFramesToShimmy = 1000;
	int shimmyFrameCount;
	
	float stepSize = 0.3f;
	
	enum Directions {CAM_STAY, CAM_UP, CAM_DOWN, CAM_LEFT, CAM_RIGHT};
	
	Camera mainCamera;

	CharacterMovement playerController;
	Transform currentPlayerPos;

	// Use this for initialization
	void Start () {
	
		mainCamera = Camera.main;
		currentPlayerPos = FindObjectOfType<Player>().GetComponent<Transform>();
		playerController = FindObjectOfType<CharacterMovement>();
		ResetFramesToShimmy();
	}
	
	// Update is called once per frame
	void Update () {
		
		// stepSize = 0.01f;
		ShimmyCameraToNewLocation(PlayerGoingOffScreen());
	}
	
	Directions PlayerGoingOffScreen()
	{
		Vector2 flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
		
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
	
	void SnapCameraToNewLocation(Directions pMovement)
	{
		Vector3 currentPos = mainCamera.transform.position;
	
		switch(pMovement)
		{
		case Directions.CAM_STAY:
			break;
		case Directions.CAM_RIGHT:
			if(playerController.GetFacingRight())
			{
				currentPos.x += horizontalStep;
				mainCamera.transform.position = currentPos;
				Debug.Log("Moving the camera to the right.");
			}
			break;
		case Directions.CAM_LEFT:
			if (!playerController.GetFacingRight())
			{
				currentPos.x -= horizontalStep;
				mainCamera.transform.position = currentPos;
				Debug.Log("Moving the camera to the left.");
			}
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
	
	void ShimmyCameraToNewLocation(Directions pMovement)
	{
		
		Vector3 currentPos = mainCamera.transform.position;
		Vector2 flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
		
		switch (pMovement)
		{
		case Directions.CAM_RIGHT:
			Debug.Log ("Flattened player position is " + flattenedPlayerPosition.x);
			if(flattenedPlayerPosition.x > horizontalActiveBorder && shimmyFrameCount > 0)
			{
				Debug.Log ("Shimmying right.");
				currentPos.x += stepSize;
				mainCamera.transform.position = currentPos;
				flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
				shimmyFrameCount--;
			}
			break;
		case Directions.CAM_LEFT:
			if(flattenedPlayerPosition.x < 1 - horizontalActiveBorder && shimmyFrameCount > 0)
			{
				currentPos.x -= stepSize;
				mainCamera.transform.position = currentPos;
				flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
				shimmyFrameCount--;
			}
			break;
		case Directions.CAM_DOWN:
			if(flattenedPlayerPosition.y < verticalActiveBorder && shimmyFrameCount > 0)
			{
				currentPos.y += stepSize;
				mainCamera.transform.position = currentPos;
				flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
				shimmyFrameCount--;
			}
			break;
		case Directions.CAM_UP:
			if(flattenedPlayerPosition.y > 1 - verticalActiveBorder && shimmyFrameCount > 0)
			{
				currentPos.y -= stepSize;
				mainCamera.transform.position = currentPos;
				flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
				shimmyFrameCount--;
			}
			break;
		}
		
		if (shimmyFrameCount == 0)
		{
			pMovement = Directions.CAM_STAY;
			ResetFramesToShimmy();
		}
		
	}
	
	/// <summary>
	/// 3D to 2D – discards the Z co-ordinate of a 3D vector
	/// </summary>
	/// <returns>The flattened Vector2 data with the same x and y as the input vector.</returns>
	/// <param name="pInputVector">The 3D vector to be flattened.</param>
	Vector2 FlattenVector(Vector3 pInputVector)
	{
		Vector2 outputVector; 
		
		outputVector.x = pInputVector.x;
		outputVector.y = pInputVector.y;
		
		return outputVector;
	}
	
	void ResetFramesToShimmy()
	{
		shimmyFrameCount = numberOfFramesToShimmy;
	}
}