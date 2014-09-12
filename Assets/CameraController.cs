using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float verticalActiveBorder = 0.1f;
	public float horizontalActiveBorder = 0.01f;
	
	public float verticalStep = 6.5f;
	public float horizontalStep = 11.6f;
	
	public bool shimmyActive = false;
	public Directions directionToShimmy = Directions.CAM_STAY;
	
	public float deadBand = 0.1f;
	
	float stepSize = 0.3f;
	
	public enum Directions {CAM_STAY, CAM_UP, CAM_DOWN, CAM_LEFT, CAM_RIGHT};
	public enum CameraMode {CAM_MODE_SNAP, CAM_MODE_SHIMMY, CAM_MODE_TRACK_LOOSE, CAM_MODE_TRACK_TIGHT};
	public CameraMode cameraSetting = CameraMode.CAM_MODE_SHIMMY;
	
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
		ControlCameraMovement(cameraSetting);
	}
	
	Directions PlayerGoingOffScreen()
	{
		Vector2 flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
		
		if (flattenedPlayerPosition.x > 1 - horizontalActiveBorder)
		{
			shimmyActive = true;
			return Directions.CAM_RIGHT;
		}
		
		if (flattenedPlayerPosition.x < horizontalActiveBorder)
		{
			shimmyActive = true;
			return Directions.CAM_LEFT;
		}
		
		if (flattenedPlayerPosition.y < verticalActiveBorder)
		{
			shimmyActive = true;
			return Directions.CAM_UP;
		}
		
		if (flattenedPlayerPosition.y > 1 - verticalActiveBorder)
		{
			shimmyActive = true;
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
			if(!playerController.GetFacingRight())
			{
				currentPos.x += horizontalStep;
				mainCamera.transform.position = currentPos;
				Debug.Log("Moving the camera to the right.");
			}
			break;
		case Directions.CAM_LEFT:
			if (playerController.GetFacingRight())
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
		// Find the camera's current position
		Vector3 currentPos = mainCamera.transform.position;
		
		// Find the player's current position in the plane
		Vector2 flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
		
		
		// Perform the right translation, depending on the movement passed to the method
		switch (pMovement)
		{
		case Directions.CAM_RIGHT:
			currentPos.x += stepSize;
			mainCamera.transform.position = currentPos;
			flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
			
			if (flattenedPlayerPosition.x < horizontalActiveBorder + deadBand)
					shimmyActive = false;
			break;
		case Directions.CAM_LEFT:
			currentPos.x -= stepSize;
			mainCamera.transform.position = currentPos;
			flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));

			if (flattenedPlayerPosition.x > (1 - horizontalActiveBorder) - deadBand)
					shimmyActive = false;
			break;
		case Directions.CAM_DOWN:
			if(flattenedPlayerPosition.y > 1 - verticalActiveBorder)
			{
				currentPos.y = stepSize;
				mainCamera.transform.position = currentPos;
				flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
			}
			break;
		case Directions.CAM_UP:
			if(flattenedPlayerPosition.y < verticalActiveBorder)
			{
				currentPos.y -= stepSize;
				mainCamera.transform.position = currentPos;
				flattenedPlayerPosition = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
			}
			break;
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

	
	void ControlCameraMovement(CameraMode pCamMode)
	{
		switch(pCamMode)
		{
		case CameraMode.CAM_MODE_SHIMMY:
				if (!shimmyActive)
					directionToShimmy = PlayerGoingOffScreen();
				if (shimmyActive)
					ShimmyCameraToNewLocation(directionToShimmy);
		break;
		
		case CameraMode.CAM_MODE_SNAP:
				shimmyActive = false;
				SnapCameraToNewLocation(PlayerGoingOffScreen());
		break;
		
		case CameraMode.CAM_MODE_TRACK_LOOSE:
				shimmyActive = false;
				TrackPlayerMovement(PlayerGoingOffScreen());
		break;
		
		case CameraMode.CAM_MODE_TRACK_TIGHT:
				shimmyActive = false;
				TrackPlayerMovement();
		
		
		break;
		}
	}
	
	void TrackPlayerMovement(Directions pDirectionToFace)
	{
		// Find the camera's current position
		Vector3 currentPos = mainCamera.transform.position;
		Debug.Log ("The camera's current position is: " + currentPos.x);
		
		// Find the player's current position in the plane
		Vector2 flattenedPlayerPosition = FlattenVector(currentPlayerPos.position);
		Debug.Log ("The player's current position in the world is: " + flattenedPlayerPosition.x);
		
		// Find the player's current position in the frame
		Vector2 flattenedPlayerPositionInFrame = FlattenVector(mainCamera.WorldToViewportPoint(currentPlayerPos.position));
		Debug.Log ("The player's current position in the frame is: " + flattenedPlayerPositionInFrame.x);
	}
	
	void TrackPlayerMovement()
	{
		// Find the camera's current position
		Vector3 currentPos = mainCamera.transform.position;
		
		
		// Find the player's current position in the plane
		Vector2 flattenedPlayerPosition = FlattenVector(currentPlayerPos.position);
		
		currentPos.x = flattenedPlayerPosition.x;
		currentPos.y = flattenedPlayerPosition.y;
		
		mainCamera.transform.position = currentPos;
	}
	
}