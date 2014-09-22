using UnityEngine;
using System.Collections;

public class Character : CharacterMovement {

	public string myName;
	public enum CharacterType {	PLAYER_ONE,
								PLAYER_TWO,
								PLAYER_THREE,
								NPC_ENEMY,
								NPC_FRIEND};
	
	public CharacterType myType;
	
}
