using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	//the maximum score a player can reach
	public int scoreLimit = 10;

	//the start points of each player paddle
	public Transform spawnP1;
	public Transform spawnP2;

	//the paddle prefab
	public GameObject paddlePrefab;

	//the display text of player 1 score
	public TextMesh player1ScoreDisplay;

	//the display text of player 2 score
	public TextMesh player2ScoreDisplay;

	//player 1's score
	private int p1Score = 0;

	//player 2's score
	private int p2Score = 0;

	void Start(){

		if (Network.isServer){
			Network.Instantiate(paddlePrefab, spawnP1.position, Quaternion.identity, 0);

			//nobody has joined yet, display "Waiting ..." for player 2
			player2ScoreDisplay.text = Network.player.ipAddress;
		}//end if

	}//end method

	void OnPlayerConnected(NetworkPlayer player){

		//when a player joins, tell them to spawn
		networkView.RPC("net_DoSpawn", player, spawnP2.position);
		//change player 2's score display from "waiting ..." to "0"
		player2ScoreDisplay.text = "0";

	}//end method


	void OnPlayerDisconnected(NetworkPlayer player){

		//player 2 left, reset scores
		p1Score = 0;
		p2Score = 0;

		//display each player's scores
		//display "Waiting ..." for player 2
		player1ScoreDisplay.text = p1Score.ToString();
		player2ScoreDisplay.text = "Waiting ...";

	}//end method

	void OnDisconnectedFromServer(NetworkDisconnection cause){
		//go back to the main menu
		Application.LoadLevel("Menu");
	}//end method

	[RPC]
	void net_DoSpawn(Vector3 position){

		Network.Instantiate(paddlePrefab, position, Quaternion.identity, 0); 

	}//end method

	public void AddScore( int player ){
		//networkView.RPC("net_AddScore", RPCMode.All, player);
		net_AddScore(player);
	}//end method

	public void net_AddScore( int player ){

		if (player == 1){
			p1Score++;
		}
		else if (player == 2){
			p2Score++;
		}

		Debug.Log("p1 score: "+p1Score);
		Debug.Log("p2 score: "+p2Score);

		if (p1Score >= scoreLimit || p2Score >= scoreLimit){

			if (p1Score > p2Score){
				Debug.Log("player 1 wins");
			}
			else if (p2Score > p1Score)
				Debug.Log("player 2 wins");
			else
				Debug.Log("players are tied");
		
			p1Score = 0;
			p2Score = 0;
		}//end if

		player1ScoreDisplay.text = p1Score.ToString();
		player2ScoreDisplay.text = p2Score.ToString();

	}//end method

}//end class
