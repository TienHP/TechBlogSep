using UnityEngine;
using System.Collections;

public class ExampleUnityNetworkingConnectToServer : MonoBehaviour {

	private string ip = "";
	private string port = "";
	private string password = "";
	private bool connectedServer = false;

	void OnGUI(){

		GUILayout.Label("IP Address");
		ip = GUILayout.TextField(ip, GUILayout.Width(200.0f));

		GUILayout.Label("Port");
		port = GUILayout.TextField(port, GUILayout.Width(50.0f));

		GUILayout.Label("Password (optional)");
		password = GUILayout.PasswordField (password, '*', GUILayout.Width(200.0f));

		if (GUILayout.Button("Connect")){

			int portNum = 25005;
			if (!int.TryParse(port, out portNum)){

				Debug.LogWarning("Given port is not a number");

			}//end if
			else{
				Network.Connect(ip, portNum, password);
				Debug.Log("debug connected");
			}//end else
		
		}//end if

		GUILayout.Label("CONNECTED SERVER STATUS: "+ connectedServer);
	}//end method

	void OnConnectedToServer(){
		Debug.Log("Connected to server");
		connectedServer = true;
	}//end method

	void OnFailedToConnect(NetworkConnectionError error){

		Debug.Log("Failed to connect to server: "+ error.ToString());

	}//end method
}//end class
