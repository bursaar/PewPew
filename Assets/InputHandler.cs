using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	public bool debugMode = false;

	public enum InputCommands {INPUT_NONE,
						MOVE_LEFT, 
						MOVE_RIGHT, 
						MOVE_UP, 
						MOVE_DOWN, 
						MOVE_JUMP, 
						MOVE_CROUCH, 
						MODE_WALK, 
						MODE_RUN, 
						MODE_SNEAK}

	public InputCommands currentInput;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	// Compare command against read input
	public bool GetInput(InputCommands inputToCheck)
	{
		InputCommands scancode = InputCommands.INPUT_NONE;
		
		// Put in checks for all applicable keys here.
		if(Input.GetKeyDown(KeyCode.Space) ||
			Input.GetKeyDown(KeyCode.UpArrow) ||
			Input.GetKeyDown(KeyCode.W))
		{
			if(debugMode)
				Debug.Log("Moving up.");
			scancode = InputCommands.MOVE_JUMP;	
		}
			
			
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if(debugMode)
				Debug.Log("Moving left.");
			scancode = InputCommands.MOVE_LEFT;
		}
			
		
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if(debugMode)
				Debug.Log("Moving right.");
			scancode = InputCommands.MOVE_RIGHT;
		}
			
			
		// Gives us a readout on the inspector
		currentInput = scancode;
		
		
		// This compares the input against the command being checked		
		if (scancode == inputToCheck)
			return true;
			
			
		// Debug.Log ("Assigning None to input controller.");
		scancode = InputCommands.INPUT_NONE;
		return false;
	}
	
}
