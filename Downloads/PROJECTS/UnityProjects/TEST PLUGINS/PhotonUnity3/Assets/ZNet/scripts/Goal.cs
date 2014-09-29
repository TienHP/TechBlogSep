using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	//the player who gets a point for this goal, 1 or 2
	public int player = 1;

	//the scorekeeper
	public ScoreKeeper scoreKeeper;

	public void GetPoint(){

		scoreKeeper.AddScore(player); 

	}//end method
}//end class
