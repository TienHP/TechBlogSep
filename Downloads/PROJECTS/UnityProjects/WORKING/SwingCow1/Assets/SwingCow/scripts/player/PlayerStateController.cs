using UnityEngine;
using System.Collections;

namespace SwingCow
{
		public class PlayerStateController : MonoBehaviour
		{
				public enum PlayerState
				{
						idle = 0,//normally, do nothing
						jump_funny = 1, // jump funny
						jump = 2, // jump when tapping
						collided = 3, // collided with something
						taking_vane = 4
				}//end defining

				void Start ()
				{
						onStateChange += HandleOnStateChange;	
						EasyTouch.On_SimpleTap += HandleOn_SimpleTap;
						Debug.Log("On Start");
				}//end method
				
				void OnDisable ()
				{
						EasyTouch.On_SimpleTap -= HandleOn_SimpleTap;
				}//end method
				
				void HandleOn_SimpleTap (Gesture gesture)
				{
						if (currentState == PlayerState.idle)
							FireOnStateChangeEvt (PlayerState.jump, null, null);
				}//end method

				void HandleOnStateChange (PlayerState newState, System.Object[] exParams1, object[] exParams2)
				{
						if (newState != currentState) {
								currentState = newState;
						}//end if
				}//end method	

				public static void FireOnStateChangeEvt (PlayerState newState, System.Object[] exParams1, object[] exParams2)
				{
						if (onStateChange != null)
								onStateChange (newState, exParams1, exParams2);
				}//end method

				public PlayerState currentState = PlayerState.idle;		

				public delegate void playerStateHandler (PlayerStateController.PlayerState newState, System.Object[] exParams1, object[] exParams2);
				
				public static event playerStateHandler onStateChange;
				
				void OnDrawGizmos ()
				{
				}//end method
				
		}//end class

}//end namespace