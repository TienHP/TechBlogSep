using UnityEngine;
using System.Collections;

namespace SwingCow
{
		public class PlayerGameStateListener : MonoBehaviour
		{
				// Use this for initialization
				void Start ()
				{
						GameController.OnGameStateChangingEvt += HandleOnGameStateChangingEvt;
				}//end method

				void HandleOnGameStateChangingEvt (GameController.GameState newState)
				{
						switch (newState) {
						case GameController.GameState.START:
								Debug.Log ("start game");
								PlayerStateController.FireOnStateChangeEvt (PlayerStateController.PlayerState.jump_funny, null, null);
								break;
						}//end switch
				}//end method
	
			
	
		}//end class
}//end namespace