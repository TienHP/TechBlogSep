using UnityEngine;
using System.Collections;

public class ExampleUnityNetworkInitializeServer : MonoBehaviour {

	private bool isInitialized = false;
	public bool isServer = false;

	void OnGUI(){

		if (!isServer)
			return;

		if (!isInitialized)
			if (GUILayout.Button("Lauch Server")){
				LauchServer();
			}//end if

		GUILayout.Label("INIT STATUS: " + isInitialized);
	}//end method

	void LauchServer(){

		Network.InitializeServer(8, 25005, true);

	}//end method

	void OnServerInitialized(){

		Debug.Log("Server initialized");
		isInitialized = true;
	}//end method

}//end class
