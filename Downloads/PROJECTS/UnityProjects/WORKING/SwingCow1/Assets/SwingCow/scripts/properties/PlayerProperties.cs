using UnityEngine;
using System.Collections;

public class PlayerProperties {

	private float jump_speed = 500.0f;

	public float JumpSpeed {
		get{
			return jump_speed;
		}
		set{
			jump_speed = value;
		}
	}//end prop

}//end class
