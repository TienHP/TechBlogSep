using UnityEngine;
using System.Collections;

namespace SwingCow
{
		public class GameController : MonoBehaviour
		{

				public enum GameState
				{
						START, // start the game
						PLAYING // ready to control
				}

				public delegate void GameStateHandler (GameState newState);

				public static event GameStateHandler OnGameStateChangingEvt;

				// Use this for initialization
				void StartGame ()
				{
						if (OnGameStateChangingEvt != null)
								OnGameStateChangingEvt (GameState.START);
				}

				void BecomeControllable ()
				{
						if (OnGameStateChangingEvt != null)
								OnGameStateChangingEvt (GameState.PLAYING);
				}//end method
		}
}