using UnityEngine;
using System.Collections;

namespace SwingCow
{
		public class VaneAutoMovement : MonoBehaviour
		{

				public float flyingSpeed = 1.0f;
				bool isFlying = false;
				public Transform root;
				private int direction = -1;
				private bool isInRange = false;
	
				void StartFlying ()
				{
						isFlying = true;
						isInRange = true;
						PlayerStateController.onStateChange += HandleonStateChange;
				}//end method

				void HandleonStateChange (PlayerStateController.PlayerState newState, object[] exParams1, object[] exParams2)
				{
						switch (newState) {
						case PlayerStateController.PlayerState.collided:
								Destroy (gameObject, 0.1f);
								break;
						}//end switch				

				}//end method

				void Update ()
				{
		
						if (isFlying) {
								root.Translate (direction * Vector3.right * flyingSpeed * Time.deltaTime, Space.Self);
								var rootPosScreen = Camera.main.WorldToScreenPoint (root.position);
								if ((rootPosScreen.x <= Screen.width / 2 - 50)) {
										//if bird fly from right to left, change it direction
										if (direction == -1) {
												direction *= -1;
												transform.localScale = new Vector3 (-direction, 1, 1);
										}
								}//end if
								else if (rootPosScreen.x >= Screen.width / 2 + 50) {
										//if bird fly from left to right, change it direction
										if (direction == 1) {
												direction *= -1;
												transform.localScale = new Vector3 (-direction, 1, 1);
										}//end if
								}//end else
						}//end if
				}//end method
		}//end class

}//end namespace
