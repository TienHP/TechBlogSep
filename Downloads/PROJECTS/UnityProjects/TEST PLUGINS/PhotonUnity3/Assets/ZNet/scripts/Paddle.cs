using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]
public class Paddle : MonoBehaviour {

	//how fast the paddle can move
	public float moveSpeed = 10.0f;

	//how far up and down the paddle can move
	public float moveRange = 10.0f;

	//whethe this paddle can accept player input
	public bool acceptsInput = true;

	//cache the transform
	private Transform m_transform;

	//the position read from the network
	//used for interpolation
	private Vector3 readNetworkPos;

	void Start(){

		m_transform = transform;
		acceptsInput = networkView.isMine;
		networkView.observed = this;
		m_transform.localScale = new Vector3(1, 1, 4);

	}//end method

	void Update(){

		if (!acceptsInput){
			transform.position = Vector3.Lerp(transform.position, readNetworkPos, 10.0f * Time.deltaTime);
			//dont use player input
			return ; 
		}//end if

		//get user input
		float input = Input.GetAxis("Vertical");

		//move the paddle
		Vector3 pos = transform.position;
		pos.z += input * moveSpeed * Time.deltaTime;

		//clamp paddle position
		//pos.z = Mathf.Clamp (pos.x, -moveRange, moveRange);

		//set position
		transform.position = pos;

	}//end method

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		if (stream.isWriting){

			Vector3 pos = transform.position;
			stream.Serialize(ref pos);

		}//end if
		else{

			Vector3 pos = Vector3.zero;
			stream.Serialize(ref pos);
			readNetworkPos = pos;

		}//end else
	}//end method

}//end class
