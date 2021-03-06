﻿using UnityEngine;
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
						MODE_SNEAK,
						ACTION_FIRE}

	public InputCommands currentInput;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
	
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
		
		if (Input.GetKey(KeyCode.LeftControl) ||
			Input.GetKey(KeyCode.RightControl) ||
			Input.GetKey (KeyCode.DownArrow))
		{
			if (debugMode)
				Debug.Log ("Shhh! Sneaking...");
			scancode = InputCommands.MODE_SNEAK;
		}
		
		if (Input.GetKey(KeyCode.LeftShift) ||
			Input.GetKey (KeyCode.RightShift))
		{
			if(debugMode)
				Debug.Log("Running!");
			scancode = InputCommands.MODE_RUN;
		}
		
		if (Input.GetKeyUp(KeyCode.LeftShift) ||
		    Input.GetKeyUp (KeyCode.RightShift) ||
		    Input.GetKeyUp (KeyCode.LeftControl) ||
		    Input.GetKeyUp (KeyCode.RightControl) ||
		    Input.GetKeyUp (KeyCode.DownArrow))
		{
			if(debugMode)
				Debug.Log("Walking.");
			scancode = InputCommands.MODE_WALK;
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			if(debugMode)
				Debug.Log ("Firing.");
			scancode = InputCommands.ACTION_FIRE;
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
