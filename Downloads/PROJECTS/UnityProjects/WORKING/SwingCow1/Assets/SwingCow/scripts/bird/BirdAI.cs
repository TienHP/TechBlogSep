using UnityEngine;
using System.Collections;

public class BirdAI : MonoBehaviour {

	public float flyingSpeed = 1.0f;
	bool isFlying = false;
	public Transform root;
	private int direction = -1;
	private bool isInRange = false;

	void StartFlying(){
		isFlying = true;
		isInRange = true;
	}//end method

	void Update(){

		if (isFlying){
			root.Translate(direction * Vector3.right * flyingSpeed * Time.deltaTime, Space.Self);
			var rootPosScreen = Camera.main.WorldToScreenPoint(root.position);
			if ((rootPosScreen.x <= -50)){
				//if bird fly from right to left, change it direction
				if (direction == -1){
					direction *= -1;
					transform.localScale = new Vector3(-direction, 1, 1);
				}
			}//end if
			else if(rootPosScreen.x >= Screen.width + 50){
				//if bird fly from left to right, change it direction
				if (direction == 1){
					direction *= -1;
					transform.localScale = new Vector3(-direction, 1, 1);
				}
			}
		}//end if
	}//end method
}//end method
