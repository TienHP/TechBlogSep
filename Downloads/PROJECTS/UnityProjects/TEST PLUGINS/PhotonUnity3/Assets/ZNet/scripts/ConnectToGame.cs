using UnityEngine;
using System.Collections;

public class ConnectToGame : MonoBehaviour {

	private string ip = "";
	private int port = 25005;

	void OnGUI(){
		if (isConnected)
			return;

		//let the user enter IP address
		GUILayout.Label("IP Address");
		ip = GUILayout.TextField(ip, GUILayout.Width(200f));

		//let the user enter port number
		GUILayout.Label("Port");
		string port_str = GUILayout.TextField(port.ToString(), GUILayout.Width(100f));
		int port_number = port;
		if (int.TryParse(port_str, out port_number))
			port = port_number;

		//connect to the IP and port
		if (GUILayout.Button("Connect", GUILayout.Width(100f))){
			Network.Connect(ip, port);
		}//end if

		//host a server on the given port, only allow 1 incoming
		if (GUILayout.Button("Host", GUILayout.Width(100.0f))){
			Network.InitializeServer( 2, port, true);
		}//end if

	}//end method

	private bool isConnected = false;
	void OnConnectedToServer(){
		Debug.Log("connected to server: "+Network.player.ipAddress);
		isConnected = true;
		NetworkLevelLoader.Instance.LoadLevel("Game");
	}//end method

	void OnServerInitialized(){
		Debug.Log("server initialized");
		Debug.Log("connected to server: "+Network.player.ipAddress);
		NetworkLevelLoader.Instance.LoadLevel("Game");
	}//end method

	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
	}//end method

}//end class
