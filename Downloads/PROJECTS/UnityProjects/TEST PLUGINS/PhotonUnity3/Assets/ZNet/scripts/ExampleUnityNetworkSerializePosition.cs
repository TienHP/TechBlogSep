using UnityEngine;
using System.Collections;

public class ExampleUnityNetworkSerializePosition : MonoBehaviour {

	public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){

		if (stream.isWriting){

			Vector3 position = transform.position;
			stream.Serialize(ref position);

		}//end if
		else{

			// read the first vector3 and store it in 'position'
			Vector3 position = Vector3.zero;
			stream.Serialize(ref position);
			//set the object's position to the value we were sent
			transform.position = position;

		}//end else

	}//end method

}//end class
