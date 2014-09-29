using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]
public class ExampleUnityNetworkCallRPC : MonoBehaviour {

	void Update(){

		if (!networkView.isMine){
			return;
		}//end if

		if (Input.GetKeyDown ( KeyCode.Space )){
			networkView.RPC("testRPC", RPCMode.All);
		}
	}//end method

	[RPC]
	void testRPC(NetworkMessageInfo info){
		Debug.Log("test RPC called from "+info.sender.ipAddress);
	}//end method
}//end class
